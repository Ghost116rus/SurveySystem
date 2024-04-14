using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Characteristic : BaseEntity
    {
        private string _description = default!;

        public Characteristic(string description, CharacteristicType characteristicType, double minValue, double maxValue)
        {
            Description = description;
            CharacteristicType = characteristicType;
            SetMinAndMaxValue(minValue, maxValue);
        }

        /// <summary>
        /// Конструктор EF
        /// </summary>
        protected Characteristic() { }

        /// <summary>
        /// Описание характеристики
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = string.IsNullOrEmpty(value) ? 
                throw new RequiredFieldNotSpecifiedException("CharacteristicDescription") : value;
        }

        /// <summary>
        /// Тип характеристики: к примеру, есть интерес к физике - интерес к предмету, либо - умение работать в команде - личное качество.
        /// Подробно смотри в <see cref="CharacteristicType"/>
        /// </summary>
        public CharacteristicType CharacteristicType { get; set; }

        public double MinValue { get; private set; }    
        public double MaxValue { get; private set; }

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
