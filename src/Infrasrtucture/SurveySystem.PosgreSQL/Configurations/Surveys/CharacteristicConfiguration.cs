using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.PosgreSQL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.PosgreSQL.Configurations.Surveys
{
    internal class CharacteristicConfiguration : EntityBaseConfiguration<Characteristic>
    {
        public override void ConfigureChild(EntityTypeBuilder<Characteristic> builder)
        {
            builder.ToTable("characteristics", "public")
                .HasComment("характеристики");

            builder.Property(q => q.Description)
                .IsRequired()
                .HasComment("Описание характеристики");

            builder.Property(q => q.CharacteristicType)
                .IsRequired()
                .HasComment("тип характеристики");

            builder.Property(q => q.MinValue)
                .IsRequired()
                .HasDefaultValue(0)
                .HasComment("Минимальное значение характеристики");

            builder.Property(q => q.MaxValue)
                .IsRequired()
                .HasDefaultValue(1)
                .HasComment("Максимальное значение характеристики");
        }
    }
}
