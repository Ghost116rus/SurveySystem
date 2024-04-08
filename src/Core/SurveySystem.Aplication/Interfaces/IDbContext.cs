using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;

namespace SurveySystem.Aplication.Interfaces
{
    public interface IDbContext
    {
        #region UsersEntities
        
        /// <summary>
        /// Пользователи системы
        /// </summary>
        DbSet<User> Users { get; }

        /// <summary>
        /// Студенты, каждый студент связан <see cref="User"/>
        /// </summary>
        DbSet<Student> Students { get; }

        /// <summary>
        /// Ответы студента
        /// </summary>
        DbSet<StudentAnswer> StudentAnswers { get; }

        /// <summary>
        /// Характеристики студента
        /// </summary>
        DbSet<StudentCharacteristic> StudentCharacteristic { get; }

        /// <summary>
        /// Прогресс прохождения студентом опроса
        /// </summary>
        DbSet<StudentSurveyProgress> SurveyProgress { get; }

        #endregion

        #region SurveyEntities

        /// <summary>
        /// Ответы
        /// </summary>
        DbSet<Answer> Answers { get; }

        /// <summary>
        /// Значение ответа на вопрос
        /// </summary>
        DbSet<AnswerCharacteristicValue> AnswerCharacteristicValues { get; }

        /// <summary>
        /// Характерные черты
        /// </summary>
        DbSet<Characteristic> Characteristics { get; }

        /// <summary>
        /// Вопросы
        /// </summary>
        DbSet<Question> Questions { get; }

        /// <summary>
        /// Критерии оценки откртыго вопроса (вопроса с произволной формой ответа)
        /// </summary>
        DbSet<QuestionEvaluationCriteria> QuestionEvaluationCriteries { get; }

        /// <summary>
        /// Опросы для студентов
        /// </summary>
        DbSet<Survey> Surveys { get; }

        /// <summary>
        /// Вопросы, которые входят в опрос
        /// </summary>
        DbSet<SurveyTestQuestion> SurveysTestQuestions { get;}

        #endregion

        #region OrganizationEntities

        /// <summary>
        /// Институты
        /// </summary>
        DbSet<Institute> Institutes { get; }

        /// <summary>
        /// Кафедры
        /// </summary>
        DbSet<Faculty> Faculties { get; }

        /// <summary>
        /// Семестры обучения
        /// </summary>
        DbSet<Semester> Semesters { get; }

        #endregion

        /// <summary>
        /// БД в памяти
        /// </summary>
        bool IsInMemory { get; }

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Количество обновленных записей</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
