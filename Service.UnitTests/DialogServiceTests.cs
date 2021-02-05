using AutoMapper;
using Contract.Bot.Interface;
using Moq;
using Repository;
using Repository.Storage.Interface;
using Service.Interface;
using Service.AutoMapper;
using System;
using Xunit;
using Service.Implementation;
using System.Threading.Tasks;
using Contract.DTO;
using DataAccess.Model;
using System.Threading;
using System.Collections.Generic;

namespace Service.UnitTests
{
    public class DialogServiceTests
    {
        private Mock<ChatNpgSQLContext> _dbContextMock;
        private Mock<IDialogRepository> _dialogRepositoryMock;
        private IDialogService _service;
        private Mock<IChatEventRepository> _chatEventRepositoryMock;
        private Mock<IBotNotifier> _botNotifierMock;

        public DialogServiceTests()
        {
            _dbContextMock = new Mock<ChatNpgSQLContext>();
            _dialogRepositoryMock = new Mock<IDialogRepository>();
            _chatEventRepositoryMock = new Mock<IChatEventRepository>();
            _botNotifierMock = new Mock<IBotNotifier>();

            Profile myProfile = new ChatProfile();
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(myProfile)));
            _service = new DialogService(_dbContextMock.Object, _dialogRepositoryMock.Object, mapper, _chatEventRepositoryMock.Object, _botNotifierMock.Object);
        }

        [Fact]
        public async Task CreateDialogAsyncAddsDialogCallsSaveChanges_and_ReturnsServiceResponseWithData()
        {
            DialogAddDTO dialogAddDTO = new DialogAddDTO() { Title = "some_title"};

            ServiceResponse<DialogGetDTO> serviceResponse = await _service.CreateDialogAsync(dialogAddDTO);

            _dialogRepositoryMock.Verify(r => r.Add(It.IsAny<Dialog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.AtLeast(1));
            Assert.IsAssignableFrom<ServiceResponse<DialogGetDTO>>(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.True(serviceResponse.Success);
            Assert.Equal(dialogAddDTO.Title, serviceResponse.Data.Title);
        }

        [Fact]
        public async Task CreateDialogAsyncReturnsServiceResponseWithExMessage_IfErrorOccurs()
        {
            DialogAddDTO dialogAddDTO = new DialogAddDTO() {};
            _dbContextMock.Setup(r => r.SaveChangesAsync(CancellationToken.None)).Callback(() => throw new Exception());

            ServiceResponse<DialogGetDTO> serviceResponse = await _service.CreateDialogAsync(dialogAddDTO);

            Assert.Null(serviceResponse.Data);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task AddUserToDialogAsyncAddsUserCallsSaveChanges_and_ReturnsServiceResponseWithData()
        {
            UserDialogAddDTO userDialogAddDTO = new UserDialogAddDTO();
            _dialogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(new Dialog()));

            ServiceResponse<int> serviceResponse = await _service.AddUserToDialogAsync(userDialogAddDTO);

            _dialogRepositoryMock.Verify(r => r.AddUserToDialog(It.IsAny<UserDialog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.AtLeast(1));
            Assert.IsAssignableFrom<ServiceResponse<int>>(serviceResponse);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task AddUserToDialogAsyncAddsChatEventCallsBotNotifier()
        {
            UserDialogAddDTO userDialogAddDTO = new UserDialogAddDTO();
            _dialogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(new Dialog()));

            ServiceResponse<int> serviceResponse = await _service.AddUserToDialogAsync(userDialogAddDTO);

            _chatEventRepositoryMock.Verify(r => r.Add(It.IsAny<ChatEvent>()), Times.Once);
            _botNotifierMock.Verify(r => r.NotifyAsync(), Times.Once);
        }

        [Fact]
        public async Task AddUserToDialogAsyncReturnsServiceResponseWithExMessage_IfErrorOccurs()
        {
            UserDialogAddDTO userDialogAddDTO = new UserDialogAddDTO();
            _dbContextMock.Setup(r => r.SaveChangesAsync(CancellationToken.None)).Callback(() => throw new Exception());

            ServiceResponse<int> serviceResponse = await _service.AddUserToDialogAsync(userDialogAddDTO);

            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task AddUserToDialogAsyncReturnsServiceResponseWithExMessage_IfDialogIsPrivate()
        {
            UserDialogAddDTO userDialogAddDTO = new UserDialogAddDTO();
            _dialogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(It.Is<Dialog>(m => m.IsTeteATete == true)));

            ServiceResponse<int> serviceResponse = await _service.AddUserToDialogAsync(userDialogAddDTO);

            _dialogRepositoryMock.Verify(r => r.AddUserToDialog(It.IsAny<UserDialog>()), Times.Never);
            _chatEventRepositoryMock.Verify(r => r.Add(It.IsAny<ChatEvent>()), Times.Never);
            _botNotifierMock.Verify(r => r.NotifyAsync(), Times.Never);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task DeleteUserFromDialogDeletesUserCallsSaveChanges_and_ReturnsServiceResponseWithData()
        {
            UserDialogAddDTO userDialogAddDTO = new UserDialogAddDTO();
            _dialogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(new Dialog()));
            _dialogRepositoryMock.Setup(r => r.GetUserDialogByFKIdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new UserDialog()));

            ServiceResponse<bool> serviceResponse = await _service.DeleteUserFromDialogAsync(userDialogAddDTO);

            _dialogRepositoryMock.Verify(r => r.DeleteUserFromDialog(It.IsAny<UserDialog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.AtLeast(1));
            Assert.IsAssignableFrom<ServiceResponse<bool>>(serviceResponse);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task DeleteUserFromDialogAsyncAddsChatEventCallsBotNotifier()
        {
            UserDialogAddDTO userDialogAddDTO = new UserDialogAddDTO();
            _dialogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(new Dialog()));
            _dialogRepositoryMock.Setup(r => r.GetUserDialogByFKIdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new UserDialog()));

            ServiceResponse<bool> serviceResponse = await _service.DeleteUserFromDialogAsync(userDialogAddDTO);

            _chatEventRepositoryMock.Verify(r => r.Add(It.IsAny<ChatEvent>()), Times.Once);
            _botNotifierMock.Verify(r => r.NotifyAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserFromDialogAsyncReturnsServiceResponseWithExMessage_IfDialogIsPrivate()
        {
            UserDialogAddDTO userDialogAddDTO = new UserDialogAddDTO();
            _dialogRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(It.Is<Dialog>(m => m.IsTeteATete == true)));
            _dialogRepositoryMock.Setup(r => r.GetUserDialogByFKIdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new UserDialog()));

            ServiceResponse<bool> serviceResponse = await _service.DeleteUserFromDialogAsync(userDialogAddDTO);

            _dialogRepositoryMock.Verify(r => r.DeleteUserFromDialog(It.IsAny<UserDialog>()), Times.Never);
            _chatEventRepositoryMock.Verify(r => r.Add(It.IsAny<ChatEvent>()), Times.Never);
            _botNotifierMock.Verify(r => r.NotifyAsync(), Times.Never);
            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task DeleteDialogAsyncCallsSaveChanges_and_ReturnsServiceResponseWithData()
        {
            int id = 1;
            _dialogRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(true));

            ServiceResponse<bool> serviceResponse = await _service.DeleteDialogAsync(id);

            _dialogRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.AtLeast(1));
            Assert.IsAssignableFrom<ServiceResponse<bool>>(serviceResponse);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task DeleteDialogAsyncReturnsServiceResponseWithExMessage_IfErrorOccurs()
        {
            int id = 1;
            _dbContextMock.Setup(r => r.SaveChangesAsync(CancellationToken.None)).Callback(() => throw new Exception());

            ServiceResponse<bool> serviceResponse = await _service.DeleteDialogAsync(id);

            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetUsersByDialogIdAsyncReturnsServiceResponseWithData()
        {
            int id = 1;

            ServiceResponse<UserGetDTO[]> serviceResponse = await _service.GetUsersByDialogIdAsync(id);

            Assert.IsAssignableFrom<ServiceResponse<UserGetDTO[]>>(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.True(serviceResponse.Success);
        }
    }
}
