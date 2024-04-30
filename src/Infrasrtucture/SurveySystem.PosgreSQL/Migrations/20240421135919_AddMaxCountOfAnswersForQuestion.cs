using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.PosgreSQL.Migrations
{
    public partial class AddMaxCountOfAnswersForQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_question_tag_tag_tags_id",
                schema: "public",
                table: "question_tag");

            migrationBuilder.DropForeignKey(
                name: "fk_survey_tag_tag_tags_id",
                schema: "public",
                table: "survey_tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tag",
                schema: "public",
                table: "tag");

            migrationBuilder.RenameTable(
                name: "tag",
                schema: "public",
                newName: "tags",
                newSchema: "public");

            migrationBuilder.AddColumn<int>(
                name: "max_count_of_answers",
                schema: "public",
                table: "questions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Максимальное количество ответов");

            migrationBuilder.AlterColumn<string>(
                name: "text",
                schema: "public",
                table: "answers",
                type: "text",
                nullable: false,
                comment: "Текст ответа",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Текст вопроса");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tags",
                schema: "public",
                table: "tags",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_question_tag_tags_tags_id",
                schema: "public",
                table: "question_tag",
                column: "tags_id",
                principalSchema: "public",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_survey_tag_tags_tags_id",
                schema: "public",
                table: "survey_tag",
                column: "tags_id",
                principalSchema: "public",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_question_tag_tags_tags_id",
                schema: "public",
                table: "question_tag");

            migrationBuilder.DropForeignKey(
                name: "fk_survey_tag_tags_tags_id",
                schema: "public",
                table: "survey_tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tags",
                schema: "public",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "max_count_of_answers",
                schema: "public",
                table: "questions");

            migrationBuilder.RenameTable(
                name: "tags",
                schema: "public",
                newName: "tag",
                newSchema: "public");

            migrationBuilder.AlterColumn<string>(
                name: "text",
                schema: "public",
                table: "answers",
                type: "text",
                nullable: false,
                comment: "Текст вопроса",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Текст ответа");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tag",
                schema: "public",
                table: "tag",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_question_tag_tag_tags_id",
                schema: "public",
                table: "question_tag",
                column: "tags_id",
                principalSchema: "public",
                principalTable: "tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_survey_tag_tag_tags_id",
                schema: "public",
                table: "survey_tag",
                column: "tags_id",
                principalSchema: "public",
                principalTable: "tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
