using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Characteristics.Delete
{
    public class DeleteCharacteristicCommandHandler : IRequestHandler<DeleteCharacteristicCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteCharacteristicCommandHandler(IDbContext ddContext)
        {
            _dbContext = ddContext;
        }

        public async Task<Unit> Handle(DeleteCharacteristicCommand command, CancellationToken cancellationToken)
        {
            var characteristic = await _dbContext.Characteristics
                .FirstOrDefaultAsync(x => x.Id == command.CharactetisticId) ?? throw new NotFoundException();

            _dbContext.Characteristics.Remove(characteristic);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
