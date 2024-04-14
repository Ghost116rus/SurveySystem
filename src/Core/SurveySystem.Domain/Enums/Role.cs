using System.ComponentModel;

namespace SurveySystem.Domain.Enums
{
    public enum Role
    {
        [Description("Студент")]
        Student,

        [Description("Наблюдатель")]
        Observer,

        [Description("Составитель опросов")]
        SurveyMaker,

        [Description("Администратор системы")]
        Administrator
    }


}
