using DataAccess.Model;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storage.Implementation
{
    public class BotActionOnEventRepository: IBotActionOnEventRepository
    {
        private readonly ChatNpgSQLContext _context;

        public BotActionOnEventRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public void Add(BotActionOnEvent obj)
        {
            _context.BotActionOnEvents.Add(obj);
        }
    }
}
