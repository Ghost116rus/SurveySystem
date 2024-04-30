using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Organization
{
    internal class SemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.ToTable("semesters", "public")
                .HasComment("Семестры");

            builder.HasKey(s => s.Number);
            builder.HasMany(s => s.Surveys)
                .WithMany(s => s.Semesters);
        }
    }
}
