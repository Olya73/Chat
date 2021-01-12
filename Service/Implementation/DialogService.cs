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

        public async Task CreatePrivateDialog(DialogAddDTO dialogDTO)
        {
            if (dialogDTO.IsTeteATete && dialogDTO.UsersId.Count != 2) throw new Exception();
            Dialog dialog = null;
            _dialogRepository.Add(dialog);
            await _context.SaveChangesAsync();
        }

        public async Task CreateGroupDialog(DialogAddDTO dialogDTO)
        {
            Dialog dialog = null;
            _dialogRepository.Add(dialog);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserToDialogAsync(UserDialogAddDTO userDialogDTO)
        {
            var id = userDialogDTO.DialogId;
            var dialog = await _dialogRepository.GetAsync(id);
            if (dialog.IsTeteATete ) throw new Exception();
            UserDialog userDialogs = null;
            _dialogRepository.AddUserToDialog(userDialogs);
        }
    }
}
