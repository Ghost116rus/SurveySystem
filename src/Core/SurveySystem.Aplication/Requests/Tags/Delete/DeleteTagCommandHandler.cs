using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Tags.Delete
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteTagCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var tag = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                throw new NotFoundException("Тег с заданным Id не найден");

            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();
            return default;
        }
    }
}
