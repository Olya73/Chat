using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storage.Interface
{
    public interface IChatEventRepository
    {
        void Add(ChatEvent obj);
        ChatEvent FindNewAndUpdate();
        ChatEvent Update(ChatEvent obj);
    }
}
