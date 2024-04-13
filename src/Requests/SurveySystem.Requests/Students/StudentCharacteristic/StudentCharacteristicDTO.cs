using SurveySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Students.StudentCharacteristic
{
    public class StudentCharacteristicDTO
    {
        public string Description { get; set; }
        public CharacteristicMeasure CharacteristicMeasure { get; set; }
    }
}
