using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storage.Interface
{
    public interface IBotActionOnEventRepository
    {
        void Add(BotActionOnEvent obj);
    }
}
