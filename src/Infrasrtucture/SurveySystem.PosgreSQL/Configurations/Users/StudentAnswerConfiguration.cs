using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Users
{
    internal class StudentAnswerConfiguration : EntityBaseConfiguration<StudentAnswer>
    {
        public override void ConfigureChild(EntityTypeBuilder<StudentAnswer> builder)
        {
            builder.ToTable("student_answers", "public")
                .HasComment("Ответы студентов");

            builder.Property(x => x.IsActual)
                .IsRequired()
                .HasComment("Актуальность ответа")
                .HasDefaultValue(true);

            builder.HasOne(a => a.SurveyProgress)
                .WithMany(s => s.Answers)
                .HasForeignKey(a => a.SurveyProgressId)
                .IsRequired();

            builder.SetPropertyAccessModeField(x => x.SurveyProgress, StudentAnswer.SurveyProgressField);
            builder.SetPropertyAccessModeField(x => x.Answer, StudentAnswer.AnswerField);
        }

    }
}
