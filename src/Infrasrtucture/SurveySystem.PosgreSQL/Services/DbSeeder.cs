using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Exceptions;
using System.ComponentModel;

namespace SurveySystem.PosgreSQL.Services
{
    /// <summary>
    /// Сервис добавления данных в БД
    /// </summary>
    public class DbSeeder : IDbSeeder
    {
        //private static readonly Type RolesEnumType = typeof(DefaultRoles);

        private readonly IReadOnlyDictionary<Guid, string> _roles = new Dictionary<Guid, string>
        {
            //[DefaultRoles.StudentId] = GetDefaultValueDescription(nameof(DefaultRoles.StudentId), RolesEnumType),
            //[DefaultRoles.ManagerId] = GetDefaultValueDescription(nameof(DefaultRoles.ManagerId), RolesEnumType),
        };

        /// <inheritdoc/>
        public async Task SeedAsync(IDbContext dbContext, CancellationToken cancellationToken = default)
        {
            //ArgumentNullException.ThrowIfNull(dbContext);

            //await SeedRolesAsync(dbContext, cancellationToken);
            //await SeedRolesPrivilegesAsync(dbContext, cancellationToken);
            //await SeedInstitutesAndFacultiesAsync(dbContext, cancellationToken);
            //await dbContext.SaveChangesAsync(cancellationToken);
            //await SeedTestUsersAsync(dbContext, cancellationToken);
            //await dbContext.SaveChangesAsync(cancellationToken);
        }


        private async Task SeedInstitutesAndFacultiesAsync(IDbContext dbContext, CancellationToken cancellationToken)
        {
            var isExist = await dbContext.Institutes.AnyAsync(cancellationToken);

            if (!isExist)
            {
                var instituteIKTZI = new Institute("Институт компьютерных технологий и защиты информации", "ИКТЗИ");

                var facultyPMI = new Faculty("Кафедра прикладной математики и информатики", "ПМИ", instituteIKTZI);
                var facultySIB = new Faculty("Кафедра систем информационной безопасности", "СИБ", instituteIKTZI);

                await dbContext.Institutes.AddRangeAsync(instituteIKTZI);
                await dbContext.Faculties.AddRangeAsync(facultyPMI, facultySIB);
            }
        }

        private async Task SeedTestUsersAsync(IDbContext dbContext, CancellationToken cancellationToken)
        {
            //var roleManager = await dbContext.Roles
            //    .FirstOrDefaultAsync(x => x.Id == DefaultRoles.ManagerId, cancellationToken);

            //var roleStudent = await dbContext.Roles
            //    .FirstOrDefaultAsync(x => x.Id == DefaultRoles.StudentId, cancellationToken);

            //if (roleManager == null || roleStudent == null)
            //    return;

            //var passwordHashService = new PasswordEncryptionService();

            //var passwordHash = passwordHashService.EncodePassword("123456");

            //var user = new User(
            //    lastName: "Менеджеров",
            //    firstName: "Менеджер",
            //    birthday: new DateTime(2000, 12, 12),
            //    login: "manager",
            //    passwordHash: passwordHash,
            //    email: "manager@mail.ru",
            //    role: roleManager);

            //var user2 = new User(
            //    lastName: "Менеджеров2",
            //    firstName: "Менеджер2",
            //    birthday: new DateTime(2002, 12, 12),
            //    login: "manager2",
            //    passwordHash: passwordHash,
            //    email: "manager2@mail.ru",
            //    role: roleManager);

            //var user3 = new User(
            //    lastName: "Тестов",
            //    firstName: "Тест",
            //    birthday: new DateTime(2002, 12, 12),
            //    login: "test",
            //    passwordHash: passwordHash,
            //    email: "test@mail.ru",
            //    role: roleStudent);

            //if (await dbContext.Users.AnyAsync(
            //    x => x.Login == user.Login || x.Login == user2.Login || x.Login == user3.Login,
            //    cancellationToken: cancellationToken))
            //    return;

            //await dbContext.Users.AddRangeAsync(user, user2, user3);
        }

        private static string GetDefaultValueDescription(string fieldName, Type enumWithDefaultValue)
        {
            var memberInfo = enumWithDefaultValue.GetMember(fieldName)
                ?? throw new ExceptionBase("Не удалось получить свойство");

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length < 1 ? fieldName : ((DescriptionAttribute)attributes[0]).Description;

        }
    }
}
