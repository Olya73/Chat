using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class ChatEventConfiguration
    {
        public ChatEventConfiguration(EntityTypeBuilder<ChatEvent> entityBuilder)
        {
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(d => d.TypeOfActionId).HasColumnName("type_of_action_id").IsRequired();
            entityBuilder.Property(d => d.MessageId).HasColumnName("message_id");
            entityBuilder.Property(d => d.UserId).HasColumnName("user_id").IsRequired();
            entityBuilder.Property(d => d.DialogId).HasColumnName("dialog_id").IsRequired();
            entityBuilder.Property(d => d.State).HasColumnName("state").IsRequired().HasDefaultValue(0);

            entityBuilder.ToTable("chat_events");
        }
    }
}
