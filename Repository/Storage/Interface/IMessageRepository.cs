using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Interface
{
    public interface IMessageRepository : IRepository<Message, long>
    {
        Task<UserDialog> GetUserDialogByDialogId(int dialogId);
        Task<Message[]> GetMessagesByDialogIdAsync(int id, int limit, int offset);
        Task<UserDialog> GetUserDialogByFKIdAsync(int userId, int dialogId);
        Task<bool> HasUserWithId(int userId, long messId);
    }
}
