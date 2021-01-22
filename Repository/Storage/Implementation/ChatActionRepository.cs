using DataAccess.Model;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storage.Implementation
{
    public class ChatActionRepository: IChatActionRepository
    {
        private readonly ChatNpgSQLContext _context;

        public ChatActionRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public void Add(ChatAction obj)
        {
            _context.ChatActions.Add(obj);
        }
    }
}
