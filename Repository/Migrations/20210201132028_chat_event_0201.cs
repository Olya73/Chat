using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Repository.Migrations
{
    public partial class chat_event_0201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_actions");

            migrationBuilder.CreateTable(
                name: "chat_events",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_of_action_id = table.Column<int>(type: "integer", nullable: false),
                    dialog_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    message_id = table.Column<long>(type: "bigint", nullable: true),
                    state = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_chat_events_dialogs_dialog_id",
                        column: x => x.dialog_id,
                        principalTable: "dialogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_events_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chat_events_type_of_actions_type_of_action_id",
                        column: x => x.type_of_action_id,
                        principalTable: "type_of_actions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_events_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bot_action_on_events",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chat_event_id = table.Column<int>(type: "integer", nullable: false),
                    bot_name = table.Column<string>(type: "character varying(30)", nullable: false),
                    bot_response = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bot_action_on_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_bot_action_on_events_bots_bot_name",
                        column: x => x.bot_name,
                        principalTable: "bots",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bot_action_on_events_chat_events_chat_event_id",
                        column: x => x.chat_event_id,
                        principalTable: "chat_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bot_action_on_events_bot_name",
                table: "bot_action_on_events",
                column: "bot_name");

            migrationBuilder.CreateIndex(
                name: "IX_bot_action_on_events_chat_event_id",
                table: "bot_action_on_events",
                column: "chat_event_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_events_dialog_id",
                table: "chat_events",
                column: "dialog_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_events_message_id",
                table: "chat_events",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_events_type_of_action_id",
                table: "chat_events",
                column: "type_of_action_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_events_user_id",
                table: "chat_events",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bot_action_on_events");

            migrationBuilder.DropTable(
                name: "chat_events");

            migrationBuilder.CreateTable(
                name: "chat_actions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bot_name = table.Column<string>(type: "character varying(30)", nullable: false),
                    bot_response = table.Column<string>(type: "text", nullable: true),
                    dialog_id = table.Column<int>(type: "integer", nullable: false),
                    message_id = table.Column<long>(type: "bigint", nullable: true),
                    type_of_action_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_actions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
    }
}
