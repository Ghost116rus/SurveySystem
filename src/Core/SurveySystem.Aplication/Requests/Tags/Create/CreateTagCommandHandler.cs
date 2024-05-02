using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Tags.Create
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
    {
        private readonly IDbContext _dbContext;
        public CreateTagCommandHandler(IDbContext dbContext) 
            => _dbContext = dbContext;

        public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (await _dbContext.Tags.AnyAsync(x => x.Description == request.Description))
                throw new BadDataException("Тег с данным описанием уже существует в базе данных");

            var tag = new Tag(request.Description, request.Type);

            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();

            return tag.Id;
        }
    }
}
