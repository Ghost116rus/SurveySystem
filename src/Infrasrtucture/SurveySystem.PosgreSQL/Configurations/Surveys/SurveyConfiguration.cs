using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Surveys
{
    internal class SurveyConfiguration : EntityBaseConfiguration<Survey>
    {
        public override void ConfigureChild(EntityTypeBuilder<Survey> builder)
        {
            builder.ToTable("surveys", "public")
                .HasComment("Опросы");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasComment("Название опроса");

            builder.Property(s => s.StartDate);

            builder.Property(s => s.IsRepetable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.IsVisible)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasMany(x => x.Tags)
                .WithMany(s => s.Surveys);

            builder.HasMany(x => x.Semesters)
                .WithMany(s => s.Surveys);

            builder.HasMany(x=> x.Faculties)
                .WithMany(i => i.Surveys);

            builder.HasMany(x=> x.Institutes)
                .WithMany(i => i.Surveys);

            builder.HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CreatedByUser)
                .WithMany(u => u.CreatedSurveys)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.ModifiedByUser)
                .WithMany(u => u.ModifiedSurveys)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.SetPropertyAccessModeField(x => x.Semesters, Survey.SemestersField);
            builder.SetPropertyAccessModeField(x => x.Institutes, Survey.InstitutesField);
            builder.SetPropertyAccessModeField(x => x.Faculties, Survey.FacultiesField);
            builder.SetPropertyAccessModeField(x => x.Questions, Survey.QuestionsField);
            builder.SetPropertyAccessModeField(x => x.CreatedByUser, Survey.CreatedByUserField);
            builder.SetPropertyAccessModeField(x => x.ModifiedByUser, Survey.ModifiedByUserField);
        }
    }
}
