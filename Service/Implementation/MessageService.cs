using AutoMapper;
using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using DataAccess.Model;
using Repository;
using Repository.Storage.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly ChatNpgSQLContext _context;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IChatEventRepository _chatEventRepository;
        private readonly IBotNotifier _botNotifier;

        public MessageService(ChatNpgSQLContext context, 
            IMessageRepository messageRepository,
            IChatEventRepository chatEventRepository,
            IMapper mapper,
            IBotNotifier botNotifier)
        {
            _context = context;
            _messageRepository = messageRepository;
            _mapper = mapper;
            _chatEventRepository = chatEventRepository;
            _botNotifier = botNotifier;
        }

        public async Task<ServiceResponse<MessageGetDTO>> CreateMessageAsync(MessageAddDTO messageDTO)
        {
            ServiceResponse<MessageGetDTO> serviceResponse = new ServiceResponse<MessageGetDTO>();

            try
            {
                UserDialog userDialog = await _messageRepository.GetUserDialogByFKIdAsync(messageDTO.UserId, messageDTO.DialogId);
                if (userDialog == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет доступа";
                    return serviceResponse;
                }
                Message message = new Message()
                {
                    Text = messageDTO.Text,
                    UserDialog = userDialog
                };
                _messageRepository.Add(message);
                await _context.SaveChangesAsync();

                _chatEventRepository.Add(new ChatEvent()
                {
                    UserId = messageDTO.UserId,
                    DialogId = messageDTO.DialogId,
                    TypeOfActionId = (int)ActionTypes.NewMessage,
                    MessageId = message.Id
                });
                await _context.SaveChangesAsync();
                _botNotifier.NotifyAsync();

                serviceResponse.Data = _mapper.Map<MessageGetDTO>(messageDTO);
                serviceResponse.Data.Id = message.Id;
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteMessageAsync(MessageGetDTO message)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            try
            {
                if ((DateTime.UtcNow.Day - message.DateTime.Day) >= 1)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Срок истек";
                    return serviceResponse;
                }
                var b = await _messageRepository.HasUserWithId(message.UserId, message.Id);
                if (!b)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет доступа";
                    return serviceResponse;
                }
                if (await _messageRepository.DeleteAsync(message.Id))
                {
                    await _context.SaveChangesAsync();

                    _chatEventRepository.Add(new ChatEvent()
                    {
                        UserId = message.UserId,
                        DialogId = message.DialogId,
                        TypeOfActionId = (int)ActionTypes.MessageDeleted
                    });
                    await _context.SaveChangesAsync();
                    _botNotifier.NotifyAsync();

                    serviceResponse.Data = true;                    
                }                    
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Сообщение не найдено";
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message + ex.InnerException;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserBotMessageDTO[]>> GetMessagesByDialogIdAsync(int id, int limit = 50, int offset = 0)
        {
            ServiceResponse<UserBotMessageDTO[]> serviceResponse = new ServiceResponse<UserBotMessageDTO[]>();

            IEnumerable<UserBotMessage> messages = await _messageRepository.GetMessagesByDialogIdAsync(id, limit, offset);
            serviceResponse.Data = _mapper.Map<UserBotMessageDTO[]>(messages);

            return serviceResponse;
        }

        public async Task<ServiceResponse<MessageGetDTO>> GetMessageAsync(long id)
        {
            ServiceResponse<MessageGetDTO> serviceResponse = new ServiceResponse<MessageGetDTO>();

            serviceResponse.Data = _mapper.Map<MessageGetDTO>(await _messageRepository.GetAsync(id));

            return serviceResponse;
        } 
    }
}
