using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok();
        }
    }
}
