using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IDialogService
    {
        void AddUsersToDialogAsync(List<UserDialogAddDTO> userDialogDTOs);
    }
}
