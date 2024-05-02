using MediatR;
using SurveySystem.Aplication.Interfaces;


namespace SurveySystem.Aplication.Requests.Characteristics.Create
{
    public class CreateCharacteristicCommandHandler : IRequestHandler<CreateCharacteristicCommand, Guid>
    {
        private readonly IDbContext _dbContext;

        public CreateCharacteristicCommandHandler(IDbContext dbContext) { _dbContext = dbContext; }

        public async Task<Guid> Handle(CreateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var characteristic = new Domain.Entities.Surveys.Characteristic(
                positiveDescription: request.PositiveDescription, negativeDescription: request.NegativeDescription,
                characteristicType: request.CharacteristicType,
                minValue: request.MinValue, maxValue: request.MaxValue);

            await _dbContext.Characteristics.AddAsync(characteristic);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return characteristic.Id;
        }
    }
}
