using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Constants;
using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Domain.Interfaces;
using System.ComponentModel;


namespace SurveySystem.PosgreSQL.Services
{
    /// <summary>
    /// Сервис добавления данных в БД
    /// </summary>
    public class DbSeeder : IDbSeeder
    {
        private IDbContext _dbContext;
        private readonly IAnswersService _answersService;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public DbSeeder(IDbContext dbContext, IAnswersService answersService, IPasswordEncryptionService passwordEncryptionService)
        {
            _dbContext = dbContext;
            _answersService = answersService; 
            _passwordEncryptionService = passwordEncryptionService;
        }

        private List<Tag> basicTags;
        private List<Characteristic> basicCharacteristics;
        private List<Characteristic> basicSubjects;

        /// <inheritdoc/>
        public async Task SeedAsync(IDbContext dbContext, CancellationToken cancellationToken = default)
        {
            _dbContext = dbContext;          
            ArgumentNullException.ThrowIfNull(_dbContext);

            await SeedTags(cancellationToken);
            await SeedSemesters(cancellationToken);
            await SeedInstitutesAndFacultiesAsync(cancellationToken);
            await SeedCharacteristics(cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await SeedBasicUsersAsync(cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await SeedTestStudentCharacteristcs(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await SeedTestSurvey(cancellationToken);
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

        #region seed Surveys

        private async Task SeedTags(CancellationToken cancellationToken)
        {
            if (await _dbContext.Tags.AnyAsync(cancellationToken))
                return;

            basicTags = new List<Tag>
            {
                new Tag("Базовые теги", TagType.All),
                new Tag("Базовые тег для всех тестовых данных", TagType.All),
                new Tag("Интерес к образованию", TagType.ForQuestions),
                new Tag("Личность", TagType.All),
                new Tag("Опрос для первых пользователей", TagType.ForSurveys),
            };
        }

        private async Task SeedTestSurvey(CancellationToken cancellationToken)
        {
            if (basicCharacteristics == null)
                return;

            var greeting = new Question(
                    questionType: QuestionType.Information,
                    text: "   Приветствую тебя в нашем новом приложении.\r\nЯ твой персональный ассистент, что поможет тебе лучше познать себя. " +
                    "Позволь мне задать тебе несколько вопросов, это не займет много времени.\r\nОтвечай честно, это опрос не для кого-то, а для тебя.\r\n",
                    1, null
                    );
            var final = new Question(
                    questionType: QuestionType.Information,
                    text: "Вот и всё закончилось, поздравляю тебя. Если хочешь посмотреть, что я узнал о тебе внизу будет кнопка перейти в визуализацию твоего " +
                    "цифрового профиля (также она есть слева “Цифровой профиль”). Если захочешь пройти знакомство заново, можешь нажать кнопку ниже, " +
                    "либо во вкладке “Опросы”, в верхнем левом углу",
                    1, null
                    );

            var questions = new List<Question>()
            {
                new Question(QuestionType.Alternative, "Я с удовольствием иду в университет", 1, basicTags, true),
                new Question(QuestionType.Alternative, "Я люблю задавать вопросы на лекции", 1, basicTags, true),
                new Question(QuestionType.Alternative, "Полученные знания важнее оценок", 1, basicTags, true),
                new Question(QuestionType.Alternative, "Мне нравится моя будущая профессия", 1, basicTags, true),
                new Question(QuestionType.Alternative, "Я и сейчас уверен в правильности выбора профессии", 1, basicTags, true),
                new Question(QuestionType.NonAlternative, "Какие предметы вам нравятся больше всего, выберите 3 самых любимых", 3, basicTags),
                new Question(QuestionType.NonAlternative, "Выберите что вам действительно интересно из этого", 2, null),
                new Question(QuestionType.Alternative, "Мне легко общаться с моими однокурсниками", 1, null, true),
                new Question(QuestionType.Alternative, "Я люблю помогать своим однокурсникам и если мне требуется помощь, я всегда могу к ним обраться", 1, null, true),
                new Question(QuestionType.Alternative, "Мне нравится заниматься исследованием актуальных научных проблем", 1, null, true),
                new Question(QuestionType.Alternative, "Я привык выделять в делах главное и не отвлекаться на посторонее", 1, null, true),
                new Question(QuestionType.Alternative, "Умею длительно работать с полной отдачей сил", 1, null, true),
            };

            await _dbContext.Questions.AddAsync(greeting);
            await _dbContext.Questions.AddAsync(final);
            await _dbContext.Questions.AddRangeAsync(questions);

            await _dbContext.SaveChangesAsync();

            var defaultAnswersAndCharacteristics = new List<Tuple<Characteristic, List<Answer>>>()
            {
                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[0], _answersService.GetDefaultAnswers(questions[0])),
                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[0], _answersService.GetDefaultAnswers(questions[1])),

                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[7], _answersService.GetDefaultAnswers(questions[2])),
                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[7], _answersService.GetDefaultAnswers(questions[3])),
                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[7], _answersService.GetDefaultAnswers(questions[4])),

                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[5], _answersService.GetDefaultAnswers(questions[7])),
                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[5], _answersService.GetDefaultAnswers(questions[8])),

                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[6], _answersService.GetDefaultAnswers(questions[9])),

                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[8], _answersService.GetDefaultAnswers(questions[10])),
                new Tuple<Characteristic, List<Answer>>(basicCharacteristics[8], _answersService.GetDefaultAnswers(questions[11])),
            };

            foreach (var item in defaultAnswersAndCharacteristics)            
                await _dbContext.Answers.AddRangeAsync(item.Item2);

            await _dbContext.SaveChangesAsync();

            await _dbContext.AnswerCharacteristicValues.AddRangeAsync(
                _answersService.GetDefaultAnswerCharacteristicValuesForSurvey(defaultAnswersAndCharacteristics));

            await _dbContext.SaveChangesAsync();

            var subjectAnswers = new List<Answer>()
            {
                new Answer("Физика", questions[5]),
                new Answer("Информатика", questions[5]),
                new Answer("Философия", questions[5]),
                new Answer("Дизайн", questions[5]),
                new Answer("Русский язык", questions[5]),
                new Answer("Английский язык", questions[5]),
                new Answer("Программирование C#", questions[5]),
                new Answer("Математика", questions[5]),
            };
            var subjectsAnswersCharacteristics = new List<AnswerCharacteristicValue>()
            {
                // Физика, Аналитический склад
                new AnswerCharacteristicValue(subjectAnswers[0], basicSubjects[1], 1),                
                new AnswerCharacteristicValue(subjectAnswers[0], basicCharacteristics[1], 0.5),

                // Информатика, Аналитический склад
                new AnswerCharacteristicValue(subjectAnswers[1], basicSubjects[3], 1),
                new AnswerCharacteristicValue(subjectAnswers[1], basicCharacteristics[1], 0.5),
                
                // Философия, Гуманитарный склад
                new AnswerCharacteristicValue(subjectAnswers[2], basicSubjects[5], 1),
                new AnswerCharacteristicValue(subjectAnswers[2], basicCharacteristics[2], 0.5),
                                
                // Дизайн, Гуманитарный склад
                new AnswerCharacteristicValue(subjectAnswers[3], basicSubjects[8], 1),
                new AnswerCharacteristicValue(subjectAnswers[3], basicCharacteristics[2], 0.5),

                // Русский язык, Гуманитарный склад
                new AnswerCharacteristicValue(subjectAnswers[4], basicSubjects[7], 1),
                new AnswerCharacteristicValue(subjectAnswers[4], basicCharacteristics[2], 0.5),
                                
                // Английский язык, Гуманитарный склад
                new AnswerCharacteristicValue(subjectAnswers[5], basicSubjects[6], 1),
                new AnswerCharacteristicValue(subjectAnswers[5], basicCharacteristics[2], 0.5),
                
                // C#б программирование, Аналитический склад
                new AnswerCharacteristicValue(subjectAnswers[6], basicSubjects[0], 1),
                new AnswerCharacteristicValue(subjectAnswers[6], basicSubjects[4], 1),
                new AnswerCharacteristicValue(subjectAnswers[6], basicCharacteristics[1], 0.5),

                // Математика, Аналитический склад
                new AnswerCharacteristicValue(subjectAnswers[7], basicSubjects[2], 1),
                new AnswerCharacteristicValue(subjectAnswers[7], basicCharacteristics[1], 0.5),
            };

            var actionAnswers = new List<Answer>()
            {
                new Answer("Проектировать дизайн приложения", questions[6]),
                new Answer("Проектировать логику приложения", questions[6]),
            };
            var actionCharacteristics = new List<AnswerCharacteristicValue>()
            {
                new AnswerCharacteristicValue(actionAnswers[0], basicCharacteristics[4], 1),
                new AnswerCharacteristicValue(actionAnswers[1], basicCharacteristics[3], 1)
            };

            await _dbContext.Answers.AddRangeAsync(subjectAnswers);
            await _dbContext.Answers.AddRangeAsync(actionAnswers);

            await _dbContext.SaveChangesAsync();

            await _dbContext.AnswerCharacteristicValues.AddRangeAsync(subjectsAnswersCharacteristics);
            await _dbContext.AnswerCharacteristicValues.AddRangeAsync(actionCharacteristics);

            var testSurvey = new Survey("Ознакомительный опрос", null, true, true, null, null, null, basicTags);
            var surveyQuestions = new List<SurveyTestQuestion>()
            {
                new SurveyTestQuestion(0, testSurvey, greeting)
            };
            int position = 1;
            surveyQuestions.AddRange(questions.Select(q => new SurveyTestQuestion(position++, testSurvey, q)));
            surveyQuestions.Add(new SurveyTestQuestion(position, testSurvey, final));

            testSurvey.UpdateSurveyQuestions(surveyQuestions);

            var secondSurvey = new Survey("Обучающий опрос", null, false, true, null, null, null, new List<Tag>() { basicTags[0]});
            secondSurvey.UpdateSurveyQuestions(new List<SurveyTestQuestion>()
            {
                new SurveyTestQuestion(0, secondSurvey, greeting),
                new SurveyTestQuestion(1, secondSurvey, questions[0]),
                new SurveyTestQuestion(2, secondSurvey, questions[5]),
                new SurveyTestQuestion(3, secondSurvey, questions[2]),
                new SurveyTestQuestion(4, secondSurvey, final),
            });

            await _dbContext.Surveys.AddAsync(testSurvey);
            await _dbContext.Surveys.AddAsync(secondSurvey);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var student = await _dbContext.Students.Include(s => s.User)
                .FirstOrDefaultAsync(x => x.User != null && x.User.Login == "student");

            if (student == null)
                return;

            var studentProgress1 = new StudentSurveyProgress(student, testSurvey);
            var studentProgress2 = new StudentSurveyProgress(student, secondSurvey);

            await _dbContext.SurveyProgress.AddAsync(studentProgress1);
            await _dbContext.SurveyProgress.AddAsync(studentProgress2);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }


        private async Task SeedCharacteristics(CancellationToken cancellationToken)
        {
            if (await _dbContext.Characteristics.AnyAsync(cancellationToken))
                return;

            basicCharacteristics = new List<Characteristic>()
            {
                new Characteristic("Интерес к учебе", "Отсутствует интерес к учебе", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Аналитический склад ума", "отсутствует интерес к математическим предметам", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Гуманитарный склад ума", "отсутствует интерес к гуманитарным предметам", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Есть интерес к backend разработке", "отсутствует интерес к backend разработке", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Есть интерес к frontend разработке", "отсутствует интерес к frontend разработке", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Умеет работать в команде", "Не умеет работать в команде", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Интерес к науке", "Отсутствует интерес к науке", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Интерес к будущей профессии", "Отсутствует интерес к будущей профессии", CharacteristicType.Peculiarity, -1, 1),
                new Characteristic("Целеустремленность", "Не целеустремленный", CharacteristicType.Peculiarity, -1, 1),
            };

            basicSubjects = new List<Characteristic>()
            {
                new Characteristic("C#", "не нравится C#", CharacteristicType.Subject, 0, 1),
                new Characteristic("Физика", "не нравится Физика", CharacteristicType.Subject, 0, 1),
                new Characteristic("Математика", "не нравится Математика", CharacteristicType.Subject, 0, 1),
                new Characteristic("Информатика", "не нравится Информатика", CharacteristicType.Subject, 0, 1),
                new Characteristic("Программирование", "не нравится Программирование", CharacteristicType.Subject, 0, 1),
                new Characteristic("Философия", "не нравится Философия", CharacteristicType.Subject, 0, 1),
                new Characteristic("Английский язык", "не нравится Английский язык", CharacteristicType.Subject, 0, 1),
                new Characteristic("Русский язык", "не нравится Русский язык", CharacteristicType.Subject, 0, 1),
                new Characteristic("Дизайн", "не нравится Дизайн", CharacteristicType.Subject, 0, 1),
            };

            await _dbContext.Characteristics.AddRangeAsync(basicCharacteristics);
            await _dbContext.Characteristics.AddRangeAsync(basicSubjects);
        }

        private async Task SeedTestStudentCharacteristcs(CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students.Include(s => s.User)
                .Include(s => s.StudentCharacteristics)
                .FirstOrDefaultAsync(x => x.User != null && x.User.Login == "student");

            if (student == null || student.StudentCharacteristics.Count != 0) 
                return;

            var characteristics = await _dbContext.Characteristics.Where(x => 
                x.PositiveDescription == "Умеет работать в команде" ||
                x.PositiveDescription == "Целеустремленность" ||
                x.PositiveDescription == "C#" ||
                x.PositiveDescription == "Физика").ToListAsync();

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

            await _dbContext.SaveChangesAsync();
            _dbContext.AdminId = admin.Id;
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
