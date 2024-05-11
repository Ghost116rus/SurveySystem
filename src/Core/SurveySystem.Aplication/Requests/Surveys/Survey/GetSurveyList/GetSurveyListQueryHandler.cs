using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Extenstions;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Surveys;
using SurveySystem.Requests.Surveys.Survey;
using SurveySystem.Requests.Tags;

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
                    Tags = x.Tags != null ? x.Tags.Select(
                        x => new TagDTO() { Id = x.Id, Description = x.Description }).ToList() : null, 
                })
                .ToListAsync(cancellationToken);

            if (surveys.Count < 1)
                throw new NotFoundException("Анкеты " + (request.Tags.Count > 1 ? "с заданными тегами" : "") + "не были найдены");

            return new GetSurveysListResponse() { Surveys = surveys };
        }
    }
}
