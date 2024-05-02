using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Surveys.Survey
{
    public class UpdateSurveyRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRepetable { get; set; }
        public bool IsVisible { get; set; }
        public DateTime? StartDate { get; set; }

        public HashSet<int>? Semesters { get; set; }
        public HashSet<Guid>? FacultiesId { get; set; }
        public HashSet<Guid>? InstitutesId { get; set; }
        public HashSet<Guid>? TagsId { get; set; }
        public HashSet<SurveyTestQuestionDTO> TestQuestions { get; set; }
    }
}
