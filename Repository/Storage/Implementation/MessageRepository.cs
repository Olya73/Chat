﻿using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Repository.Storage.Implementation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatNpgSQLContext _context;

        public MessageRepository(ChatNpgSQLContext context)
        {
            _context = context;
        }

        public void Add(Message obj)
        {
            _context.Messages.Add(obj);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var obj = await _context.Messages.SingleOrDefaultAsync(p => p.Id == id);
            if (obj is null)
            {
                return false;
            }
            _context.Messages.Remove(obj);

            return true;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToArrayAsync();
        }

        public async Task<Message> GetAsync(long id)
        {
            return await _context.Messages.Include(u=> u.UserDialog).ThenInclude(u => u.User).SingleOrDefaultAsync(p => p.Id == id);
        }

        public Message Update(Message obj)
        {
            _context.Messages.Update(obj);
            return obj;
        }

        public async Task<UserBotMessage[]> GetMessagesByDialogIdAsync(int id, int limit, int offset)
        {
            var usersMessages = _context.Messages
                .Where(m => m.UserDialog.DialogId == id)
                //.Include(s => s.UserDialog).ThenInclude(u => u.User)
                .Select(s => new UserBotMessage() 
                { 
                    Id = s.Id,
                    MemberId = s.UserDialog.UserId,
                    MemberName = s.UserDialog.User.Login,
                    MemberType = nameof(s.UserDialog.User),
                    DateTime = s.DateTime,
                    Text = s.Text 
                });
            var botsMessages = _context.BotActionOnEvents
                .Where(a => a.ChatEvent.DialogId == id)
                .Select(s => new UserBotMessage()
                {
                    Id = s.Id,
                    MemberId = 0,
                    MemberName = s.Bot.Name,
                    MemberType = nameof(s.Bot),
                    DateTime = s.DateTime,
                    Text = s.BotResponse
                });

            var messages = await usersMessages.Union(botsMessages)
                .OrderBy(d => d.DateTime)
                .Skip(offset)
                .Take(limit)
                .ToArrayAsync();
            return messages;            
        }

        public async Task<bool> HasUserWithId(int userId, long messId)
        {
            return await _context.Messages.AnyAsync(m => m.Id == messId && m.UserDialog.UserId == userId);
        }

        public async Task<UserDialog> GetUserDialogByDialogId(int dialogId)
        {
            UserDialog userDialog = await _context.UserDialogs.SingleOrDefaultAsync(u => u.DialogId == dialogId);
            return userDialog;
        }

        public async Task<UserDialog> GetUserDialogByFKIdAsync(int userId, int dialogId)
        {
            return await _context.UserDialogs.SingleOrDefaultAsync(u => u.UserId == userId && u.DialogId == dialogId);
        }
    }
}
