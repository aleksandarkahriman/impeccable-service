using System;
using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Dependency;
using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Logger.Abstraction;
using Utility.Application.Extension;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRemoteRepository _authenticationRemoteRepository;
        private readonly ILogger _logger;

        public AuthenticationService(
            IAuthenticationRemoteRepository authenticationRemoteRepository,
            ILogger logger)
        {
            _authenticationRemoteRepository = authenticationRemoteRepository;
            _logger = logger;
        }

        public async Task<Result> RegisterWithEmail(EmailCredentials emailCredentials)
        {
            var emailValidationResult = emailCredentials.Email.IsValidEmail();
            if (!emailValidationResult.Success)
            {
                _logger.Warning<AuthenticationService>(emailValidationResult.ErrorReason, "Email validation failed.");
                return new ResultWithData<User>(emailValidationResult.ErrorReason);
            }

            const int minimalPasswordLength = 5;
            if (emailCredentials.Password.Length < minimalPasswordLength)
            {
                _logger.Warning<AuthenticationService>("Password validation failed.");
                return new ResultWithData<User>(new ArgumentException("Password must be at least 5 characters long."));
            }

            var registrationResult = await _authenticationRemoteRepository.RegisterWithEmail(emailCredentials);
            if (!registrationResult.Success)
            {
                _logger.Warning<AuthenticationService>(registrationResult.ErrorReason, "Registration failed.");
                return new ResultWithData<User>(registrationResult.ErrorReason);
            }

            return Result.Ok();
        }
    }
}
