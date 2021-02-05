using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Implementation
{
    public class ChatEventRepository: IChatEventRepository
    {
        private readonly ChatNpgSQLContext _context;

        public ChatEventRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public async Task<ChatEvent> FindNewAndUpdateAsync()
        {
            var entity = _context.ChatEvents
                .Include(c => c.Message)
                .Include(c => c.User)
                .FirstOrDefault(item => item.State == 0 || item.State == 3); //
            if (entity != null)
            {
                entity.State = 1;
                await _context.SaveChangesAsync();
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
