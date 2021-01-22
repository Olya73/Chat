using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storage.Interface
{
    public interface IChatActionRepository
    {
        void Add(ChatAction obj);
    }
}
