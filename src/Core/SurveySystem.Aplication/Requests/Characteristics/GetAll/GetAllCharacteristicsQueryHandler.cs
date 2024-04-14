using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Requests.Characteristic;
using SurveySystem.Requests.Characteristic.GetAll;

namespace SurveySystem.Aplication.Requests.Characteristics.GetAll
{
    public class GetAllCharacteristicsQueryHandler : IRequestHandler<GetAllCharacteristicsQuery, GetAllCharacteristicResponse>
    {
        private readonly IDbContext _dbContext;

        public GetAllCharacteristicsQueryHandler(IDbContext dbContext)
            => _dbContext = dbContext;
        
        public async Task<GetAllCharacteristicResponse> Handle(GetAllCharacteristicsQuery query, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var characteristics = await _dbContext.Characteristics.Select(x => new CharacteristicDTO()
            {
                Id = x.Id,
                Description = x.Description,
                CharacteristicType = x.CharacteristicType,
                CharacteristicTypeDescription = Enum.GetName(x.CharacteristicType)!,
                MinValue = x.MinValue,
                MaxValue = x.MaxValue,

            }).ToListAsync(cancellationToken);

            return new GetAllCharacteristicResponse() { Characteristics = characteristics };
        }
    }
}
