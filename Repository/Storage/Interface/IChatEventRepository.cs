using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Interface
{
    public interface IChatEventRepository
    {
        void Add(ChatEvent obj);
        Task<ChatEvent> FindNewAndUpdateAsync();
        ChatEvent Update(ChatEvent obj);
    }
}
