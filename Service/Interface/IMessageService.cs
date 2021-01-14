using DataAccess.Model;
using Service.DTO;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMessageService
    {
        Task<ServiceResponse<MessageGetDTO>> CreateMessageAsync(MessageAddDTO messageDTO);
        Task<ServiceResponse<bool>> DeleteMessageAsync(MessageGetDTO message);
        Task<ServiceResponse<MessageGetDTO[]>> GetMessagesByDialogId(int id, int limit = 50, int offset = 0);
        Task<ServiceResponse<MessageGetDTO>> GetMessageAsync(long id);
    }
}
