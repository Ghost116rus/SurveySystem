using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.Requests.Characteristics.Patch
{
    public class PatchCharacteristicCommandHandler : IRequestHandler<PatchCharacteristicCommand>
    {
        private readonly IDbContext _dbContext;

        public PatchCharacteristicCommandHandler(IDbContext dbContext) { _dbContext = dbContext; }  

        public async Task<Unit> Handle(PatchCharacteristicCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var charactersitc = await _dbContext.Characteristics
                .FirstOrDefaultAsync(c => c.Id == request.CharactetisticId) ?? throw new NotFoundException();

            charactersitc.PositiveDescription = request.PositiveDescription;
            charactersitc.NegativeDescription = request.NegativeDescription;
            charactersitc.SetMinAndMaxValue(request.MinValue, request.MaxValue);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default!;
        }
    }
}
