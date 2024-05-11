using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Students
{
    public class GetLightStudentProgressesResponse
    {
        public List<StudentProgressLightDTO> LightStudentProgresses { get; set; } = new();
    }
}
