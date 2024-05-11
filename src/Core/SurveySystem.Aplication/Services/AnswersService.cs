using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Domain.Interfaces;

namespace SurveySystem.Aplication.Services
{
    public class AnswersService : IAnswersService
    {
        public List<Answer> GetDefaultAnswers(Question question)
        {
            if (question.Type != Domain.Enums.QuestionType.Alternative)
                throw new BadDataException("Стандартные ответы могут быть только у альтернативных вопросов");

            if (!question.IsAutoCreatedAnswers)
                throw new RequiredFieldNotSpecifiedException("Автоматическое формирование ответов неправильно задано");

            return new List<Answer>()
            {
                new Answer("Это совсем не относится ко мне", question),
                new Answer("Это не относится ко мне", question),
                new Answer("Сомневаюсь в ответе", question),
                new Answer("Это относится ко мне", question),
                new Answer("Это полностью относится ко мне", question)
            };
        }

        public List<AnswerCharacteristicValue> GetDefaultAnswerCharacteristicValuesForSurvey(List<Tuple<Characteristic, List<Answer>>> characteristicsAndAnswers)
        {
            var countOfEachAnswerForCharacteristic = characteristicsAndAnswers
                .GroupBy(x => x.Item1.PositiveDescription)
                .ToDictionary(group => group.Key, group => group.Count());

            var answerCharacteristicList = new List<AnswerCharacteristicValue>();

            foreach (var tuple in characteristicsAndAnswers)
            {
                var characteristic = tuple.Item1;
                var count = countOfEachAnswerForCharacteristic[characteristic.PositiveDescription];
                var answers = tuple.Item2;

                var minValueForDefalutAnswer = characteristic.MinValue / count;
                var negativeValueForDefalutAnswer = characteristic.MiddleNegativeValue / count;
                var neutralValueForDefalutAnswer = characteristic.MiddleValue / count;
                var positiveValueForDefalutAnswer = characteristic.MiddlePositiveValue / count;
                var maxValueForDefalutAnswer = characteristic.MaxValue / count;

                answerCharacteristicList.AddRange(new List<AnswerCharacteristicValue>()
                {
                    new AnswerCharacteristicValue(answers[0], characteristic, minValueForDefalutAnswer),
                    new AnswerCharacteristicValue(answers[1], characteristic, negativeValueForDefalutAnswer),
                    new AnswerCharacteristicValue(answers[2], characteristic, neutralValueForDefalutAnswer),
                    new AnswerCharacteristicValue(answers[3], characteristic, positiveValueForDefalutAnswer),
                    new AnswerCharacteristicValue(answers[4], characteristic, maxValueForDefalutAnswer),
                });
            }

            return answerCharacteristicList;
        }
    }
}
