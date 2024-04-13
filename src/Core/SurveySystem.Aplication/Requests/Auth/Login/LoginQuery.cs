using MediatR;
using SurveySystem.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Auth.Login
{
    public class LoginQuery : LoginRequest, IRequest<AuthResponse>
    {
    }
}
