using DataAccess.Model;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMessageService
    {
        public Task DeleteMessageAsync(MessageGetDTO message);
        public Task<MessageGetDTO> CreateMessage(MessageGetDTO message);
        Task<Message> GetMessage(long id);
    }
}
