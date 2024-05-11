using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Auth;

namespace SurveySystem.Aplication.Requests.Auth.Register
{
    /// <summary>
    /// Обработчик команды <see cref="RegisterStudentCommand"/>
    /// </summary>
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, AuthResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IPasswordEncryptionService _passwordEncryptionService;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IClaimsIdentityFactory _claimsIdentityFactory;

        public RegisterStudentCommandHandler(IDbContext dbContext, IPasswordEncryptionService passwordEncryptionService, ITokenAuthenticationService tokenAuthenticationService, IClaimsIdentityFactory claimsIdentityFactory)
        {
            _dbContext = dbContext;
            _passwordEncryptionService = passwordEncryptionService;
            _tokenAuthenticationService = tokenAuthenticationService;
            _claimsIdentityFactory = claimsIdentityFactory;
        }

        public async Task<AuthResponse> Handle(RegisterStudentCommand command, CancellationToken cancellationToken)
        {
            if (await _dbContext.Users.AnyAsync(x => x.Login == command.Login))
                throw new BadDataException($"Пользователь с таким логином ({command.Login}) уже существует!");

            var faculty = await _dbContext.Faculties.FirstOrDefaultAsync(x => x.Id == command.FacultyId);

            if (faculty is null)
                throw new NotFoundException("Кафедра не найдена");

            var user = new User(command.FullName, command.Login,
                _passwordEncryptionService.EncodePassword(command.Password), Role.Student);

            await _dbContext.Users.AddAsync(user);

            var student = new Domain.Entities.Users.Student(user, command.EducationLevel, command.GroupNumber, faculty, command.StartDateOfLearning);

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var claims = _claimsIdentityFactory.CreateClaimsIdentity(user);
            var token = _tokenAuthenticationService.CreateToken(claims, TokenTypes.Auth);

            return new AuthResponse(user.FullName, Enum.GetName(user.Role)!, token);
        }
    }
}
