using AutoMapper;
using BotLibrary.Inteface;
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
        private readonly IBotManager _botManager;
        private readonly IBotRepository _botRepository;
        private readonly IChatActionRepository _chatActionRepository;

        public DialogService(ChatNpgSQLContext context, 
            IDialogRepository dialogRepository, 
            IMapper mapper, 
            IBotManager botManager, 
            IBotRepository botRepository,
            IChatActionRepository chatActionRepository)
        {
            _context = context;
            _dialogRepository = dialogRepository;
            _mapper = mapper;
            _botManager = botManager;
            _botRepository = botRepository;
            _chatActionRepository = chatActionRepository;
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

                string[] names = await _botRepository.GetBotsNamesByDialogIdAsync(userDialogDTO.DialogId);
                foreach (var response in _botManager.BotOnMessage(names, "NewMessage"))
                {
                    ChatAction chatAction = new ChatAction()
                    {
                        DialogId = userDialogDTO.DialogId,
                        UserId = userDialogDTO.UserId,
                        BotResponse = response,
                        TypeOfActionId = 1
                    };
                    _chatActionRepository.Add(chatAction);
                }

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

        public async Task<ServiceResponse<UserGetDTO[]>> GetUsersByDialogId(int id)
        {
            ServiceResponse<UserGetDTO[]> serviceResponse = new ServiceResponse<UserGetDTO[]>();

            serviceResponse.Data = _mapper.Map<UserGetDTO[]>(await _dialogRepository.GetUsersByDialogIdAsync(id));
            foreach (var bot in _botManager.BotOnMessage(new string[] { "Hi", "Shit" }, "rthytffg"))
            {
                var str = bot;
            }
            return serviceResponse;
        }
    }
}
