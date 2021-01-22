using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class BotDialogConfiguration
    {
        public BotDialogConfiguration(EntityTypeBuilder<BotDialog> entityBuilder)
        {
            entityBuilder.HasKey(b => new { b.DialogId, b.BotName });
            entityBuilder.Property(b => b.DialogId).HasColumnName("dialog_id");
            entityBuilder.Property(b => b.BotName).HasColumnName("bot_id");

            entityBuilder.HasOne(b => b.Bot)
                .WithMany(b => b.BotDialogs)
                .HasForeignKey(b => b.BotName);

            entityBuilder.HasOne(b => b.Dialog)
                .WithMany(b => b.BotDialogs)
                .HasForeignKey(b => b.DialogId);

            entityBuilder.ToTable("bot_dialogs");
        }
    }
}
