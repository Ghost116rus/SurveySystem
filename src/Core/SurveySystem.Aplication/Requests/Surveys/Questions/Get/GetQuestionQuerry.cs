using MediatR;
using SurveySystem.Requests.Questions.GetQuestionList;

namespace SurveySystem.Aplication.Requests.Surveys.Questions.Get
{
    public class GetQuestionQuerry : GetQuestionListRequest, IRequest<GetQuestionListResponse>
    {
    }
}
