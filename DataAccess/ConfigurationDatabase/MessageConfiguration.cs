using DataAccess.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class MessageConfiguration
    {
        public MessageConfiguration(EntityTypeBuilder<Message> entityBuilder)
        {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.Property(m => m.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(m => m.Text).HasColumnName("text").HasColumnType("text").IsRequired();
            entityBuilder.Property(m => m.UserDialogId).HasColumnName("user_dialog_id").IsRequired();
            entityBuilder.Property(m => m.DateTime)
                .HasColumnName("datetime")
                .HasColumnType("timestamptz")
                .HasDefaultValue("current_timestamp()");

            entityBuilder.ToTable("messages");
        }
    }
}
