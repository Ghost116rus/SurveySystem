using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.DigitalProfile;

namespace SurveySystem.Aplication.Requests.DigitalProfile.GetStudentInterests
{
    public class GetStudentInterestsQueryHandler : IRequestHandler<GetStudentInterestsQuery, GetStudentInterestsResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetStudentInterestsQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<GetStudentInterestsResponse> Handle(GetStudentInterestsQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var student = await _dbContext.Students
                .Include(s => s.StudentCharacteristics)
                .ThenInclude(c => c.Characteristic)
                .FirstOrDefaultAsync(s => s.Id == _userContext.CurrentUserId)
                    ?? throw new NotFoundException("Заданный пользователь не найден");

            var studentCharacteristic = student.StudentCharacteristics
                .Where(x => 
                    x.Characteristic!.CharacteristicType == CharacteristicType.Subject 
                    && x.CheckIsPostiveCharacteristicValue())
                .Select(x => new StudentCharacteristicDTO()
                {
                    Description = x.Characteristic!.PositiveDescription,
                    CharacteristicMeasure = Enum.GetName(CharacteristicMeasureMethods.GetValue(x.Value))!
                })
                .ToList();

            return new GetStudentInterestsResponse()
            {
                Subjects = studentCharacteristic
            };
        }
    }
}
