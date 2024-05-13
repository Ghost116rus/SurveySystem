using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.PosgreSQL.Migrations
{
    public partial class AddedPositionInQuestionForAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_student_answers_survey_progress_student_survey_progress_id",
                schema: "public",
                table: "student_answers");

            migrationBuilder.DropIndex(
                name: "ix_student_answers_student_survey_progress_id",
                schema: "public",
                table: "student_answers");

            migrationBuilder.DropColumn(
                name: "student_survey_progress_id",
                schema: "public",
                table: "student_answers");

            migrationBuilder.AddColumn<int>(
                name: "position_in_question",
                schema: "public",
                table: "answers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Позиция вопроса");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "position_in_question",
                schema: "public",
                table: "answers");

            migrationBuilder.AddColumn<Guid>(
                name: "student_survey_progress_id",
                schema: "public",
                table: "student_answers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_student_answers_student_survey_progress_id",
                schema: "public",
                table: "student_answers",
                column: "student_survey_progress_id");

            migrationBuilder.AddForeignKey(
                name: "fk_student_answers_survey_progress_student_survey_progress_id",
                schema: "public",
                table: "student_answers",
                column: "student_survey_progress_id",
                principalSchema: "public",
                principalTable: "student_progresses",
                principalColumn: "id");
        }
    }
}
