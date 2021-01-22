using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class BotConfiguration
    {
        public BotConfiguration(EntityTypeBuilder<Bot> entityBuilder)
        {
            entityBuilder.HasKey(b => b.Name);
            entityBuilder.Property(b => b.Name).HasColumnName("name").HasMaxLength(30);
            entityBuilder.Property(b => b.Description).HasColumnName("description").IsRequired();
            entityBuilder.Property(b => b.Implementation).HasColumnName("implementation").HasColumnType("text").IsRequired();

            entityBuilder.ToTable("bots");
        }
    }
}
