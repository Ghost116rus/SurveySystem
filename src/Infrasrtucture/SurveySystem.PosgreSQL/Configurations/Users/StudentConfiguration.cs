using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.PosgreSQL.Extensions;

namespace SurveySystem.PosgreSQL.Configurations.Users
{
    /// <summary>
    /// Конфигурация для <see cref="Student"/>
    /// </summary>
    internal class StudentConfiguration : EntityBaseConfiguration<Student>
    {
        public override void ConfigureChild(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students", "public")
                .HasComment("Студенты");

            builder.Property(p => p.StartDateOfLearning)
                .IsRequired()
                .HasComment("Дата начала обучения");

            builder.Property(p => p.EducationLevel)
                .IsRequired()
                .HasComment("Уровень образования");

            builder.Property(p => p.GroupNumber)
                .HasComment("Номер группы");

            builder.HasOne(s => s.User)
                .WithOne(s => s.Student)
                .HasForeignKey<Student>(s => s.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Progresses)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.StudentCharacteristics)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            builder.SetPropertyAccessModeField(x => x.User, Student.UserField);
            builder.SetPropertyAccessModeField(x => x.Faculty, Student.FacultyField);
            builder.SetPropertyAccessModeField(x => x.Progresses, Student.ProgressField);
            builder.SetPropertyAccessModeField(x => x.StudentCharacteristics, Student.SrudentCharacteristicsField);
        }
    }
}
