using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.PosgreSQL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.PosgreSQL.Configurations.Users
{
    /// <summary>
    /// Конфигурация для <see cref="User"/>
    /// </summary>
    internal class UserConfiguration : EntityBaseConfiguration<User>
    {
        public override void ConfigureChild(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", "public")
                .HasComment("Пользователь");

            builder.Property(p => p.Login)
                .HasComment("Логин").IsRequired();

            builder.Property(p => p.PasswordHash)
                .HasComment("Хеш пароля").IsRequired();

            builder.Property(p => p.Role)
                .HasComment("Роль пользователя").IsRequired();

            builder.Property(p => p.FullName)
                .HasComment("Полное имя").IsRequired();

            builder.HasOne(x => x.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(x => x.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            builder.SetPropertyAccessModeField(x => x.Student, User.StudentField);
            builder.SetPropertyAccessModeField(x => x.CreatedSurveys, User.CreatedSurveysField);
            builder.SetPropertyAccessModeField(x => x.ModifiedSurveys, User.ModifiedSurveysField);
        }
    }
}
