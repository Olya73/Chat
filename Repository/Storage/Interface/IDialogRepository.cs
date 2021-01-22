using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Interface
{
    public interface IDialogRepository : IRepository<Dialog, int>
    {
        void AddUserToDialog(UserDialog userDialog);
        void DeleteUserFromDialog(UserDialog userDialog);
        Task<UserDialog> GetUserDialogByFKIdAsync(int userId, int dialogId);
        Task<User[]> GetUsersByDialogIdAsync(int id);
    }
}
