using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.Requests.Characteristics.Put
{
    public class PutCharacteristicCommandHandler : IRequestHandler<PutCharacteristicCommand>
    {
        private readonly IDbContext _dbContext;

        public PutCharacteristicCommandHandler(IDbContext dbContext) { _dbContext = dbContext; }  

        public async Task<Unit> Handle(PutCharacteristicCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var charactersitc = await _dbContext.Characteristics
                .FirstOrDefaultAsync(c => c.Id == request.CharactetisticId) ?? throw new NotFoundException();

            charactersitc.PositiveDescription = request.PositiveDescription;
            charactersitc.NegativeDescription = request.NegativeDescription;
            charactersitc.SetMinAndMaxValue(request.MinValue, request.MaxValue);
            charactersitc.CharacteristicType = request.CharacteristicType;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default!;
        }
    }
}
