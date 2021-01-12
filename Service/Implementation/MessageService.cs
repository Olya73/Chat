using AutoMapper;
using DataAccess.Model;
using Repository;
using Repository.Storage.Interface;
using Service.DTO;
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

        public MessageService(ChatNpgSQLContext context, IMessageRepository messageRepository, IMapper mapper)
        {
            _context = context;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<MessageGetDTO> CreateMessage(MessageAddDTO messageDTO)
        {
            Message message = new Message()
            {
                DateTime = messageDTO.DateTime,
            };
            long id = _messageRepository.Add(message);
            
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task DeleteMessageAsync(MessageGetDTO message)
        {
            if ((DateTime.UtcNow - message.DateTime).Days < 1)
                if (await _messageRepository.DeleteAsync(message.Id))
                    await _context.SaveChangesAsync();
        }

        public async Task GetMessagesByDialogId(int id, int limit, int offset = 0)
        {
            await _messageRepository.GetMessagesByIdDialog(id, offset, limit);
        }

        public async Task<Message> GetMessage(long id)
        {
            return await _messageRepository.GetAsync(id);
        }
    }
}
