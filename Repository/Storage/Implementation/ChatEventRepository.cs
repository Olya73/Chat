using DataAccess.Model;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Storage.Implementation
{
    public class ChatEventRepository: IChatEventRepository
    {
        private readonly ChatNpgSQLContext _context;

        public ChatEventRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public ChatEvent FindNewAndUpdate()
        {
            var entity = _context.ChatEvents.FirstOrDefault(item => item.State == 0 || item.State == 3);
            if (entity != null)
            {
                entity.State = 1;
                _context.SaveChanges();
            }
            return entity;
        }

        public ChatEvent Update(ChatEvent obj)
        {
            _context.ChatEvents.Update(obj);
            return obj;
        }

        public void Add(ChatEvent obj)
        {
            _context.ChatEvents.Add(obj);
        }
    }
}
