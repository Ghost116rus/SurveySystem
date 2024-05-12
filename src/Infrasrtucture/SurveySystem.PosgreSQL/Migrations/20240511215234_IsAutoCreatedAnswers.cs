using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.PosgreSQL.Migrations
{
    public partial class IsAutoCreatedAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_auto_created_answers",
                schema: "public",
                table: "questions",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Автоматически ли сгенерированы ответы");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_auto_created_answers",
                schema: "public",
                table: "questions");
        }
    }
}
