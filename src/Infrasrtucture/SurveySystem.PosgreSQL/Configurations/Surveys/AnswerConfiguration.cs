using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.PosgreSQL.Extensions;


namespace SurveySystem.PosgreSQL.Configurations.Surveys
{
    internal class AnswerConfiguration : EntityBaseConfiguration<Answer>
    {
        public override void ConfigureChild(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("answers", "public")
                .HasComment("Ответы");

            builder.Property(a => a.Text)
                .IsRequired()
                .HasComment("Текст ответа");

            builder.Property(a => a.PositionInQuestion)
                .IsRequired()
                .HasComment("Позиция вопроса");

            builder.HasMany(a => a.AnswerCharacteristicValues)
                .WithOne(ch => ch.Answer)
                .HasForeignKey(ch => ch.AnswerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.StudentAnswers)
                .WithOne(ch => ch.Answer)
                .HasForeignKey(ch => ch.AnswerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.SetPropertyAccessModeField(x => x.Question, Answer.QuestionField);
            builder.SetPropertyAccessModeField(x => x.AnswerCharacteristicValues, Answer.AnswerCharacteristicField);
            builder.SetPropertyAccessModeField(x => x.StudentAnswers, Answer.StudentAnswersField);

        }
    }
}
