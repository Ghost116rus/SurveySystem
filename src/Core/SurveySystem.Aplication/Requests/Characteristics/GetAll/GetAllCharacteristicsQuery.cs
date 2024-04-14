using MediatR;
using SurveySystem.Requests.Characteristic.GetAll;

namespace SurveySystem.Aplication.Requests.Characteristics.GetAll
{
    public class GetAllCharacteristicsQuery : GetAllCharacteristicRequest, IRequest<GetAllCharacteristicResponse>
    {
    }
}
