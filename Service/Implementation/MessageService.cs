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
        private readonly IBotManager _botManager;
        private readonly IBotRepository _botRepository;
        private readonly IChatActionRepository _chatActionRepository;

        public MessageService(ChatNpgSQLContext context, 
            IMessageRepository messageRepository, 
            IMapper mapper,
            IBotManager botManager,
            IBotRepository botRepository,
            IChatActionRepository chatActionRepository)
        {
            _context = context;
            _messageRepository = messageRepository;
            _botManager = botManager;
            _botRepository = botRepository;
            _chatActionRepository = chatActionRepository;
            _mapper = mapper;
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
                if (!await _messageRepository.HasUserWithId(message.UserId, message.Id))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет доступа";
                    return serviceResponse;
                }
                if (await _messageRepository.DeleteAsync(message.Id))
                {
                    await _context.SaveChangesAsync();
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
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<MessageGetDTO[]>> GetMessagesByDialogId(int id, int limit = 50, int offset = 0)
        {
            ServiceResponse<MessageGetDTO[]> serviceResponse = new ServiceResponse<MessageGetDTO[]>();

            IEnumerable<Message> messages = await _messageRepository.GetMessagesByDialogIdAsync(id, limit, offset);
            serviceResponse.Data = _mapper.Map<MessageGetDTO[]>(messages);

            return serviceResponse;
        }

        public async Task<ServiceResponse<MessageGetDTO>> GetMessageAsync(long id)
        {
            ServiceResponse<MessageGetDTO> serviceResponse = new ServiceResponse<MessageGetDTO>();

            serviceResponse.Data = _mapper.Map<MessageGetDTO>(await _messageRepository.GetAsync(id));

            return serviceResponse;
        }

        public async Task<List<string>> GetBotsOnNewMessage(MessageGetDTO messageDTO)
        {
            List<string> responses = new List<string>();

            string[] names = await _botRepository.GetBotsNamesByDialogIdAsync(messageDTO.DialogId);
            BotMessageDTO botMessageDTO = new BotMessageDTO()
            {
                Login = messageDTO.User.Login,
                Message = messageDTO.Text
            };
            foreach (var response in _botManager.BotOnEvent(names, ActionTypes.NewMessage, botMessageDTO))
            {
                if (!String.IsNullOrEmpty(response))
                {
                    ChatAction chatAction = new ChatAction()
                    {
                        DialogId = messageDTO.DialogId,
                        UserId = messageDTO.UserId,
                        BotResponse = response,
                        TypeOfActionId = (int)ActionTypes.NewMessage,
                        MessageId = messageDTO.Id
                    };
                    _chatActionRepository.Add(chatAction);
                    await _context.SaveChangesAsync();
                    responses.Add(response);
                }
            }

            return responses;
        }
    }
}
