using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Requests.Students;

namespace SurveySystem.Aplication.Requests.Student.StudentProgress
{
    public class GetStudentProgressesQueryHandler : IRequestHandler<GetStudentProgressesQuery, GetLightStudentProgressesResponse>
    {
        private readonly IUserContext _userContext;
        private readonly IDbContext _dbContext;

        public GetStudentProgressesQueryHandler(IUserContext userContext, IDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task<GetLightStudentProgressesResponse> Handle(GetStudentProgressesQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var userProgresses = await _dbContext.SurveyProgress
                .Include(sP => sP.Survey)
                .Where(sP => sP.StudentId == _userContext.CurrentUserId)
                .OrderBy(s => s.ModifiedOn)
                .Select(sP => new StudentProgressLightDTO
                {
                    Id = sP.Id,
                    SurveyName = sP.Survey!.Name,
                    UpdatedTime = sP.ModifiedOn.Date.ToString("dd.MM.yyyy"),
                }).ToListAsync();

            return new GetLightStudentProgressesResponse() 
            { 
                LightStudentProgresses = userProgresses
            };
        }
    }
}
