using MediatR;
using SurveySystem.Aplication.Requests.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Characteristics.Create
{
    public class CreateCharacteristicCommand : CreateCharacteristicRequest, IRequest<Guid>
    {
    }
}
