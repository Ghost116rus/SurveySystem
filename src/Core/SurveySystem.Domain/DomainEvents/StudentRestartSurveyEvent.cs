using SurveySystem.Domain.Interfaces;

namespace SurveySystem.Domain.DomainEvents
{
    public class StudentRestartSurveyEvent : IDomainEvent
    {
        public StudentRestartSurveyEvent(Guid surveyProgressId)
        {
            SurveyProgressId = surveyProgressId;
        }

        public Guid SurveyProgressId { get; private set; }
        public bool IsInTransaction => true;
    }
}
