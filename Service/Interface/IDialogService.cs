using Contract.DTO;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDialogService
    {
        Task<ServiceResponse<int>> AddUserToDialogAsync(UserDialogAddDTO userDialogDTO);
        Task<ServiceResponse<DialogGetDTO>> CreateDialogAsync(DialogAddDTO dialogDTO);
        Task<ServiceResponse<bool>> DeleteDialogAsync(int id);
        Task<ServiceResponse<bool>> DeleteUserFromDialogAsync(UserDialogAddDTO userDialogDTO);
        Task<ServiceResponse<UserGetDTO[]>> GetUsersByDialogIdAsync(int id);
    }
}
