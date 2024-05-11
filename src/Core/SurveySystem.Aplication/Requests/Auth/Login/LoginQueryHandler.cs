using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Auth;

namespace SurveySystem.Aplication.Requests.Auth.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>  
    {
        private readonly IDbContext _dbContext;
        private readonly IClaimsIdentityFactory _claimsIdentityFactory;
        private readonly IPasswordEncryptionService _passwordEncryptionService;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;


        public LoginQueryHandler(IDbContext dbContext, IClaimsIdentityFactory claimsIdentityFactory, 
            IPasswordEncryptionService passwordEncryptionService, ITokenAuthenticationService tokenAuthenticationService)
        {
            _dbContext = dbContext;
            _claimsIdentityFactory = claimsIdentityFactory;
            _passwordEncryptionService = passwordEncryptionService;
            _tokenAuthenticationService = tokenAuthenticationService;
        }


        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await _dbContext.Users.FirstOrDefaultAsync(
                x => x.Login == request.Login);

            if (user == null)
                throw new NotFoundException("Неверный логин или пароль");
            if (!_passwordEncryptionService.ValidatePassword(request.Password, user.PasswordHash))
                throw new BadDataException("Неверный логин или пароль");

            var claims = _claimsIdentityFactory.CreateClaimsIdentity(user);
            var token = _tokenAuthenticationService.CreateToken(claims, TokenTypes.Auth);

            return new AuthResponse(user.FullName, Enum.GetName(user.Role)!, token);
        }
    }
}
