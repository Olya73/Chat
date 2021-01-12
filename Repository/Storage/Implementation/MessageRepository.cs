using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Repository.Storage.Implementation
{
    class MessageRepository : IMessageRepository
    {
        private readonly ChatNpgSQLContext _context;

        public MessageRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public long Add(Message obj)
        {
            _context.Messages.Add(obj);
            return obj.Id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var obj = await _context.Messages.SingleOrDefaultAsync(p => p.Id == id);
            if (obj is null)
            {
                return false;
            }
            _context.Messages.Remove(obj);

            return true;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToArrayAsync();
        }

        public async Task<Message> GetAsync(long id)
        {
            return await _context.Messages.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Message Update(Message obj)
        {
            _context.Messages.Update(obj);
            return obj;
        }

        public async Task<Message[]> GetMessagesByDialogIdAsync(int id, int limit, int offset)
        {
            var messages = await _context.Messages
                .Include(m => m.UserDialog)
                    .ThenInclude(u => u.Dialog.Id == id)
                .OrderBy(d => d.DateTime)
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync();
            return messages;            
        }

        public async Task<bool> HasUserWithId(int userId, int messId)
        {
            return await _context.Messages
                .Where(m => m.Id == messId)
                .Include(m => m.UserDialog.UserId == userId)
                .AnyAsync();
        }
    }
}
