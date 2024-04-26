using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Students.StudentProgress
{
    public class GetAllStudentProgressResponse
    {
        public List<LightStudentProgressDTO> LightStudentProgressDTOs { get; set; } = new List<LightStudentProgressDTO>();
    }

    public class LightStudentProgressDTO
    {
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public bool IsCompleted { get; set; }
    }
}
