using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.StudentCharacteristic;

namespace SurveySystem.Aplication.Requests.Student.Characteristics.GetStudentCharacteristic
{
    public class GetStudentCharacteristicQueryHandler : IRequestHandler<GetStudentCharacteristicQuery, GetStudentCharacteristicResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetStudentCharacteristicQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<GetStudentCharacteristicResponse> Handle(GetStudentCharacteristicQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var student = await _dbContext.Students
                .Include(s => s.StudentCharacteristics)
                .ThenInclude(c => c.Characteristic)
                .FirstOrDefaultAsync(s => s.Id == _userContext.CurrentUserId) 
                ?? throw new NotFoundException("Заданный пользователь не найден");

            var studentCharacteristic = student.StudentCharacteristics.Where(x => CheckMeasuredCharacteristic(x)).ToList();

            var dict = new Dictionary<CharacteristicType, List<Tuple<string, CharacteristicMeasure>>>()
            { 
                { CharacteristicType.Subject, new List<Tuple<string, CharacteristicMeasure>>()}, 
                { CharacteristicType.Peculiarity, new List<Tuple<string, CharacteristicMeasure>>()} 
            };

            foreach (var x in studentCharacteristic)
                dict[x.Characteristic!.CharacteristicType]
                    .Add(new Tuple<string, CharacteristicMeasure>(x.Characteristic!.Description, CharacteristicMeasureExtension.GetValue(x.Value)));

            return new GetStudentCharacteristicResponse()
            {
                PersonalCharacteristics = dict[CharacteristicType.Peculiarity],
                Subjects = dict[CharacteristicType.Peculiarity],
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentCharacteristic"></param>
        /// <returns></returns>
        private bool CheckMeasuredCharacteristic(StudentCharacteristic studentCharacteristic)
        {
            var characteristic = studentCharacteristic.Characteristic;
            var denominator = characteristic!.MaxValue - characteristic.MinValue;

            if (denominator == 0) return false;

            studentCharacteristic.Value = (studentCharacteristic.Value - characteristic!.MinValue)
                / denominator;

            if (studentCharacteristic.Value > 0.5)
                return true;

            return false;
        }

    }
}
