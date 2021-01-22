using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Repository.Migrations
{
    public partial class Bot_0121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bots",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    implementation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bots", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "type_of_actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_of_actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "type_of_bots",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    @interface = table.Column<string>(name: "interface", type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_of_bots", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bot_dialogs",
                columns: table => new
                {
                    bot_id = table.Column<string>(type: "character varying(30)", nullable: false),
                    dialog_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bot_dialogs", x => new { x.dialog_id, x.bot_id });
                    table.ForeignKey(
                        name: "FK_bot_dialogs_bots_bot_id",
                        column: x => x.bot_id,
                        principalTable: "bots",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bot_dialogs_dialogs_dialog_id",
                        column: x => x.dialog_id,
                        principalTable: "dialogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_actions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_of_action_id = table.Column<int>(type: "integer", nullable: false),
                    dialog_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    bot_name = table.Column<string>(type: "character varying(30)", nullable: false),
                    bot_response = table.Column<string>(type: "text", nullable: false),
                    message_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_actions", x => x.id);
                    table.ForeignKey(
                        name: "FK_chat_actions_bots_bot_name",
                        column: x => x.bot_name,
                        principalTable: "bots",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_actions_dialogs_dialog_id",
                        column: x => x.dialog_id,
                        principalTable: "dialogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_actions_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_actions_type_of_actions_type_of_action_id",
                        column: x => x.type_of_action_id,
                        principalTable: "type_of_actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_actions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bot_type_of_bots",
                columns: table => new
                {
                    bot_id = table.Column<string>(type: "character varying(30)", nullable: false),
                    type_of_bot_id = table.Column<int>(type: "integer", nullable: false),
                    members = table.Column<string[]>(type: "varchar(20)[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bot_type_of_bots", x => new { x.bot_id, x.type_of_bot_id });
                    table.ForeignKey(
                        name: "FK_bot_type_of_bots_bots_bot_id",
                        column: x => x.bot_id,
                        principalTable: "bots",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bot_type_of_bots_type_of_bots_type_of_bot_id",
                        column: x => x.type_of_bot_id,
                        principalTable: "type_of_bots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bot_dialogs_bot_id",
                table: "bot_dialogs",
                column: "bot_id");

            migrationBuilder.CreateIndex(
                name: "IX_bot_type_of_bots_type_of_bot_id",
                table: "bot_type_of_bots",
                column: "type_of_bot_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_actions_bot_name",
                table: "chat_actions",
                column: "bot_name");

            migrationBuilder.CreateIndex(
                name: "IX_chat_actions_dialog_id",
                table: "chat_actions",
                column: "dialog_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_actions_message_id",
                table: "chat_actions",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_actions_type_of_action_id",
                table: "chat_actions",
                column: "type_of_action_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_actions_user_id",
                table: "chat_actions",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bot_dialogs");

            migrationBuilder.DropTable(
                name: "bot_type_of_bots");

            migrationBuilder.DropTable(
                name: "chat_actions");

            migrationBuilder.DropTable(
                name: "type_of_bots");

            migrationBuilder.DropTable(
                name: "bots");

            migrationBuilder.DropTable(
                name: "type_of_actions");
        }
    }
}
