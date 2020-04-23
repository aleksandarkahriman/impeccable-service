using System.Threading.Tasks;
using Logger.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger _logger;

        public AuthenticationController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            _logger.Info<AuthenticationController>("User registration in progress.");
            return Created(string.Empty, null);
        }
    }
}