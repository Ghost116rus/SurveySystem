using SurveySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Tags
{
    public class CreateTagRequest
    {
        public string Description { get; set; }
        public TagType Type { get; set; }
    }
}
