using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ConfigurationDatabase
{
    public class BotActionOnEventsConfiguration
    {
        public BotActionOnEventsConfiguration(EntityTypeBuilder<BotActionOnEvent> entityBuilder)
        {
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entityBuilder.Property(d => d.BotName).HasColumnName("bot_name").IsRequired();
            entityBuilder.Property(d => d.BotResponse).HasColumnName("bot_response");
            entityBuilder.Property(d => d.ChatEventId).HasColumnName("chat_event_id");
            entityBuilder.Property(d => d.DateTime)
                .HasColumnName("datetime")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("now() at time zone 'utc'");
            entityBuilder.HasOne(d => d.ChatEvent).WithMany(e => e.BotActionOnEvents).OnDelete(DeleteBehavior.SetNull);

            entityBuilder.ToTable("bot_action_on_events");
        }
    }
}
