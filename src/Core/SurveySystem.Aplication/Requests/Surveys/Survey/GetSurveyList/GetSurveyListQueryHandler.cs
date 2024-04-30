using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Extenstions;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Requests.Surveys;
using SurveySystem.Requests.Surveys.Survey;

namespace SurveySystem.Aplication.Requests.Surveys.Survey.GetSurveyList
{
    public class GetSurveyListQueryHandler : IRequestHandler<GetSurveyListQuery, GetSurveysListResponse>
    {
        private readonly IDbContext _dbContext;

        public GetSurveyListQueryHandler(IDbContext dbContext) 
            => _dbContext = dbContext;

        public async Task<GetSurveysListResponse> Handle(GetSurveyListQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var surveys = await _dbContext.Surveys.FilterByTags(request.Tags)
                .Select(x => new SurveyLightDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TagsId = x.Tags != null ? x.Tags.Select(x => x.Id).ToList() : null, 
                })
                .ToListAsync(cancellationToken);

            return new GetSurveysListResponse() { Surveys = surveys };
        }
    }
}
