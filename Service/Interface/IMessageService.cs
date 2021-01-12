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
        Task DeleteMessageAsync(MessageGetDTO message);
        Task<MessageGetDTO> CreateMessage(MessageAddDTO messageDTO);
        Task<Message> GetMessageAsync(long id);
    }
}
