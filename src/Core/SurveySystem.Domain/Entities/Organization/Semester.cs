using SurveySystem.Domain.Entities.Surveys;

namespace SurveySystem.Domain.Entities.Organization
{
    public class Semester
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Semester()
        {
        }

        public int Number { get; set; }

        public List<Survey> Surveys { get; set; } = new List<Survey>();
    }
}