using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Characteristic : BaseEntity
    {
        private string _positiveDescription = default!;
        private string _negativeDescription = default!;

        public Characteristic(string positiveDescription, string negativeDescription, CharacteristicType characteristicType, double minValue, double maxValue)
        {
            PositiveDescription = positiveDescription;
            NegativeDescription = negativeDescription;
            CharacteristicType = characteristicType;
            SetMinAndMaxValue(minValue, maxValue);
        }

        /// <summary>
        /// Конструктор EF
        /// </summary>
        protected Characteristic() { }

        /// <summary>
        /// Положительное описание характеристики (качество, которым в той или иной степени обладает студент)
        /// </summary>
        public string PositiveDescription
        {
            get => _positiveDescription;
            set => _positiveDescription = string.IsNullOrEmpty(value) ? 
                throw new RequiredFieldNotSpecifiedException("CharacteristicPositiveDescription") : value;
        }

        /// <summary>
        /// Отрицательное описание характеристики (качество, которым в той или иной степени НЕ обладает студент)
        /// </summary>
        public string NegativeDescription
        {
            get => _negativeDescription;
            set => _negativeDescription = string.IsNullOrEmpty(value) ? 
                throw new RequiredFieldNotSpecifiedException("CharacteristicNegativeDescription") : value;
        }

        /// <summary>
        /// Тип характеристики: к примеру, есть интерес к физике - интерес к предмету, либо - умение работать в команде - личное качество.
        /// Подробно смотри в <see cref="CharacteristicType"/>
        /// </summary>
        public CharacteristicType CharacteristicType { get; set; }

        public double MinValue { get; private set; }    
        public double MaxValue { get; private set; }

        public double MiddlePositiveValue { get => (MaxValue + MiddleValue) / 2; }
        public double MiddleValue         { get => (MaxValue + MinValue) / 2; }
        public double MiddleNegativeValue { get => (MinValue + MiddleValue) / 2; }

        /// <summary>
        /// Метод установки минимального и максимального значения характеристики
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <exception cref="BadDataException"></exception>
        public void SetMinAndMaxValue(double min, double max)
        {
            if (max - min == 0)
                throw new BadDataException("Значения min и max не могут быть одинаковы!");
            if (min > max)
                throw new BadDataException("Значения min не могут быть больше max");
            MinValue = min;
            MaxValue = max;
        }
    }
}
