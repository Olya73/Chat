using Contract.Bot.Interface;
using DataAccess.ConfigurationDatabase;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatNpgSQLContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDialog> UserDialogs { get; set; }
        public DbSet<Bot> Bots { get; set; }
        public DbSet<BotDialog> BotDialogs { get; set; }
        public DbSet<ChatEvent> ChatEvents { get; set; }
        public DbSet<BotActionOnEvent> BotActionOnEvents { get; set; }

        public ChatNpgSQLContext(DbContextOptions<ChatNpgSQLContext> options) : base(options)
        { }
        protected ChatNpgSQLContext() { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new MessageConfiguration(builder.Entity<Message>());
            new DialogConfiguration(builder.Entity<Dialog>());
            new UserConfiguration(builder.Entity<User>());
            new UserDialogConfiguration(builder.Entity<UserDialog>());
            new BotConfiguration(builder.Entity<Bot>());
            new BotDialogConfiguration(builder.Entity<BotDialog>());
            new TypeOfActionConfiguration(builder.Entity<TypeOfAction>());
            new TypeOfBotConfiguration(builder.Entity<TypeOfBot>());
            new BotTypeOfBotConfiguration(builder.Entity<BotTypeOfBot>());
            new BotActionOnEventsConfiguration(builder.Entity<BotActionOnEvent>());
            new ChatEventConfiguration(builder.Entity<ChatEvent>());
        }
    }
}
