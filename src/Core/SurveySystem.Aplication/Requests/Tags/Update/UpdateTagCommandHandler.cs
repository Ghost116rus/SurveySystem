using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.Requests.Tags.Update
{
    internal class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly IDbContext _dbContext;
        public UpdateTagCommandHandler(IDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var tag = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                throw new NotFoundException("Тег с заданным Id не найден");

            tag.UpdateInfo( request.Description, request.Type);

            await _dbContext.SaveChangesAsync();

            return default;
        }
    }
}
