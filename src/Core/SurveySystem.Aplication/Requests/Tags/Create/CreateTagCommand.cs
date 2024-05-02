using MediatR;
using SurveySystem.Requests.Tags;

namespace SurveySystem.Aplication.Requests.Tags.Create
{
    public class CreateTagCommand : CreateTagRequest, IRequest<Guid>
    {
    }
}
