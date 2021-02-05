using AutoMapper;
using Contract.Bot.Interface;
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
    public class MessageServiceTests
    {
        private Mock<ChatNpgSQLContext> _dbContextMock;
        private Mock<IMessageRepository> _messageRepositoryMock;
        private IMessageService _service;
        private Mock<IChatEventRepository> _chatEventRepositoryMock;
        private Mock<IBotNotifier> _botNotifierMock;

        public MessageServiceTests()
        {
            _dbContextMock = new Mock<ChatNpgSQLContext>();
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _chatEventRepositoryMock = new Mock<IChatEventRepository>();
            _botNotifierMock = new Mock<IBotNotifier>();

            Profile myProfile = new ChatProfile();
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(myProfile)));
            _service = new MessageService(_dbContextMock.Object, _messageRepositoryMock.Object, _chatEventRepositoryMock.Object, mapper, _botNotifierMock.Object);
        }

        [Fact]
        public async Task CreateMessageAsyncAddsMessageCallsSaveChanges_and_ReturnsServiceResponseWithData()
        {
            MessageAddDTO messageAddDTO = new MessageAddDTO();
            _messageRepositoryMock.Setup(r => r.GetUserDialogByFKIdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new UserDialog()));

            ServiceResponse<MessageGetDTO> serviceResponse = await _service.CreateMessageAsync(messageAddDTO);

            _messageRepositoryMock.Verify(r => r.Add(It.IsAny<Message>()), Times.Once);
            _dbContextMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.AtLeast(1));
            Assert.IsAssignableFrom<ServiceResponse<MessageGetDTO>>(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task CreateMessageAsyncAddsChatEventCallsBotNotifier()
        {
            MessageAddDTO messageAddDTO = new MessageAddDTO();
            _messageRepositoryMock.Setup(r => r.GetUserDialogByFKIdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(new UserDialog()));

            ServiceResponse<MessageGetDTO> serviceResponse = await _service.CreateMessageAsync(messageAddDTO);

            _chatEventRepositoryMock.Verify(r => r.Add(It.IsAny<ChatEvent>()), Times.Once);
            _botNotifierMock.Verify(r => r.NotifyAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateMessageAsyncReturnsServiceResponseWithExMessage_IfErrorOccurs()
        {
            MessageAddDTO messageAddDTO = new MessageAddDTO();
            _dbContextMock.Setup(r => r.SaveChangesAsync(CancellationToken.None)).Callback(() => throw new Exception());

            ServiceResponse<MessageGetDTO> serviceResponse = await _service.CreateMessageAsync(messageAddDTO);

            Assert.False(serviceResponse.Success);
            Assert.NotNull(serviceResponse.Message);
        }

        [Fact]
        public async Task GetMessagesByDialogIdAsyncReturnsServiceResponseWithData()
        {
            int id = 1;

            ServiceResponse<UserBotMessageDTO[]> serviceResponse = await _service.GetMessagesByDialogIdAsync(id);

            Assert.IsAssignableFrom<ServiceResponse<UserBotMessageDTO[]>>(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public async Task GetMessageAsyncReturnsServiceResponseWithData()
        {
            long id = 1;
            Message message = new Message() { Id = id };
            _messageRepositoryMock.Setup(r => r.GetAsync(It.IsAny<long>())).Returns(Task.FromResult(message));

            ServiceResponse<MessageGetDTO> serviceResponse = await _service.GetMessageAsync(id);

            Assert.IsAssignableFrom<ServiceResponse<MessageGetDTO>>(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.True(serviceResponse.Success);
            Assert.Equal(serviceResponse.Data.Id, id);
        }
    }
}
