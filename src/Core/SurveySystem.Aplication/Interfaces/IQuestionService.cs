using SurveySystem.Domain.Entities.Users;
using SurveySystem.Requests.Students.GetCurrentStudentSurvey;

namespace SurveySystem.Aplication.Interfaces
{
    public interface IQuestionService
    {
        public Task<CurrentStudentSurveyTestQuestionDTO?> GetCurrentQuestionDTO(StudentSurveyProgress studentProgress);
    }
}
