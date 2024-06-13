using MediatR;
using SurveySystem.Requests.MessageBroker;

namespace SurveySystem.Aplication.Requests.MessageBroker.RegisterNewStudent
{
    public class RegisterNewStudentCommand : RegisterNewStudentFromBrokerDTO, IRequest
    {
    }
}
