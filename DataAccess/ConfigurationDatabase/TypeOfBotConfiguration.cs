using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class TypeOfBotConfiguration
    {
        public TypeOfBotConfiguration(EntityTypeBuilder<TypeOfBot> entityBuilder)
        {
            entityBuilder.HasKey(b => b.Id);
            entityBuilder.Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(d => d.Inteface).HasColumnName("interface").HasMaxLength(40).IsRequired();
            entityBuilder.Property(d => d.Type).HasColumnName("type").HasMaxLength(20).IsRequired();

            entityBuilder.ToTable("type_of_bots");
        }
    }
}
