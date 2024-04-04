using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Organization
{
    /// <summary>
    /// Конфигурация для <see cref="Institute"/>
    /// </summary>
    internal class InstituteConfiguration : EntityBaseConfiguration<Institute>
    {
        /// <inheritdoc/>
        public override void ConfigureChild(EntityTypeBuilder<Institute> builder)
        {
            builder.ToTable("institute", "public")
                .HasComment("Институт");

            builder.Property(p => p.FullName)
                .HasComment("Полное имя")
                .IsRequired();

            builder.Property(p => p.ShortName)
                .HasComment("Сокращенное имя")
                .IsRequired();

            builder.HasMany(x => x.Faculties)
                .WithOne(y => y!.Institute)
                .HasForeignKey(x => x.InstituteId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.SetPropertyAccessModeField(x => x.Faculties, Institute.FacultiesField);
        }
    }
}
