using System.ComponentModel;

namespace SurveySystem.Domain.Enums
{
    public enum CharacteristicType
    {
        [Description("Предмет, что обучающиеся изучают")]
        Subject = 1,

        [Description("Черта личности")]
        Peculiarity = 2,
    }
}
