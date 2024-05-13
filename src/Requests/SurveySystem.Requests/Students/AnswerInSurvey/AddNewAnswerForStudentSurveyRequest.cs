using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Students.AnswerInSurvey
{
    public class AddNewAnswerForStudentSurveyRequest
    {
        public Guid StudentSurveyProgressId { get; set; }
        public List<Guid> AnswersId { get; set; }

    }
}
