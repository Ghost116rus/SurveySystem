using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.Students.GetCurrentStudentSurvey
{
    public class GetCurrentStudentSurveyResponse
    {
        public bool IsRepetable { get; set; }
        public bool IsCompleted { get; set; }

        public List<CurrentStudentSurveyTestQuestionDTO> Answers { get; set; } = new();

        public CurrentStudentSurveyTestQuestionDTO CurrentQuestion { get; set; }  

    }

    public class CurrentStudentSurveyTestQuestionDTO
    {
        public Guid Id { get; set; }  
        public string QuestionText { get; set; }
        public int MaxCountOfAnswers { get; set; }
        public QuestionType Type { get; set; }
        public bool IsActual { get; set; }
        public string AnswerTime { get; set; } 

        public List<CurrentStudentSurveyQuestionAnswerDTO> Answers { get; set; } = new ();
    }

    public class CurrentStudentSurveyQuestionAnswerDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public bool IsSelected { get; set; }
    }

}
