using MediatR;
using SurveySystem.Requests.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Student.StudentProgress
{
    public class GetStudentProgressesQuery :IRequest<GetLightStudentProgressesResponse>
    {
    }
}
