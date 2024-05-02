using MediatR;
using SurveySystem.Requests.Tags;

namespace SurveySystem.Aplication.Requests.Tags.Delete
{
    public class DeleteTagCommand : DeleteTagRequest, IRequest
    {
    }
}
