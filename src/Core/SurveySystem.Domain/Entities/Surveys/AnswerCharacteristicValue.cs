using SurveySystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class AnswerCharacteristicValue
    {
        /// <summary>
        /// Поле для <see cref="_answer"/>
        /// </summary>
        public const string AnswerField = nameof(_answer);

        /// <summary>
        /// Поле для <see cref="_characteristic"/>
        /// </summary>
        public const string CharacteristicField = nameof(_characteristic);


        private double _value;
        private Answer? _answer;
        private Characteristic? _characteristic;

        public AnswerCharacteristicValue(Answer answer, Characteristic characteristic)
        {
            Value = 0;
            Answer = answer;
            Characteristic = characteristic;
        }

        protected AnswerCharacteristicValue() { }

        public Guid AnswerId { get; private set; }
        public Guid CharacteristicId { get; private set; }

        /// <summary>
        /// Влияние ответа на заданную характеристику
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (value < Characteristic?.MinValue)
                    _value = Characteristic.MinValue;
                else if (value > Characteristic?.MaxValue)
                    _value = Characteristic.MaxValue;
                else
                    _value = value;
            }
        }

        #region NavigfationProperties

        public Answer? Answer
        {
            get => _answer;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _answer = value;
                AnswerId = value.Id;
            }
        }

        public Characteristic? Characteristic
        {
            get => _characteristic;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _characteristic = value;
                CharacteristicId = value.Id;
            }
        }

        #endregion
    }
}
