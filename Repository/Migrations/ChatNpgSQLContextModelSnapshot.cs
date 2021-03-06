﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(ChatNpgSQLContext))]
    partial class ChatNpgSQLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("DataAccess.Model.Bot", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("name");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Implementation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("implementation");

                    b.HasKey("Name");

                    b.ToTable("bots");
                });

            modelBuilder.Entity("DataAccess.Model.BotActionOnEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("BotName")
                        .IsRequired()
                        .HasColumnType("character varying(30)")
                        .HasColumnName("bot_name");

                    b.Property<string>("BotResponse")
                        .HasColumnType("text")
                        .HasColumnName("bot_response");

                    b.Property<int>("ChatEventId")
                        .HasColumnType("integer")
                        .HasColumnName("chat_event_id");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamptz")
                        .HasColumnName("datetime")
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.HasKey("Id");

                    b.HasIndex("BotName");

                    b.HasIndex("ChatEventId");

                    b.ToTable("bot_action_on_events");
                });

            modelBuilder.Entity("DataAccess.Model.BotDialog", b =>
                {
                    b.Property<int>("DialogId")
                        .HasColumnType("integer")
                        .HasColumnName("dialog_id");

                    b.Property<string>("BotName")
                        .HasColumnType("character varying(30)")
                        .HasColumnName("bot_name");

                    b.HasKey("DialogId", "BotName");

                    b.HasIndex("BotName");

                    b.ToTable("bot_dialogs");
                });

            modelBuilder.Entity("DataAccess.Model.BotTypeOfBot", b =>
                {
                    b.Property<string>("BotName")
                        .HasColumnType("character varying(30)")
                        .HasColumnName("bot_name");

                    b.Property<int>("TypeOfBotId")
                        .HasColumnType("integer")
                        .HasColumnName("type_of_bot_id");

                    b.Property<string[]>("Members")
                        .HasColumnType("varchar(20)[]")
                        .HasColumnName("members");

                    b.HasKey("BotName", "TypeOfBotId");

                    b.HasIndex("TypeOfBotId");

                    b.ToTable("bot_type_of_bots");
                });

            modelBuilder.Entity("DataAccess.Model.ChatEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("DialogId")
                        .HasColumnType("integer")
                        .HasColumnName("dialog_id");

                    b.Property<long?>("MessageId")
                        .HasColumnType("bigint")
                        .HasColumnName("message_id");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("state");

                    b.Property<int>("TypeOfActionId")
                        .HasColumnType("integer")
                        .HasColumnName("type_of_action_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.HasIndex("MessageId");

                    b.HasIndex("TypeOfActionId");

                    b.HasIndex("UserId");

                    b.ToTable("chat_events");
                });

            modelBuilder.Entity("DataAccess.Model.Dialog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("IsTeteATete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_tete_a_tete");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("dialogs");
                });

            modelBuilder.Entity("DataAccess.Model.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamptz")
                        .HasColumnName("datetime")
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<int>("UserDialogId")
                        .HasColumnType("integer")
                        .HasColumnName("user_dialog_id");

                    b.HasKey("Id");

                    b.HasIndex("UserDialogId");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("DataAccess.Model.TypeOfAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("2 ^ nextval('seq_action_types')");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("type_of_actions");
                });

            modelBuilder.Entity("DataAccess.Model.TypeOfBot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Inteface")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("interface");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("type_of_bots");
                });

            modelBuilder.Entity("DataAccess.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("login");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DataAccess.Model.UserDialog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("DialogId")
                        .HasColumnType("integer")
                        .HasColumnName("dialog_id");

                    b.Property<DateTime>("EnterDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamptz")
                        .HasColumnName("enter_date")
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("LeaveDate")
                        .HasColumnType("timestamptz")
                        .HasColumnName("leave_date");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.HasIndex("UserId", "DialogId")
                        .IsUnique();

                    b.ToTable("user_dialogs");
                });

            modelBuilder.Entity("DataAccess.Model.BotActionOnEvent", b =>
                {
                    b.HasOne("DataAccess.Model.Bot", "Bot")
                        .WithMany("BotActionOnEvents")
                        .HasForeignKey("BotName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.ChatEvent", "ChatEvent")
                        .WithMany("BotActionOnEvents")
                        .HasForeignKey("ChatEventId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Bot");

                    b.Navigation("ChatEvent");
                });

            modelBuilder.Entity("DataAccess.Model.BotDialog", b =>
                {
                    b.HasOne("DataAccess.Model.Bot", "Bot")
                        .WithMany("BotDialogs")
                        .HasForeignKey("BotName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Dialog", "Dialog")
                        .WithMany("BotDialogs")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");

                    b.Navigation("Dialog");
                });

            modelBuilder.Entity("DataAccess.Model.BotTypeOfBot", b =>
                {
                    b.HasOne("DataAccess.Model.Bot", "Bot")
                        .WithMany("BotTypes")
                        .HasForeignKey("BotName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.TypeOfBot", "TypeOfBot")
                        .WithMany("BotTypes")
                        .HasForeignKey("TypeOfBotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");

                    b.Navigation("TypeOfBot");
                });

            modelBuilder.Entity("DataAccess.Model.ChatEvent", b =>
                {
                    b.HasOne("DataAccess.Model.Dialog", "Dialog")
                        .WithMany("ChatEvents")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.Message", "Message")
                        .WithMany("ChatEvents")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DataAccess.Model.TypeOfAction", "TypeOfAction")
                        .WithMany("ChatEvents")
                        .HasForeignKey("TypeOfActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User", "User")
                        .WithMany("ChatEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dialog");

                    b.Navigation("Message");

                    b.Navigation("TypeOfAction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Model.Message", b =>
                {
                    b.HasOne("DataAccess.Model.UserDialog", "UserDialog")
                        .WithMany("Messages")
                        .HasForeignKey("UserDialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDialog");
                });

            modelBuilder.Entity("DataAccess.Model.UserDialog", b =>
                {
                    b.HasOne("DataAccess.Model.Dialog", "Dialog")
                        .WithMany("UserDialogs")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Model.User", "User")
                        .WithMany("UserDialogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dialog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Model.Bot", b =>
                {
                    b.Navigation("BotActionOnEvents");

                    b.Navigation("BotDialogs");

                    b.Navigation("BotTypes");
                });

            modelBuilder.Entity("DataAccess.Model.ChatEvent", b =>
                {
                    b.Navigation("BotActionOnEvents");
                });

            modelBuilder.Entity("DataAccess.Model.Dialog", b =>
                {
                    b.Navigation("BotDialogs");

                    b.Navigation("ChatEvents");

                    b.Navigation("UserDialogs");
                });

            modelBuilder.Entity("DataAccess.Model.Message", b =>
                {
                    b.Navigation("ChatEvents");
                });

            modelBuilder.Entity("DataAccess.Model.TypeOfAction", b =>
                {
                    b.Navigation("ChatEvents");
                });

            modelBuilder.Entity("DataAccess.Model.TypeOfBot", b =>
                {
                    b.Navigation("BotTypes");
                });

            modelBuilder.Entity("DataAccess.Model.User", b =>
                {
                    b.Navigation("ChatEvents");

                    b.Navigation("UserDialogs");
                });

            modelBuilder.Entity("DataAccess.Model.UserDialog", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
