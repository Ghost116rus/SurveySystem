using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
