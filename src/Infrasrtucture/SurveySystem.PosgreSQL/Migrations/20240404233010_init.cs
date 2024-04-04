using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SurveySystem.PosgreSQL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "characteristics",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    description = table.Column<string>(type: "text", nullable: false, comment: "Описание характеристики"),
                    characteristic_type = table.Column<int>(type: "integer", nullable: false, comment: "тип характеристики"),
                    min_value = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0, comment: "Минимальное значение характеристики"),
                    max_value = table.Column<double>(type: "double precision", nullable: false, defaultValue: 1.0, comment: "Максимальное значение характеристики"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_characteristics", x => x.id);
                },
                comment: "характеристики");

            migrationBuilder.CreateTable(
                name: "institute",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    full_name = table.Column<string>(type: "text", nullable: false, comment: "Полное имя"),
                    short_name = table.Column<string>(type: "text", nullable: false, comment: "Сокращенное имя"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institute", x => x.id);
                },
                comment: "Институт");

            migrationBuilder.CreateTable(
                name: "question_criteries",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    criteria = table.Column<string>(type: "text", nullable: false, comment: "Критерий оценки вопроса"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question_criteries", x => x.id);
                },
                comment: "критерий оценки вопросов");

            migrationBuilder.CreateTable(
                name: "questions",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    type = table.Column<int>(type: "integer", nullable: false, comment: "Тип вопроса"),
                    text = table.Column<string>(type: "text", nullable: false, comment: "Текст вопроса"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_questions", x => x.id);
                },
                comment: "вопросы");

            migrationBuilder.CreateTable(
                name: "semesters",
                schema: "public",
                columns: table => new
                {
                    number = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_semesters", x => x.number);
                },
                comment: "Черты студентов");

            migrationBuilder.CreateTable(
                name: "tag",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    login = table.Column<string>(type: "text", nullable: false, comment: "Логин"),
                    password_hash = table.Column<string>(type: "text", nullable: false, comment: "Хеш пароля"),
                    full_name = table.Column<string>(type: "text", nullable: false, comment: "Полное имя"),
                    role = table.Column<int>(type: "integer", nullable: false, comment: "Роль пользователя"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                },
                comment: "Пользователь");

            migrationBuilder.CreateTable(
                name: "faculty",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    full_name = table.Column<string>(type: "text", nullable: false, comment: "Полное имя"),
                    short_name = table.Column<string>(type: "text", nullable: false, comment: "Сокращенное имя"),
                    institute_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор института"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculty", x => x.id);
                    table.ForeignKey(
                        name: "fk_faculty_institute_institute_id",
                        column: x => x.institute_id,
                        principalSchema: "public",
                        principalTable: "institute",
                        principalColumn: "id");
                },
                comment: "Кафедра");

            migrationBuilder.CreateTable(
                name: "answers",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    question_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false, comment: "Текст вопроса"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_answers_questions_question_id",
                        column: x => x.question_id,
                        principalSchema: "public",
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Ответы");

            migrationBuilder.CreateTable(
                name: "question_question_evaluation_criteria",
                schema: "public",
                columns: table => new
                {
                    criteries_id = table.Column<Guid>(type: "uuid", nullable: false),
                    questions_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question_question_evaluation_criteria", x => new { x.criteries_id, x.questions_id });
                    table.ForeignKey(
                        name: "fk_question_question_evaluation_criteria_question_evaluation_c",
                        column: x => x.criteries_id,
                        principalSchema: "public",
                        principalTable: "question_criteries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_question_question_evaluation_criteria_questions_questions_id",
                        column: x => x.questions_id,
                        principalSchema: "public",
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question_tag",
                schema: "public",
                columns: table => new
                {
                    questions_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question_tag", x => new { x.questions_id, x.tags_id });
                    table.ForeignKey(
                        name: "fk_question_tag_questions_questions_id",
                        column: x => x.questions_id,
                        principalSchema: "public",
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_question_tag_tag_tags_id",
                        column: x => x.tags_id,
                        principalSchema: "public",
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "surveys",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название опроса"),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_repetable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_visible = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_surveys", x => x.id);
                    table.ForeignKey(
                        name: "fk_surveys_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_surveys_users_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                },
                comment: "Опросы");

            migrationBuilder.CreateTable(
                name: "students",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    start_date_of_learning = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата начала обучения"),
                    education_level = table.Column<int>(type: "integer", nullable: false, comment: "Уровень образования"),
                    group_number = table.Column<string>(type: "text", nullable: true, comment: "Номер группы"),
                    faculty_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students", x => x.id);
                    table.ForeignKey(
                        name: "fk_students_faculty_faculty_id",
                        column: x => x.faculty_id,
                        principalSchema: "public",
                        principalTable: "faculty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_students_users_user_id",
                        column: x => x.id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Студенты");

            migrationBuilder.CreateTable(
                name: "answer_characteristics",
                schema: "public",
                columns: table => new
                {
                    answer_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    characteristic_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_answer_characteristics", x => new { x.answer_id, x.characteristic_id });
                    table.ForeignKey(
                        name: "fk_answer_characteristics_answers_answer_id",
                        column: x => x.answer_id,
                        principalSchema: "public",
                        principalTable: "answers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_answer_characteristics_characteristics_characteristic_id",
                        column: x => x.characteristic_id,
                        principalSchema: "public",
                        principalTable: "characteristics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Черты студентов");

            migrationBuilder.CreateTable(
                name: "faculty_survey",
                schema: "public",
                columns: table => new
                {
                    faculties_id = table.Column<Guid>(type: "uuid", nullable: false),
                    surveys_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculty_survey", x => new { x.faculties_id, x.surveys_id });
                    table.ForeignKey(
                        name: "fk_faculty_survey_faculty_faculties_id",
                        column: x => x.faculties_id,
                        principalSchema: "public",
                        principalTable: "faculty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_faculty_survey_surveys_surveys_id",
                        column: x => x.surveys_id,
                        principalSchema: "public",
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "institute_survey",
                schema: "public",
                columns: table => new
                {
                    institutes_id = table.Column<Guid>(type: "uuid", nullable: false),
                    surveys_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institute_survey", x => new { x.institutes_id, x.surveys_id });
                    table.ForeignKey(
                        name: "fk_institute_survey_institute_institutes_id",
                        column: x => x.institutes_id,
                        principalSchema: "public",
                        principalTable: "institute",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_institute_survey_surveys_surveys_id",
                        column: x => x.surveys_id,
                        principalSchema: "public",
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "semester_survey",
                schema: "public",
                columns: table => new
                {
                    semesters_number = table.Column<int>(type: "integer", nullable: false),
                    surveys_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_semester_survey", x => new { x.semesters_number, x.surveys_id });
                    table.ForeignKey(
                        name: "fk_semester_survey_semester_semesters_temp_id",
                        column: x => x.semesters_number,
                        principalSchema: "public",
                        principalTable: "semesters",
                        principalColumn: "number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_semester_survey_surveys_surveys_id",
                        column: x => x.surveys_id,
                        principalSchema: "public",
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey_questions",
                schema: "public",
                columns: table => new
                {
                    survey_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    question_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_survey_questions", x => new { x.survey_id, x.question_id });
                    table.ForeignKey(
                        name: "fk_survey_questions_questions_question_id",
                        column: x => x.question_id,
                        principalSchema: "public",
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_survey_questions_surveys_survey_id",
                        column: x => x.survey_id,
                        principalSchema: "public",
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "вопросы опроса");

            migrationBuilder.CreateTable(
                name: "survey_tag",
                schema: "public",
                columns: table => new
                {
                    surveys_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_survey_tag", x => new { x.surveys_id, x.tags_id });
                    table.ForeignKey(
                        name: "fk_survey_tag_surveys_surveys_id",
                        column: x => x.surveys_id,
                        principalSchema: "public",
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_survey_tag_tag_tags_id",
                        column: x => x.tags_id,
                        principalSchema: "public",
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_characteristics",
                schema: "public",
                columns: table => new
                {
                    student_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    characteristic_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student_characteristics", x => new { x.student_id, x.characteristic_id });
                    table.ForeignKey(
                        name: "fk_student_characteristics_characteristics_characteristic_id",
                        column: x => x.characteristic_id,
                        principalSchema: "public",
                        principalTable: "characteristics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_student_characteristics_students_student_id",
                        column: x => x.student_id,
                        principalSchema: "public",
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Черты студентов");

            migrationBuilder.CreateTable(
                name: "student_progresses",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    current_postion = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    student_id = table.Column<Guid>(type: "uuid", nullable: false),
                    survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student_progresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_progresses_students_student_id",
                        column: x => x.student_id,
                        principalSchema: "public",
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_student_progresses_surveys_survey_id",
                        column: x => x.survey_id,
                        principalSchema: "public",
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_answers",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    survey_progress_id = table.Column<Guid>(type: "uuid", nullable: false),
                    answer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_actual = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "Актуальность ответа"),
                    student_survey_progress_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_student_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_answers_answers_answer_id",
                        column: x => x.answer_id,
                        principalSchema: "public",
                        principalTable: "answers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_student_answers_survey_progress_student_survey_progress_id",
                        column: x => x.student_survey_progress_id,
                        principalSchema: "public",
                        principalTable: "student_progresses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_student_answers_survey_progress_survey_progress_id",
                        column: x => x.survey_progress_id,
                        principalSchema: "public",
                        principalTable: "student_progresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Ответы студентов");

            migrationBuilder.CreateIndex(
                name: "ix_answer_characteristics_characteristic_id",
                schema: "public",
                table: "answer_characteristics",
                column: "characteristic_id");

            migrationBuilder.CreateIndex(
                name: "ix_answers_question_id",
                schema: "public",
                table: "answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_faculty_institute_id",
                schema: "public",
                table: "faculty",
                column: "institute_id");

            migrationBuilder.CreateIndex(
                name: "ix_faculty_survey_surveys_id",
                schema: "public",
                table: "faculty_survey",
                column: "surveys_id");

            migrationBuilder.CreateIndex(
                name: "ix_institute_survey_surveys_id",
                schema: "public",
                table: "institute_survey",
                column: "surveys_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_question_evaluation_criteria_questions_id",
                schema: "public",
                table: "question_question_evaluation_criteria",
                column: "questions_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_tag_tags_id",
                schema: "public",
                table: "question_tag",
                column: "tags_id");

            migrationBuilder.CreateIndex(
                name: "ix_semester_survey_surveys_id",
                schema: "public",
                table: "semester_survey",
                column: "surveys_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_answers_answer_id",
                schema: "public",
                table: "student_answers",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_answers_student_survey_progress_id",
                schema: "public",
                table: "student_answers",
                column: "student_survey_progress_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_answers_survey_progress_id",
                schema: "public",
                table: "student_answers",
                column: "survey_progress_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_characteristics_characteristic_id",
                schema: "public",
                table: "student_characteristics",
                column: "characteristic_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_progresses_student_id",
                schema: "public",
                table: "student_progresses",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_progresses_survey_id",
                schema: "public",
                table: "student_progresses",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_faculty_id",
                schema: "public",
                table: "students",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_questions_question_id",
                schema: "public",
                table: "survey_questions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_tag_tags_id",
                schema: "public",
                table: "survey_tag",
                column: "tags_id");

            migrationBuilder.CreateIndex(
                name: "ix_surveys_created_by_user_id",
                schema: "public",
                table: "surveys",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_surveys_modified_by_user_id",
                schema: "public",
                table: "surveys",
                column: "modified_by_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answer_characteristics",
                schema: "public");

            migrationBuilder.DropTable(
                name: "faculty_survey",
                schema: "public");

            migrationBuilder.DropTable(
                name: "institute_survey",
                schema: "public");

            migrationBuilder.DropTable(
                name: "question_question_evaluation_criteria",
                schema: "public");

            migrationBuilder.DropTable(
                name: "question_tag",
                schema: "public");

            migrationBuilder.DropTable(
                name: "semester_survey",
                schema: "public");

            migrationBuilder.DropTable(
                name: "student_answers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "student_characteristics",
                schema: "public");

            migrationBuilder.DropTable(
                name: "survey_questions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "survey_tag",
                schema: "public");

            migrationBuilder.DropTable(
                name: "question_criteries",
                schema: "public");

            migrationBuilder.DropTable(
                name: "semesters",
                schema: "public");

            migrationBuilder.DropTable(
                name: "answers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "student_progresses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "characteristics",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tag",
                schema: "public");

            migrationBuilder.DropTable(
                name: "questions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "students",
                schema: "public");

            migrationBuilder.DropTable(
                name: "surveys",
                schema: "public");

            migrationBuilder.DropTable(
                name: "faculty",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "institute",
                schema: "public");
        }
    }
}
