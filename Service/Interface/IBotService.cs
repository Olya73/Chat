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
        Task<ServiceResponse<string>> AddBotToDialogAsync(BotDialogAddDTO botDialogDTO);
        Task<ServiceResponse<bool>> DeleteBotFromDialogAsync(BotDialogAddDTO botDialogDTO);
        Task<ServiceResponse<BotGetDTO[]>> GetAllWithTypeAsync();
        Task<ServiceResponse<BotGetDTO>> GetBotAsync(string name);
    }
}
