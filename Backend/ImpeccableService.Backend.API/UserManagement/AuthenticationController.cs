using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement;
using ImpeccableService.Domain.UserManagement;
using Logger.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IAuthenticationService authenticationService,
            IMapper mapper,
            ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(EmailRegistrationDto emailRegistrationDto)
        {
            _logger.Info("User registration in progress.");

            var emailRegistration = _mapper.Map<EmailRegistration>(emailRegistrationDto);
            var registrationResult = await _authenticationService.RegisterWithEmail(new RequestContext<EmailRegistration>(emailRegistration));

            return registrationResult.Success
                ? Created(string.Empty, null)
                : (IActionResult)BadRequest();
        }
    }
}