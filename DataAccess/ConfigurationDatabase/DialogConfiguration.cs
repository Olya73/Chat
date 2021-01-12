using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class DialogConfiguration
    {
        public DialogConfiguration(EntityTypeBuilder<Dialog> entityBuilder)
        {
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(d => d.Title).HasColumnName("title").IsRequired();
            entityBuilder.Property(d => d.IsTeteATete).HasColumnName("is_tete_a_tete").HasDefaultValue(false);

            entityBuilder.ToTable("dialogs");
        }
    }
}
