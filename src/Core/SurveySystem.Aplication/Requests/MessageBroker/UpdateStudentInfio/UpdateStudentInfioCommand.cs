using MediatR;
using SurveySystem.Requests.MessageBroker;

namespace SurveySystem.Aplication.Requests.MessageBroker.UpdateStudentInfio
{
    public class UpdateStudentInfioCommand : UpdateStudentInfoFromBrokerDTO, IRequest
    {
    }
}
