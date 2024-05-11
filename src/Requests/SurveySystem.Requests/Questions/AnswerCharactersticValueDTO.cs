using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Questions
{
    public class AnswerCharactersticValueDTO
    {
        public Guid CharacteristicId { get; set; }
        public double CharacteristicsValue { get; set; }
    }
}
