using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Repository.Migrations
{
    public partial class type_of_action_0122 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var seq = @"CREATE SEQUENCE seq_action_types
                            INCREMENT 1
                            START 0
                            MINVALUE 0";

            migrationBuilder.Sql(seq);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "type_of_actions",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "type_of_actions",
                type: "integer",
                nullable: false,
                defaultValueSql: "2 ^ nextval('seq_action_types')",
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "bot_response",
                table: "chat_actions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP SEQUENCE seq_action_types");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "type_of_actions",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "type_of_actions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "2 ^ nextval('seq_action_types')")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "bot_response",
                table: "chat_actions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
