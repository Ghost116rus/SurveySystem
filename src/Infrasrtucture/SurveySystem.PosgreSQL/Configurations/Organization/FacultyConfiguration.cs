using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Organization
{
    /// <summary>
    /// Конфигурация для <see cref="Faculty"/>
    /// </summary>
    internal class FacultyConfiguration : EntityBaseConfiguration<Faculty>
    {
        /// <inheritdoc/>
        public override void ConfigureChild(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable("faculty", "public")
                .HasComment("Кафедра");

            builder.Property(p => p.FullName)
                .HasComment("Полное имя")
                .IsRequired();

            builder.Property(p => p.ShortName)
                .HasComment("Сокращенное имя")
                .IsRequired();

            builder.Property(p => p.InstituteId)
                .HasComment("Идентификатор института")
                .IsRequired();

            builder.HasMany(x => x.Students)
                .WithOne(y => y!.Faculty)
                .HasForeignKey(x => x.FacultyId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Institute)
                .WithMany(y => y!.Faculties)
                .HasForeignKey(x => x.InstituteId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.SetPropertyAccessModeField(x => x.Institute, Faculty.InstituteField);
            builder.SetPropertyAccessModeField(x => x.Students, Faculty.StudentsField);
        }
    }
}
