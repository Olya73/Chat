using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class ChatActionConfiguration
    {
        public ChatActionConfiguration(EntityTypeBuilder<ChatAction> entityBuilder)
        {
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(d => d.TypeOfActionId).HasColumnName("type_of_action_id").IsRequired();
            entityBuilder.Property(d => d.MessageId).HasColumnName("message_id");
            entityBuilder.Property(d => d.UserId).HasColumnName("user_id").IsRequired();
            entityBuilder.Property(d => d.DialogId).HasColumnName("dialog_id").IsRequired();
            entityBuilder.Property(d => d.BotName).HasColumnName("bot_name");
            entityBuilder.Property(d => d.BotResponse).HasColumnName("bot_response");
            entityBuilder.HasOne(d => d.Message).WithMany(m => m.ChatActions).OnDelete(DeleteBehavior.Cascade);

            entityBuilder.ToTable("chat_actions");
        }
    }
}
