using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userProfileResult = await _userService.GetProfile(new RequestContext(_mapper.Map<Identity>(User)));
            return userProfileResult.Success
                ? Ok(_mapper.Map<GetUserProfileDto>(userProfileResult.Data))
                : (IActionResult)BadRequest();
        }
    }
}
