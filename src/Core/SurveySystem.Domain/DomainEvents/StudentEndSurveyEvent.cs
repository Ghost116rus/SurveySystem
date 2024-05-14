using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Interfaces;

namespace SurveySystem.Domain.DomainEvents
{
    /// <summary>
    /// Событие завершения опроса студентом
    /// </summary>
    public class StudentEndSurveyEvent : IDomainEvent
    {
        public StudentEndSurveyEvent(Guid surveyProgressId)
        {
            SurveyProgressId = surveyProgressId;
        }

        public Guid SurveyProgressId { get; private set; }
        public bool IsInTransaction => true;
    }
}
