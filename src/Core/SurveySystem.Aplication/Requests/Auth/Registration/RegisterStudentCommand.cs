using MediatR;
using SurveySystem.Requests.Auth;

namespace SurveySystem.Aplication.Requests.Auth.Register
{
    /// <inheritdoc/>
    public class RegisterStudentCommand : RegisterStudentRequest, IRequest<AuthResponse>
    {
    }
}
