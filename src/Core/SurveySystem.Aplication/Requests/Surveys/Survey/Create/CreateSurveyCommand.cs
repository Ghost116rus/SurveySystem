using MediatR;
using SurveySystem.Requests.Surveys.Survey;

namespace SurveySystem.Aplication.Requests.Surveys.Survey.Create
{
    /// <summary>
    /// Команда на добавление анкеты и вопросов анкеты
    /// </summary>
    public class CreateSurveyCommand : CreateSurveyRequest, IRequest<Guid>
    {
    }
}
