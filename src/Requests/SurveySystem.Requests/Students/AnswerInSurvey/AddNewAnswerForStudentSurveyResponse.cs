using SurveySystem.Requests.Students.GetCurrentStudentSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Students.AnswerInSurvey
{
    public class AddNewAnswerForStudentSurveyResponse
    {
        public bool IsCompleted { get; set; }
        public CurrentStudentSurveyTestQuestionDTO? CurrentQuestion { get; set; }
    }
}
