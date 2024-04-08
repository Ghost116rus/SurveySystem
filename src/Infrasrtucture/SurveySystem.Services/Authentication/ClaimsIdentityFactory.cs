using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using System.Security.Claims;

namespace SurveySystem.Services.Authentication
{
    /// <summary>
    /// Фабрика ClaimsPrincipal для пользователей.
    /// </summary>
    public class ClaimsIdentityFactory : IClaimsIdentityFactory
    {
        /// <inheritdoc/>
        public ClaimsIdentity CreateClaimsIdentity(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var claims = new List<Claim>
            {
                new(ClaimNames.UserId, user.Id.ToString(), ClaimValueTypes.String),
                new(ClaimNames.Login, user.Login, ClaimValueTypes.String),
                new(ClaimNames.Role, Enum.GetName(user.Role)!, ClaimValueTypes.String),
            };

            return new ClaimsIdentity(claims);
        }
    }
}
