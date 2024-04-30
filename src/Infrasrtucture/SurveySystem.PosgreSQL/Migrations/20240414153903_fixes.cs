using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.PosgreSQL.Migrations
{
    public partial class fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_faculty_institute_institute_id",
                schema: "public",
                table: "faculty");

            migrationBuilder.DropForeignKey(
                name: "fk_faculty_survey_faculty_faculties_id",
                schema: "public",
                table: "faculty_survey");

            migrationBuilder.DropForeignKey(
                name: "fk_institute_survey_institute_institutes_id",
                schema: "public",
                table: "institute_survey");

            migrationBuilder.DropForeignKey(
                name: "fk_semester_survey_semester_semesters_temp_id",
                schema: "public",
                table: "semester_survey");

            migrationBuilder.DropForeignKey(
                name: "fk_students_faculty_faculty_id",
                schema: "public",
                table: "students");

            migrationBuilder.AlterTable(
                name: "semesters",
                schema: "public",
                comment: "Семестры",
                oldComment: "Черты студентов");

            migrationBuilder.AlterTable(
                name: "answer_characteristics",
                schema: "public",
                comment: "Влияние ответов на характеристики",
                oldComment: "Черты студентов");

            migrationBuilder.AddForeignKey(
                name: "fk_faculty_institutes_institute_id",
                schema: "public",
                table: "faculty",
                column: "institute_id",
                principalSchema: "public",
                principalTable: "institute",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_faculty_survey_faculties_faculties_id",
                schema: "public",
                table: "faculty_survey",
                column: "faculties_id",
                principalSchema: "public",
                principalTable: "faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_institute_survey_institutes_institutes_id",
                schema: "public",
                table: "institute_survey",
                column: "institutes_id",
                principalSchema: "public",
                principalTable: "institute",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_semester_survey_semesters_semesters_temp_id",
                schema: "public",
                table: "semester_survey",
                column: "semesters_number",
                principalSchema: "public",
                principalTable: "semesters",
                principalColumn: "number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_students_faculties_faculty_id",
                schema: "public",
                table: "students",
                column: "faculty_id",
                principalSchema: "public",
                principalTable: "faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_faculty_institutes_institute_id",
                schema: "public",
                table: "faculty");

            migrationBuilder.DropForeignKey(
                name: "fk_faculty_survey_faculties_faculties_id",
                schema: "public",
                table: "faculty_survey");

            migrationBuilder.DropForeignKey(
                name: "fk_institute_survey_institutes_institutes_id",
                schema: "public",
                table: "institute_survey");

            migrationBuilder.DropForeignKey(
                name: "fk_semester_survey_semesters_semesters_temp_id",
                schema: "public",
                table: "semester_survey");

            migrationBuilder.DropForeignKey(
                name: "fk_students_faculties_faculty_id",
                schema: "public",
                table: "students");

            migrationBuilder.AlterTable(
                name: "semesters",
                schema: "public",
                comment: "Черты студентов",
                oldComment: "Семестры");

            migrationBuilder.AlterTable(
                name: "answer_characteristics",
                schema: "public",
                comment: "Черты студентов",
                oldComment: "Влияние ответов на характеристики");

            migrationBuilder.AddForeignKey(
                name: "fk_faculty_institute_institute_id",
                schema: "public",
                table: "faculty",
                column: "institute_id",
                principalSchema: "public",
                principalTable: "institute",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_faculty_survey_faculty_faculties_id",
                schema: "public",
                table: "faculty_survey",
                column: "faculties_id",
                principalSchema: "public",
                principalTable: "faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_institute_survey_institute_institutes_id",
                schema: "public",
                table: "institute_survey",
                column: "institutes_id",
                principalSchema: "public",
                principalTable: "institute",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_semester_survey_semester_semesters_temp_id",
                schema: "public",
                table: "semester_survey",
                column: "semesters_number",
                principalSchema: "public",
                principalTable: "semesters",
                principalColumn: "number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_students_faculty_faculty_id",
                schema: "public",
                table: "students",
                column: "faculty_id",
                principalSchema: "public",
                principalTable: "faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
