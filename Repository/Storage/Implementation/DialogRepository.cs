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
    public class DialogRepository: IDialogRepository
    {
        private readonly ChatNpgSQLContext _context;

        public DialogRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public void Add(Dialog obj)
        {
            _context.Dialogs.Add(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _context.Dialogs.SingleOrDefaultAsync(p => p.Id == id);
            if (obj is null)
            {
                return false;
            }
            _context.Dialogs.Remove(obj);

            return true;
        }

        public async Task<IEnumerable<Dialog>> GetAllAsync()
        {
            return await _context.Dialogs.ToArrayAsync();
        }

        public async Task<Dialog> GetAsync(int id)
        {
            return await _context.Dialogs.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Dialog Update(Dialog obj)
        {
            _context.Dialogs.Update(obj);
            return obj;
        }

        public void AddUserToDialog(UserDialog userDialog)
        {
            _context.UserDialogs.Add(userDialog);
        }

        public void DeleteUserFromDialog(UserDialog userDialog)
        {
            _context.UserDialogs.Remove(userDialog);
        }

        public async Task<UserDialog> GetUserDialogByFKIdAsync(int userId, int dialogId)
        {
            return await _context.UserDialogs.SingleOrDefaultAsync(u => u.UserId == userId && u.DialogId == dialogId);
        }

        public async Task<UserDialog[]> GetUsersAsync(int id)
        {
            return await _context.UserDialogs.Where(u => u.DialogId == id).Include(u => u.User).ToArrayAsync();
        }
    }
}
