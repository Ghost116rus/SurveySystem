using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Surveys
{
    public class SurveyLightDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Guid>? TagsId { get; set; }
    }
}
