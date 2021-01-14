using AutoMapper;
using DataAccess.Model;
using Repository;
using Repository.Storage.Interface;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class DialogService: IDialogService
    {
        private readonly ChatNpgSQLContext _context;
        private readonly IDialogRepository _dialogRepository;
        private readonly IMapper _mapper;

        public DialogService(ChatNpgSQLContext context, IDialogRepository dialogRepository, IMapper mapper)
        {
            _context = context;
            _dialogRepository = dialogRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DialogGetDTO>> CreateDialogAsync(DialogAddDTO dialogDTO)
        {
            ServiceResponse<DialogGetDTO> serviceResponse = new ServiceResponse<DialogGetDTO>();

            try
            {
                if (dialogDTO.IsTeteATete && dialogDTO.UserIds.Count != 2)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Только два пользователя";
                    return serviceResponse;
                }
                Dialog dialog = _mapper.Map<Dialog>(dialogDTO);
                _dialogRepository.Add(dialog);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<DialogGetDTO>(dialogDTO);
                serviceResponse.Data.Id = dialog.Id;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> AddUserToDialogAsync(UserDialogAddDTO userDialogDTO)
        {
            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();

            try
            {
                var dialog = await _dialogRepository.GetAsync(userDialogDTO.DialogId);
                if (dialog.IsTeteATete)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Только два пользователя";
                    return serviceResponse;
                }
                UserDialog userDialog = new UserDialog()
                {
                    DialogId = userDialogDTO.DialogId,
                    UserId = userDialogDTO.UserId
                };
                _dialogRepository.AddUserToDialog(userDialog);
                await _context.SaveChangesAsync();
                serviceResponse.Data = userDialog.UserId;
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteUserFromDialogAsync(UserDialogAddDTO userDialogDTO)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            try
            {
                UserDialog userDialog = await _dialogRepository.GetUserDialogByFKIdAsync(userDialogDTO.UserId, userDialogDTO.DialogId);
                if (userDialog == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого пользователя";
                    return serviceResponse;
                }
                var dialog = await _dialogRepository.GetAsync(userDialogDTO.DialogId);
                if (dialog.IsTeteATete)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Только два пользователя. Нельзя удалить из приватной беседы";
                    return serviceResponse;
                }                
                _dialogRepository.DeleteUserFromDialog(userDialog);
                await _context.SaveChangesAsync();
                serviceResponse.Data = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteDialogAsync(int id)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            try
            {
                bool deleted = await _dialogRepository.DeleteAsync(id);
                await _context.SaveChangesAsync();
                serviceResponse.Data = deleted;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

            return serviceResponse;
        }
    }
}
