using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Enums
{
    public enum TagType
    {
        [Description("Универсальный тэг")]
        All = 0,

        [Description("Тэг, используемый в первую очередь для вопросов")]
        ForQuestions = 1,

        [Description("Тэг, используемый в первую очередь для опросов")]
        ForSurveys = 2,

        [Description("Тэг, используемый в первую очередь для ответов")]
        ForAnswers = 3
    }
}
