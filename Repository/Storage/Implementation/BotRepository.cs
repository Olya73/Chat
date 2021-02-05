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

        public async Task<IEnumerable<Bot>> GetAllAsync()
        {
            return await _context.Bots.ToArrayAsync();
        }

        public async Task<Bot[]> GetAllWithTypeAsync()
        {
            return await _context.Bots
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

                }).AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<string[]> GetBotsNamesByDialogIdAsync(int dialogId)
        {
            return await _context.Bots
                .SelectMany(b => b.BotDialogs)
                .Where(d => d.DialogId == dialogId)
                .Select(s => s.Bot.Name).ToArrayAsync();
            //return await _context.Dialogs
            //    .Where(d => d.Id == dialogId)
            //    .SelectMany(d => d.BotDialogs)
            //    .Select(d => d.Bot.Name).ToArrayAsync();
        }

        public void AddBotToDialog(BotDialog botDialog)
        {
            _context.BotDialogs.Add(botDialog);
        }

        public void DeleteBotFromDialog(BotDialog botDialog)
        {
            _context.BotDialogs.Remove(botDialog);
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var obj = await _context.Bots.SingleOrDefaultAsync(p => p.Name == name);
            if (obj is null)
            {
                return false;
            }
            _context.Bots.Remove(obj);

            return true;
        }

        public async Task<Bot> GetAsync(string name)
        {
            return await _context.Bots.SingleOrDefaultAsync(p => p.Name == name);
        }

        public void Add(Bot obj)
        {
            _context.Bots.Add(obj);
        }

        public Bot Update(Bot obj)
        {
            _context.Bots.Update(obj);
            return obj;
        }
    }
}
