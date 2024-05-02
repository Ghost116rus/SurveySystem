using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Tags
{
    public class UpdateTagRequest : CreateTagRequest
    {
        public Guid Id { get; set; }
    }
}
