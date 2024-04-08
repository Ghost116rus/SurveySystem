using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Surveys
{
    internal class SurveyTestQuestionConfiguration : IEntityTypeConfiguration<SurveyTestQuestion>
    {
        public void Configure(EntityTypeBuilder<SurveyTestQuestion> builder)
        {
            builder.ToTable("survey_questions", "public")
                .HasComment("вопросы опроса");

            builder.HasKey(sTq => new { sTq.SurveyId, sTq.QuestionId });
            builder.Property(s => s.SurveyId)
                .IsRequired()
                .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");
            builder.Property(s => s.QuestionId)
                .IsRequired()
                .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

            builder.Property(s => s.Position)
                .IsRequired();


            //builder.HasOne(s => s.Survey)
            //    .WithMany()
            //    .HasForeignKey(s => s.SurveyId)
            //    .IsRequired();

            //builder.HasOne(c => c.Question)
            //    .WithMany()
            //    .HasForeignKey(s => s.QuestionId)
            //    .IsRequired();

            builder.SetPropertyAccessModeField(s => s.Survey, SurveyTestQuestion.SurveyField);
            builder.SetPropertyAccessModeField(s => s.QuestionId, SurveyTestQuestion.QuestionField);
        }
    }
}
