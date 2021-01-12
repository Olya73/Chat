using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDialogService
    {
        void AddUsersToDialogAsync(List<UserDialogAddDTO> userDialogDTOs);
        Task CreateGroupDialogAsync(DialogAddDTO dialogDTO);
        Task CreatePrivateDialogAsync(DialogAddDTO dialogDTO);
    }
}
