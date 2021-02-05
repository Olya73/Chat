using AutoMapper;
using Contract.DTO;
using DataAccess.Model;
using Moq;
using Repository;
using Repository.Storage.Interface;
using Service.AutoMapper;
using Service.Implementation;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Service.UnitTests
{
    public class BotServiceTests
    {
        private Mock<ChatNpgSQLContext> _dbContextMock;
        private Mock<IBotRepository> _botRepositoryMock;
        private IBotService _service;

        public BotServiceTests()
        {
            _dbContextMock = new Mock<ChatNpgSQLContext>();
            _botRepositoryMock = new Mock<IBotRepository>();

            Profile myProfile = new ChatProfile();
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(myProfile)));
            _service = new BotService(_dbContextMock.Object, _botRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task GetAllWithTypeAsyncReturnsServiceResponseWithData()
        {            

            ServiceResponse<BotGetDTO[]> serviceResponse = await _service.GetAllWithTypeAsync();

            Assert.IsAssignableFrom<ServiceResponse<BotGetDTO[]>>(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task AddBotToDialogAsyncCallsSaveChanges_and_ReturnsServiceResponseWithData()
        {
            string name = "name";
            BotDialogAddDTO botDialog = new BotDialogAddDTO() { BotName = name };

            ServiceResponse<string> serviceResponse = await _service.AddBotToDialogAsync(botDialog);

            _botRepositoryMock.Verify(r => r.AddBotToDialog(It.Is<BotDialog>(b => b.BotName == name)), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.AtLeast(1));
            Assert.IsAssignableFrom<ServiceResponse<string>>(serviceResponse);
            Assert.True(serviceResponse.Success);
            Assert.Equal(name, serviceResponse.Data);
        }

        [Fact]
        public async Task AddBotToDialogAsyncReturnsServiceResponseWithExMessage_IfErrorOccurs()
        {
            BotDialogAddDTO botDialog = new BotDialogAddDTO();
            _dbContextMock.Setup(r => r.SaveChangesAsync(CancellationToken.None)).Callback(() => throw new Exception());

            ServiceResponse<string> serviceResponse = await _service.AddBotToDialogAsync(botDialog);

            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }
        
        [Fact]
        public async Task DeleteBotFromDialogAsyncCallsSaveChanges_and_ReturnsServiceResponseWithData()
        {
            BotDialogAddDTO botDialog = new BotDialogAddDTO();

            ServiceResponse<bool> serviceResponse = await _service.DeleteBotFromDialogAsync(botDialog);

            _botRepositoryMock.Verify(r => r.DeleteBotFromDialog(It.IsAny<BotDialog>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.AtLeast(1));
            Assert.IsAssignableFrom<ServiceResponse<bool>>(serviceResponse);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task DeleteBotFromDialogAsyncReturnsServiceResponseWithExMessage_IfErrorOccurs()
        {
            BotDialogAddDTO botDialog = new BotDialogAddDTO();
            _dbContextMock.Setup(r => r.SaveChangesAsync(CancellationToken.None)).Callback(() => throw new Exception());

            ServiceResponse<bool> serviceResponse = await _service.DeleteBotFromDialogAsync(botDialog);

            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetUsersByDialogIdAsyncReturnsServiceResponseWithData()
        {
            string name = "name";
            _botRepositoryMock.Setup(r => r.GetAsync(name)).Returns(Task.FromResult(new Bot() { Name = name}));

            ServiceResponse<BotGetDTO> serviceResponse = await _service.GetBotAsync(name);

            Assert.IsAssignableFrom<ServiceResponse<BotGetDTO>>(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.True(serviceResponse.Success);
            Assert.Equal(name, serviceResponse.Data.Name);
        }

        [Fact]
        public async Task GetUsersByDialogIdAsyncReturnsServiceResponseWithExMessage_IfErrorOccurs()
        {
            string name = "name";
            _botRepositoryMock.Setup(r => r.GetAsync(name)).Returns(Task.FromResult((Bot)null));

            ServiceResponse<BotGetDTO> serviceResponse = await _service.GetBotAsync(name);

            Assert.IsAssignableFrom<ServiceResponse<BotGetDTO>>(serviceResponse);
            Assert.Null(serviceResponse.Data);
            Assert.False(serviceResponse.Success);
        }
    }
}
