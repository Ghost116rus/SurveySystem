using System.ComponentModel;

namespace SurveySystem.Domain.Enums
{
    public enum CharacteristicMeasure
    {
        [Description("Противоположно")]
        Opposite,

        [Description("Отрицательно выраженно")]
        NegativelyExpressed,

        [Description("Нейтрально")]
        Neutral,

        [Description("Положительно выраженно")]
        PositivelyExpressed,

        [Description("Ярко выражено")]
        Pronounced
    }

    public static class CharacteristicMeasureMethods
    {
        public static CharacteristicMeasure GetValue(double value)
        {
            if (value >= 0.75)
                return CharacteristicMeasure.Pronounced;
            else if (value > 0.5)
                return CharacteristicMeasure.PositivelyExpressed;
            else if (value <= 0.25)
                return CharacteristicMeasure.Opposite;
            else if (value < 0.5)
                return CharacteristicMeasure.NegativelyExpressed;
            else
                return CharacteristicMeasure.Neutral;
        }
    }
}
