using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Surveys
{
    internal class QuestionConfiguration : EntityBaseConfiguration<Question>
    {
        public override void ConfigureChild(EntityTypeBuilder<Question> builder)
        {
            //base.ConfigureChild(builder);

            builder.ToTable("questions", "public")
                .HasComment("вопросы");

            builder.Property(q => q.Type)
                .IsRequired()
                .HasComment("Тип вопроса");

            builder.Property(q => q.Text)
                .IsRequired()
                .HasComment("Текст вопроса");

            builder.HasMany(x => x.Tags)
                .WithMany(t => t.Questions);

            builder.HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.Criteries)
                .WithMany(c => c.Questions);

            builder.SetPropertyAccessModeField(x => x.Tags, Question.TagsField);
            builder.SetPropertyAccessModeField(x => x.Answers, Question.AnswersField);
            builder.SetPropertyAccessModeField(x => x.Criteries, Question.CriteriesField);

        }
    }
}
