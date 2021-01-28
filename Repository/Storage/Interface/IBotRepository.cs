using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Interface
{
    public interface IBotRepository
    {
        void AddBotToDialog(BotDialog botDialog);
        Task<Bot[]> GetAllWithTypeAsync();
        Task<string[]> GetBotsNamesByDialogIdAsync(int dialogId);
        void DeleteBotFromDialog(BotDialog botDialog);
    }
}
