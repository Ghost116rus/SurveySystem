using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Tags
{
    public class GetAllTagsResponse
    {
        public List<TagDTO> QuestionTags { get; set; }
        public List<TagDTO> SurveyTags { get; set; }
        public List<TagDTO> UniversalTags { get; set; }
    }
}
