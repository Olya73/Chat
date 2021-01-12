using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Interface
{
    public interface IMessageRepository : IRepository<Message, long>
    {
        Task<Message[]> GetMessagesByIdDialog(int id, int limit, int offset);
    }
}
