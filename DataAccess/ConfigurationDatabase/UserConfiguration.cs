using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(u => u.Id);
            entityBuilder.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(u => u.Login).HasColumnName("login").HasMaxLength(30).IsRequired();

            entityBuilder.ToTable("users");
        }
    }
}
