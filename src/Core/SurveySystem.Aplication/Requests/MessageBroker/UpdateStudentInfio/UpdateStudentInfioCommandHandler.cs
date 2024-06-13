using MediatR;
using SurveySystem.Aplication.Interfaces;

namespace SurveySystem.Aplication.Requests.MessageBroker.UpdateStudentInfio
{
    public class UpdateStudentInfioCommandHandler : IRequestHandler<UpdateStudentInfioCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateStudentInfioCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Unit> Handle(UpdateStudentInfioCommand request, CancellationToken cancellationToken)
        {
            // Требует дальнейшей реализации
            throw new NotImplementedException();
        }
    }
}
