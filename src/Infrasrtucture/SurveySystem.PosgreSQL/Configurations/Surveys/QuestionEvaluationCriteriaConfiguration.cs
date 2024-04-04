using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Surveys
{
    internal class QuestionEvaluationCriteriaConfiguration : EntityBaseConfiguration<QuestionEvaluationCriteria>
    {
        public override void ConfigureChild(EntityTypeBuilder<QuestionEvaluationCriteria> builder)
        {
            builder.ToTable("question_criteries", "public")
                .HasComment("критерий оценки вопросов");

            builder.Property(q => q.Criteria)
                .IsRequired()
                .HasComment("Критерий оценки вопроса");

            builder.HasMany(q => q.Questions)
                .WithMany(c => c.Criteries);

            builder.SetPropertyAccessModeField(x => x.Questions, QuestionEvaluationCriteria.QuestionsField);

        }
    }
}
