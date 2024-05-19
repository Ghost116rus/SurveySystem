using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.DigitalProfile;

namespace SurveySystem.Aplication.Requests.DigitalProfile.GetStudentStudentDogitalProfile
{
    public class GetOnlyPositiveCharacteristicInStudentProfileQueryHandler : IRequestHandler<GetOnlyPositiveCharacteristicInStudentProfileQuery, GetDigitalProfileResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetOnlyPositiveCharacteristicInStudentProfileQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<GetDigitalProfileResponse> Handle(GetOnlyPositiveCharacteristicInStudentProfileQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var student = await _dbContext.Students
                .Include(s => s.StudentCharacteristics)
                .ThenInclude(c => c.Characteristic)
                .FirstOrDefaultAsync(s => s.Id == _userContext.CurrentUserId)
                    ?? throw new NotFoundException("Заданный пользователь не найден");

            var studentCharacteristic = student.StudentCharacteristics.Where(x => x.CheckIsPostiveCharacteristicValue()).ToList();

            var dict = new Dictionary<CharacteristicType, List<StudentCharacteristicDTO>>()
            {
                { CharacteristicType.Subject, new List<StudentCharacteristicDTO>()},
                { CharacteristicType.Peculiarity, new List<StudentCharacteristicDTO>()}
            };

            foreach (var x in studentCharacteristic)
                dict[x.Characteristic!.CharacteristicType]
                    .Add(new StudentCharacteristicDTO()
                    {
                        Description = x.Characteristic!.PositiveDescription,
                        CharacteristicMeasure = Enum.GetName(CharacteristicMeasureMethods.GetValue(x.Value))!
                    });

            return new GetDigitalProfileResponse()
            {
                PersonalCharacteristics = dict[CharacteristicType.Peculiarity],
                Subjects = dict[CharacteristicType.Subject],
            };
        }
    }
}
