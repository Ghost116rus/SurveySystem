using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Surveys
{
    internal class AnswerCharacteristicValueConfiguration : IEntityTypeConfiguration<AnswerCharacteristicValue>
    {
        public void Configure(EntityTypeBuilder<AnswerCharacteristicValue> builder)
        {
            builder.ToTable("answer_characteristics", "public")
                .HasComment("Черты студентов");


            builder.HasKey(aCh => new { aCh.AnswerId, aCh.CharacteristicId});
            builder.Property(s => s.CharacteristicId)
                .IsRequired()
                .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");
            builder.Property(s => s.AnswerId)
                .IsRequired()
                .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

            builder.Property(s => s.Value)
                .IsRequired();

            builder.HasOne(s => s.Characteristic)
                .WithMany()
                .HasForeignKey(s => s.CharacteristicId)
                .IsRequired();

            builder.HasOne(c => c.Answer)
                .WithMany(s => s.AnswerCharacteristicValues)
                .HasForeignKey(s => s.CharacteristicId)
                .IsRequired();

            builder.SetPropertyAccessModeField(s => s.Answer, AnswerCharacteristicValue.AnswerField);
            builder.SetPropertyAccessModeField(s => s.Characteristic, AnswerCharacteristicValue.CharacteristicField);
        }
    }
}
