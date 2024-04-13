using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SurveySystem.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("test/[controller]")]
    public class BaseController : ControllerBase
    {

    }
}
