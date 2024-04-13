using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SurveySystem.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("SurveySystem/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
    }
}
