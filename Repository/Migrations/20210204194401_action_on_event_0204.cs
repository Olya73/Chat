using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class action_on_event_0204 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bot_dialogs_bots_bot_id",
                table: "bot_dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_bot_type_of_bots_bots_bot_id",
                table: "bot_type_of_bots");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_events_messages_message_id",
                table: "chat_events");

            migrationBuilder.RenameColumn(
                name: "bot_id",
                table: "bot_type_of_bots",
                newName: "bot_name");

            migrationBuilder.RenameColumn(
                name: "bot_id",
                table: "bot_dialogs",
                newName: "bot_name");

            migrationBuilder.RenameIndex(
                name: "IX_bot_dialogs_bot_id",
                table: "bot_dialogs",
                newName: "IX_bot_dialogs_bot_name");

            migrationBuilder.AddColumn<DateTime>(
                name: "datetime",
                table: "bot_action_on_events",
                type: "timestamptz",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.AddForeignKey(
                name: "FK_bot_dialogs_bots_bot_name",
                table: "bot_dialogs",
                column: "bot_name",
                principalTable: "bots",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bot_type_of_bots_bots_bot_name",
                table: "bot_type_of_bots",
                column: "bot_name",
                principalTable: "bots",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_events_messages_message_id",
                table: "chat_events",
                column: "message_id",
                principalTable: "messages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bot_dialogs_bots_bot_name",
                table: "bot_dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_bot_type_of_bots_bots_bot_name",
                table: "bot_type_of_bots");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_events_messages_message_id",
                table: "chat_events");

            migrationBuilder.DropColumn(
                name: "datetime",
                table: "bot_action_on_events");

            migrationBuilder.RenameColumn(
                name: "bot_name",
                table: "bot_type_of_bots",
                newName: "bot_id");

            migrationBuilder.RenameColumn(
                name: "bot_name",
                table: "bot_dialogs",
                newName: "bot_id");

            migrationBuilder.RenameIndex(
                name: "IX_bot_dialogs_bot_name",
                table: "bot_dialogs",
                newName: "IX_bot_dialogs_bot_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bot_dialogs_bots_bot_id",
                table: "bot_dialogs",
                column: "bot_id",
                principalTable: "bots",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bot_type_of_bots_bots_bot_id",
                table: "bot_type_of_bots",
                column: "bot_id",
                principalTable: "bots",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_events_messages_message_id",
                table: "chat_events",
                column: "message_id",
                principalTable: "messages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
