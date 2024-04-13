using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Constants;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using System.ComponentModel;

namespace SurveySystem.PosgreSQL.Services
{
    /// <summary>
    /// Сервис добавления данных в БД
    /// </summary>
    public class DbSeeder : IDbSeeder
    {
        private IDbContext _dbContext;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public DbSeeder(IDbContext dbContext, IPasswordEncryptionService passwordEncryptionService)
        {
            _dbContext = dbContext; 
            _passwordEncryptionService = passwordEncryptionService;
        }

        /// <inheritdoc/>
        public async Task SeedAsync(IDbContext dbContext, CancellationToken cancellationToken = default)
        {
            _dbContext = dbContext;          
            ArgumentNullException.ThrowIfNull(_dbContext);

            await SeedSemesters(cancellationToken);
            await SeedInstitutesAndFacultiesAsync(cancellationToken);
            await SeedCharacteristics(cancellationToken);
            await SeedBasicUsersAsync(cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await SeedTestStudentCharacteristcs(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        #region seed Organization

        private async Task SeedInstitutesAndFacultiesAsync(CancellationToken cancellationToken)
        {
            if (await _dbContext.Institutes.AnyAsync(cancellationToken))
                return;

            var instituteIKTZI = new Institute("Институт компьютерных технологий и защиты информации", "ИКТЗИ");

            var facultyPMI = new Faculty("Кафедра прикладной математики и информатики", "ПМИ", instituteIKTZI);
            var facultySIB = new Faculty("Кафедра систем информационной безопасности", "СИБ", instituteIKTZI);

            await _dbContext.Institutes.AddRangeAsync(instituteIKTZI);
            await _dbContext.Faculties.AddRangeAsync(facultyPMI, facultySIB);
        }

        private async Task SeedSemesters(CancellationToken cancellationToken)
        {
            if (await _dbContext.Semesters.AnyAsync(cancellationToken))
                return;

            List<Semester> semsters = new();
            for (int i = 0; i < DomainConstants.SemesterConstant; i++)
            {
                var semester = new Semester() { Number = i + 1 };
                semsters.Add(semester);
            }
            await _dbContext.Semesters.AddRangeAsync(semsters);
        }

        #endregion

        #region seed Survey

        private async Task SeedCharacteristics(CancellationToken cancellationToken)
        {
            if (await _dbContext.Characteristics.AnyAsync(cancellationToken))
                return;

            var char1 = new Characteristic("Умение работать в команде", CharacteristicType.Peculiarity, -1, 1);
            var char2 = new Characteristic("Целеустремленность", CharacteristicType.Peculiarity, -1, 1);

            var subj1 = new Characteristic("C#", CharacteristicType.Subject, 0, 1);
            var subj2 = new Characteristic("Физика", CharacteristicType.Subject, 0, 1);

            await _dbContext.Characteristics.AddRangeAsync(char1, char2, subj1, subj2);
        }

        private async Task SeedTestStudentCharacteristcs(CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students.Include(s => s.User)
                .Include(s => s.StudentCharacteristics)
                .FirstOrDefaultAsync(x => x.User != null && x.User.Login == "student");

            if (student == null || student.StudentCharacteristics.Count != 0) 
                return;

            var characteristics = await _dbContext.Characteristics.Where(x => 
                x.Description == "Умение работать в команде" ||
                x.Description == "Целеустремленность" ||
                x.Description == "C#" ||
                x.Description == "Физика").ToListAsync();

            List<StudentCharacteristic> studentCharacteristics = new List<StudentCharacteristic>();    

            foreach ( var characteristic in characteristics) 
            {
                var studentChar = new StudentCharacteristic(student, characteristic) { Value = characteristic.MaxValue };
                studentCharacteristics.Add(studentChar);
            }
            await _dbContext.StudentCharacteristic.AddRangeAsync(studentCharacteristics);
        }

        #endregion

        private async Task SeedBasicUsersAsync(CancellationToken cancellationToken)
        {
            var adminHash = _passwordEncryptionService.EncodePassword("admin");
            var studentHash = _passwordEncryptionService.EncodePassword("777");
            var userHash = _passwordEncryptionService.EncodePassword("test");

            var admin = new User(
                fullName: "Админ",
                login: "admin",
                passwordHash: adminHash,
                role: Role.Administrator);

            var studentUser = new User(
                fullName: "Студентов Студент Студентович",
                login: "student",
                passwordHash: studentHash,
                role: Role.Student);

            var user3 = new User(
                fullName: "Тестов Тест Тестович",
                login: "test",
                passwordHash: userHash,
                role: Role.Observer);

            if (await _dbContext.Users.AnyAsync(
                x => x.Login == admin.Login || x.Login == studentUser.Login || x.Login == user3.Login,
                cancellationToken: cancellationToken))
                return;

            await _dbContext.Users.AddRangeAsync(admin, studentUser, user3);

            if (await _dbContext.Students.AnyAsync())
                return;

            var faculty = await _dbContext.Faculties.FirstOrDefaultAsync(x => x.ShortName == "ПМИ");
            if (faculty == null)
                return;

            var student = new Student(studentUser, EducationLevel.Bachelor, "4411", faculty, new DateTime(2020, 9, 1));

            await _dbContext.Students.AddAsync(student);
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
