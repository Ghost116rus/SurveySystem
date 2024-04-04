using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Users
{
    internal class StudentCharacteristicConfiguration : IEntityTypeConfiguration<StudentCharacteristic>
    {
        public void Configure(EntityTypeBuilder<StudentCharacteristic> builder)
        {
            builder.ToTable("student_characteristics", "public")
                .HasComment("Черты студентов");


            builder.HasKey(sCh => new { sCh.StudentId, sCh.CharacteristicId });
            builder.Property(s => s.CharacteristicId)
                .IsRequired()
                .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");
            builder.Property(s => s.StudentId)
                .IsRequired()
                .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

            builder.Property(s => s.Value)
                .IsRequired();

            builder.HasOne(s => s.Characteristic)
                .WithMany()
                .HasForeignKey(s => s.CharacteristicId)
                .IsRequired();

            builder.HasOne(c => c.Student)
                .WithMany(s => s.StudentCharacteristics)
                .HasForeignKey(s => s.CharacteristicId)
                .IsRequired();

            builder.SetPropertyAccessModeField(s => s.Student, StudentCharacteristic.StudentField);
            builder.SetPropertyAccessModeField(s => s.Characteristic, StudentCharacteristic.CharacteristicField);
        }
    }
}
