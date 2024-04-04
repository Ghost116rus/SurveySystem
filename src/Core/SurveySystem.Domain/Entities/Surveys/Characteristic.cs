using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using System.Reflection.PortableExecutable;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Characteristic : BaseEntity
    {
        private string _description = default!;

        public Characteristic(string description, CharacteristicType characteristicType, double minValue, double maxValue)
        {
            Description = description;
            CharacteristicType = characteristicType;
            MinValue = minValue;
            MaxValue = maxValue;
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

        public double MinValue { get; set; }    
        public double MaxValue { get; set; }
    }
}
