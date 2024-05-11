using MediatR;
using SurveySystem.Requests.Questions;

namespace SurveySystem.Aplication.Requests.Surveys.Questions.Create
{
    public class CreateQuestionCommand : CreateQuestionRequest, IRequest<Guid>
    {
    }
}
