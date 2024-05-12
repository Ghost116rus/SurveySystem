using System.ComponentModel;

namespace SurveySystem.Domain.Enums
{
    public enum QuestionType
    {
        [Description("Вопрос с одним вариантом ответа")]
        Alternative,

        [Description("Вопрос с несколькими вариантами ответа")]
        NonAlternative,

        [Description("Вопрос, где пользователю необходимо написать свой вариант ответа")]
        Open,

        [Description("Информация, не требующая прочтения, но не ответа")]
        Information,
    }
}
