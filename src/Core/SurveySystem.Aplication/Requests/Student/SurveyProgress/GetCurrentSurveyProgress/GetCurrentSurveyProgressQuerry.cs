using MediatR;
using SurveySystem.Requests.Students.StudentProgress;
using SurveySystem.Requests.Students.StudentProgress.GetCurrentProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Student.SurveyProgress.GetCurrentSurveyProgress
{
    public class GetCurrentSurveyProgressQuerry : GetCurrentProgressRequest, IRequest<ProgressDTO>
    {
    }
}
