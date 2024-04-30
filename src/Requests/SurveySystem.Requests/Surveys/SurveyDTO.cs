using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Surveys
{
    public class SurveyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRepetable { get; set; }
        public bool IsVisible { get; set; }
        public DateTime? StartDate { get; set; }

        public IEnumerable<int>? Semesters { get; set; }
        public IEnumerable<Guid>? FacultiesId { get; set; }
        public IEnumerable<Guid>? InstitutesId { get; set; }
        public IEnumerable<Guid>? TagsId { get; set; }
        public IEnumerable<SurveyTestQuestionDTO> TestQuestions { get; set; }
    }
}
