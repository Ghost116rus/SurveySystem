using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.Requests.MessageBroker.RegisterNewStudent
{
    public class RegisterNewStudentCommandHandler : IRequestHandler<RegisterNewStudentCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public RegisterNewStudentCommandHandler(IDbContext dbContext, IPasswordEncryptionService passwordEncryptionService)
        {
            _dbContext = dbContext;
            _passwordEncryptionService = passwordEncryptionService;
        }

        public async Task<Unit> Handle(RegisterNewStudentCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Users.AnyAsync(x => x.Login == request.Login))
                throw new BadDataException($"Пользователь с таким логином ({request.Login}) уже существует!");

            var user = new User(request.FullName, request.Login,
                _passwordEncryptionService.EncodePassword(request.Password), Role.Student);
            user.Id = request.Id;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
