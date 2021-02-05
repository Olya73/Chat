using DataAccess.Model;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Storage.Interface;
using AutoMapper;
using Contract.DTO;
using Contract.Bot;
using Contract.Bot.Interface;

namespace Service.Implementation
{
    public class BotService : IBotService
    {
        private readonly ChatNpgSQLContext _myDBContext;
        private readonly IBotRepository _botRepository;
        private readonly IMapper _mapper;


        public BotService(ChatNpgSQLContext myDBContext, IBotRepository botRepository, IMapper mapper)
        {
            _myDBContext = myDBContext;
            _botRepository = botRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<BotGetDTO[]>> GetAllWithTypeAsync()
        {
            ServiceResponse<BotGetDTO[]> serviceResponse = new ServiceResponse<BotGetDTO[]>();
            serviceResponse.Data = _mapper.Map<BotGetDTO[]>(await _botRepository.GetAllWithTypeAsync()); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> AddBotToDialogAsync(BotDialogAddDTO botDialogDTO)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            try
            {
                BotDialog botDialog = _mapper.Map<BotDialog>(botDialogDTO);
                _botRepository.AddBotToDialog(botDialog);
                await _myDBContext.SaveChangesAsync();
                serviceResponse.Data = botDialog.BotName;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteBotFromDialogAsync(BotDialogAddDTO botDialogDTO)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            try
            {
                BotDialog botDialog = _mapper.Map<BotDialog>(botDialogDTO);
                _botRepository.DeleteBotFromDialog(botDialog);
                await _myDBContext.SaveChangesAsync();
                serviceResponse.Data = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<BotGetDTO>> GetBotAsync(string name)
        {
            ServiceResponse<BotGetDTO> serviceResponse = new ServiceResponse<BotGetDTO>();

            Bot bot = await _botRepository.GetAsync(name);
            if (bot != null)
            {
                serviceResponse.Data = _mapper.Map<BotGetDTO>(bot);
                return serviceResponse;
            }
            serviceResponse.Success = false;
            serviceResponse.Message = "Бот с таким именем/ид не найден";
            return serviceResponse;
        }
    }
}
