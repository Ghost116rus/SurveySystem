using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.PosgreSQL.Migrations
{
    public partial class addPostiveAndNegativeDescriptionsForCharacteristics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                schema: "public",
                table: "characteristics");

            migrationBuilder.AddColumn<string>(
                name: "negative_description",
                schema: "public",
                table: "characteristics",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Отрицательное описание характеристики");

            migrationBuilder.AddColumn<string>(
                name: "positive_description",
                schema: "public",
                table: "characteristics",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Положительное описание характеристики");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "negative_description",
                schema: "public",
                table: "characteristics");

            migrationBuilder.DropColumn(
                name: "positive_description",
                schema: "public",
                table: "characteristics");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "public",
                table: "characteristics",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Описание характеристики");
        }
    }
}
