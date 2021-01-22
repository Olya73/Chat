using DataAccess.Model;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Storage.Interface;

namespace Service.Implementation
{
    public class BotService : IBotService
    {
        private readonly ChatNpgSQLContext _myDBContext;
        private readonly IBotRepository _botRepository;

        public BotService(ChatNpgSQLContext myDBContext, IBotRepository botRepository)
        {
            _myDBContext = myDBContext;
            _botRepository = botRepository;

        }

        public async Task<Bot[]> GetAllWithTypeAsync()
        {
            return await _botRepository.GetAllWithTypeAsync();
        }

        public async Task<Bot[]> GetBotsByDialogAsync()
        {
            throw new NotImplementedException();
        }
    }
}
