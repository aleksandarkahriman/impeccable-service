using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement;
using ImpeccableService.Backend.Core.UserManagement.Model;
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
            var registrationResult = await _authenticationService.RegisterWithEmail(new RequestContextWithModel<EmailRegistration>(emailRegistration));

            return registrationResult.Success
                ? Created(string.Empty, null)
                : (IActionResult)BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationCredentialsDto>> Login(EmailLoginDto emailLoginDto)
        {
            _logger.Info($"Email login in progress for {emailLoginDto.Email}.");

            var emailLogin = _mapper.Map<EmailLogin>(emailLoginDto);
            var loginResult = await _authenticationService.LoginWithEmail(new RequestContextWithModel<EmailLogin>(emailLogin));

            var authenticationCredentials = _mapper.Map<AuthenticationCredentialsDto>(loginResult.Data);

            return Ok(authenticationCredentials);
        }
    }
}