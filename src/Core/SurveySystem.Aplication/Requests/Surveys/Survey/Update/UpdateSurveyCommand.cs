using MediatR;
using SurveySystem.Requests.Surveys.Survey;

namespace SurveySystem.Aplication.Requests.Surveys.Survey.Update
{
    /// <summary>
    /// Команда на добавление анкеты и вопросов анкеты
    /// </summary>
    public class UpdateSurveyCommand : UpdateSurveyRequest, IRequest<Unit>
    {
    }
}
