using MediatR;
using SurveySystem.Requests.Students.StudentProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Student.SurveyProgress.GetAllSurveyProgresses
{
    public class GetAllSurveyProgressesQuery : IRequest<GetAllStudentProgressResponse>
    {
    }
}
