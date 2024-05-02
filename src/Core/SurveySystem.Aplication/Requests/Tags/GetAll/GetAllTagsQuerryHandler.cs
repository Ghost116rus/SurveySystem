using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Requests.Tags;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Tags.GetAll
{
    public class GetAllTagsQuerryHandler : IRequestHandler<GetAllTagsQuerry, GetAllTagsResponse>
    {
        private readonly IDbContext _dbContext;

        public GetAllTagsQuerryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllTagsResponse> Handle(GetAllTagsQuerry request, CancellationToken cancellationToken)
        {
            var tags = await _dbContext.Tags.ToListAsync();

            var sortedTags = tags.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key,
                    x => x.Select(x => new TagDTO()
                    {
                        Id = x.Id,
                        Description = x.Description
                    }).ToList());

            return new GetAllTagsResponse()
            {
                UniversalTags = sortedTags[Domain.Enums.TagType.All],
                QuestionTags = sortedTags[Domain.Enums.TagType.ForQuestions],
                SurveyTags = sortedTags[Domain.Enums.TagType.ForSurveys],
            };
        }
    }
}
