using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class BotTypeOfBotConfiguration
    {
        public BotTypeOfBotConfiguration(EntityTypeBuilder<BotTypeOfBot> entityBuilder)
        {
            entityBuilder.HasKey(ic => new { ic.BotName, ic.TypeOfBotId });
            entityBuilder.Property(ic => ic.TypeOfBotId).HasColumnName("type_of_bot_id");
            entityBuilder.Property(ic => ic.BotName).HasColumnName("bot_id");

            entityBuilder.HasOne(i => i.Bot)
                .WithMany(ic => ic.BotTypes)
                .HasForeignKey(ic => ic.BotName);

            entityBuilder.HasOne(c => c.TypeOfBot)
                .WithMany(ic => ic.BotTypes)
                .HasForeignKey(ic => ic.TypeOfBotId);

            entityBuilder.Property(d => d.Members).HasColumnName("members").HasColumnType("varchar(20)[]");

            entityBuilder.ToTable("bot_type_of_bots");
        }
    }
}
