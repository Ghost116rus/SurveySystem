using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Characteristic : BaseEntity
    {
        public string Description { get; set; } 
        public CharacteristicType CharacteristicType { get; set; }

        public double MinValue { get; set; }    
        public double MaxValue { get; set; }
    }
}
