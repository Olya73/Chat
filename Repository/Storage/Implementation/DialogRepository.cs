using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
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

        public int Add(Dialog obj)
        {
            _context.Dialogs.Add(obj);
            return obj.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _context.Dialogs.SingleAsync(p => p.Id == id);
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
    }
}
