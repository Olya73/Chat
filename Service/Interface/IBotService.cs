using Contract.DTO;
using DataAccess.Model;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBotService
    {
        Task<ServiceResponse<string>> AddBotToDialog(BotDialogAddDTO botDialogDTO);
        Task<ServiceResponse<bool>> DeleteBotFromDialog(BotDialogAddDTO botDialogDTO);
        Task<Bot[]> GetAllWithTypeAsync();
    }
}
