using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Users
{
    /// <summary>
    /// Конфигурация для <see cref="StudentSurveyProgress"/>
    /// </summary>
    internal class StudentProgressConfiguration : EntityBaseConfiguration<StudentSurveyProgress>
    {
        public override void ConfigureChild(EntityTypeBuilder<StudentSurveyProgress> builder)
        {
            builder.ToTable("student_progresses", "public");

            builder.Property(p => p.CurrentPostion)
                .IsRequired();

            builder.Property(p => p.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(p => p.Student)
                .WithMany(s => s.Progresses)
                .IsRequired()
                .HasForeignKey(p => p.StudentId);

            builder.HasOne(p => p.Survey)
                .WithMany()
                .IsRequired()
                .HasForeignKey(p => p.SurveyId);

            builder.HasMany(s => s.Answers)
                .WithOne(a => a.SurveyProgress)
                .HasForeignKey(a => a.SurveyProgressId);

            builder.SetPropertyAccessModeField(p => p.Survey, StudentSurveyProgress.SurveyField);
            builder.SetPropertyAccessModeField(p => p.Student, StudentSurveyProgress.StudentField);
            builder.SetPropertyAccessModeField(p => p.Answers, StudentSurveyProgress.StudentAnswersField);

        }
    }
}
