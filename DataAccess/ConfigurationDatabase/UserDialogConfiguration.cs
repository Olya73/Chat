using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class UserDialogConfiguration
    {
        public UserDialogConfiguration(EntityTypeBuilder<UserDialog> entityBuilder)
        {
            entityBuilder.HasKey(u => u.Id);
            entityBuilder.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(u => u.DialogId).HasColumnName("dialog_id").IsRequired();
            entityBuilder.Property(u => u.UserId).HasColumnName("user_id").IsRequired();           
            entityBuilder.Property(u => u.EnterDate)
                .HasColumnName("enter_date")
                .HasColumnType("timestamptz")
                .HasDefaultValue("current_timestamp()");
            entityBuilder.Property(u => u.LeaveDate).HasColumnName("leave_date").HasColumnType("timestamptz");

            entityBuilder.HasIndex(u => new { u.UserId, u.DialogId }).IsUnique();

            entityBuilder.ToTable("user_dialogs");
        }
    }
}
