using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Storage.Implementation
{
    public class BotRepository : IBotRepository
    {
        private readonly ChatNpgSQLContext _context;

        public BotRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public async Task<Bot[]> GetAllAsync()
        {
            return await _context.Bots.ToArrayAsync();
        }

        public async Task<Bot[]> GetAllWithTypeAsync()
        {
            return await _context.Bots
                //.Include(b => b.BotTypes).ThenInclude(b => b.TypeOfBot)//
                .Select(s =>
                new Bot()
                {
                    Name = s.Name,
                    Description = s.Description,
                    Implementation = s.Implementation,
                    BotTypes = (List<BotTypeOfBot>)s.BotTypes.Select(v => new BotTypeOfBot()
                    {
                        Members = v.Members,
                        TypeOfBot = v.TypeOfBot
                    })

                })
                .ToArrayAsync();
        }

        public async Task<string[]> GetBotsNamesByDialogIdAsync(int dialogId)
        {
            return await _context.Bots.SelectMany(b => b.BotDialogs).Where(d => d.DialogId == dialogId).Select(s => s.BotName).ToArrayAsync();
            //return await _context.Dialogs
            //    .Where(d => d.Id == dialogId)
            //    .SelectMany(d => d.BotDialogs)
            //    .Select(d => d.Bot.Name).ToArrayAsync();
        }

        public void AddChatAction()
        {

        }
    }
}
