﻿using DataAccess.ConfigurationDatabase;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ChatNpgSQLContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDialog> UserDialogs { get; set; }

        public ChatNpgSQLContext(DbContextOptions<ChatNpgSQLContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new MessageConfiguration(builder.Entity<Message>());
            new DialogConfiguration(builder.Entity<Dialog>());
            new UserConfiguration(builder.Entity<User>());
            new UserDialogConfiguration(builder.Entity<UserDialog>());
        }
    }
}