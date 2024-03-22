using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;

namespace SurveySystem.Aplication.Interfaces
{
    public interface IDbContext
    {
        #region UsersEntities

        DbSet<User> Users { get; }
        DbSet<Student> Students { get; }
        DbSet<StudentAnswer> StudentAnswers { get; }
        DbSet<StudentCharacteristic> StudentCharacteristic { get; }
        DbSet<StudentSurveyProgress> SurveyProgress { get; }

        #endregion

        #region SurveyEntities

        DbSet<Answer> Answers { get; }
        DbSet<AnswerCharacteristicValue> AnswerCharacteristicValues { get; }
        DbSet<Characteristic> Characteristics { get; }
        DbSet<Question> Questions { get; }
        DbSet<QuestionEvaluationCriteria> QuestionEvaluationCriteries { get; }
        DbSet<Survey> Surveys { get; }
        DbSet<SurveyTestQuestion> SurveysTestQuestions { get;}

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
