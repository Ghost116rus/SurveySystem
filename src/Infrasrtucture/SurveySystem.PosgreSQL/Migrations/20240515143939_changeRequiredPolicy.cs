using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.PosgreSQL.Migrations
{
    public partial class changeRequiredPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_of_learning",
                schema: "public",
                table: "students",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Дата начала обучения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата начала обучения");

            migrationBuilder.AlterColumn<Guid>(
                name: "faculty_id",
                schema: "public",
                table: "students",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "education_level",
                schema: "public",
                table: "students",
                type: "integer",
                nullable: true,
                comment: "Уровень образования",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Уровень образования");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_of_learning",
                schema: "public",
                table: "students",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Дата начала обучения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Дата начала обучения");

            migrationBuilder.AlterColumn<Guid>(
                name: "faculty_id",
                schema: "public",
                table: "students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "education_level",
                schema: "public",
                table: "students",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Уровень образования",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Уровень образования");
        }
    }
}
