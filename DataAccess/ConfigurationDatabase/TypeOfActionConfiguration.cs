using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class TypeOfActionConfiguration
    {
        public TypeOfActionConfiguration(EntityTypeBuilder<TypeOfAction> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(b => b.Id).HasColumnName("id").HasDefaultValueSql("2 ^ nextval('seq_action_types')");
            entityBuilder.Property(t => t.Type).HasColumnName("type").IsRequired();

            entityBuilder.ToTable("type_of_actions");
        }
    }
}
