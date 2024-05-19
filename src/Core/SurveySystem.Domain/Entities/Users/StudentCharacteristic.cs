using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Exceptions;
using System.Reflection.PortableExecutable;

namespace SurveySystem.Domain.Entities.Users
{
    public class StudentCharacteristic
    {
        /// <summary>
        /// Поле для <see cref="_student"/>
        /// </summary>
        public const string StudentField = nameof(_student);

        /// <summary>
        /// Поле для <see cref="_characteristic"/>
        /// </summary>
        public const string CharacteristicField = nameof(_characteristic);


        private double _value;
        private Student _student;
        private Characteristic _characteristic;

        public StudentCharacteristic(Student student, Characteristic characteristic)
        {
            Student = student;
            Characteristic = characteristic;
            Value = characteristic.MiddleValue;
        }

        protected StudentCharacteristic() { }

        public Guid StudentId { get; private set; }
        public Guid CharacteristicId { get; private set; }

        /// <summary>
        /// Степень выраженности данной особенности пользователя
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

        public Student Student
        {
            get => _student;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _student = value;
                StudentId = value.Id;
            }
        }

        public Characteristic Characteristic
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

        /// <summary>
        /// Приводит <see cref="Value"/> к числу от 0 до 1.
        /// Если <see cref="Characteristic"/> не был подтянут из бд, вызывается исключение
        /// </summary>
        /// <exception cref="ExceptionBase"></exception>
        private void ConvertValueToDecimalPoint()
        {
            if (Characteristic is null)
                throw new ExceptionBase("Не были подтянуты все необходимые данные");

            var denominator = Characteristic.MaxValue - Characteristic.MinValue;
            if (denominator == 0)
                throw new ExceptionBase("Не должно быть так, что максимальное и минимальное значение характеристик одинаковы");

            _value = (_value - Characteristic!.MinValue)
                / denominator;
        }

        /// <summary>
        /// Если <see cref="Characteristic"/> не был подтянут из бд, вызывается исключение
        /// </summary>
        /// <returns>В случае, если <see cref="Value"/> больше 0.5 - возвращает true</returns>
        /// <exception cref="ExceptionBase"></exception>
        public bool CheckIsPostiveCharacteristicValue()
        {
            ConvertValueToDecimalPoint();

            if (_value > 0.5)
                return true;

            return false;
        }
    }
}
