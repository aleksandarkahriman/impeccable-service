using System;
using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Dependency;
using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Client.Domain.UserManagement;
using Logger.Abstraction;
using Utility.Application.Extension;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement
{
    internal class AuthenticationService
    {
        private readonly IAuthenticationRemoteRepository _authenticationRemoteRepository;
        private readonly IUserSecureRepository _userSecureRepository;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IAuthenticationRemoteRepository authenticationRemoteRepository,
            IUserSecureRepository userSecureRepository,
            ILogger<AuthenticationService> logger)
        {
            _authenticationRemoteRepository = authenticationRemoteRepository;
            _userSecureRepository = userSecureRepository;
            _logger = logger;
        }

        public async Task<Result> RegisterWithEmail(EmailRegistration emailRegistration)
        {
            var emailValidationResult = emailRegistration.Email.IsValidEmail();
            if (emailValidationResult.Failure)
            {
                _logger.Warning(emailValidationResult.ErrorReason, "Email validation failed.");
                return new ResultWithData<User>(emailValidationResult.ErrorReason);
            }

            const int minimalPasswordLength = 5;
            if (emailRegistration.Password.Length < minimalPasswordLength)
            {
                _logger.Warning("Password validation failed.");
                return new ResultWithData<User>(new ArgumentException("Password must be at least 5 characters long."));
            }

            var registrationResult = await _authenticationRemoteRepository.RegisterWithEmail(emailRegistration);
            if (registrationResult.Failure)
            {
                _logger.Warning(registrationResult.ErrorReason, "Registration failed.");
                return new ResultWithData<User>(registrationResult.ErrorReason);
            }

            return Result.Ok();
        }

        public async Task<ResultWithData<AuthenticatedUser>> LoginWithEmail(EmailLogin emailLogin)
        {
            var emailValidationResult = emailLogin.Email.IsValidEmail();
            if (emailValidationResult.Failure)
            {
                _logger.Warning(emailValidationResult.ErrorReason, "Email validation failed.");
                return new ResultWithData<AuthenticatedUser>(emailValidationResult.ErrorReason);
            }

            var loginResult = await _authenticationRemoteRepository.LoginWithEmail(emailLogin);
            if (loginResult.Failure)
            {
                _logger.Warning(loginResult.ErrorReason, "Login failed.");
                return new ResultWithData<AuthenticatedUser>(loginResult.ErrorReason);
            }

            var writeResult = await _userSecureRepository.Write(loginResult.Data);
            if (writeResult.Failure)
            {
                _logger.Warning(writeResult.ErrorReason, "Failed to write user.");
                return new ResultWithData<AuthenticatedUser>(writeResult.ErrorReason);
            }

            return loginResult;
        }
        
        public async Task<Result> RefreshToken(AuthenticatedUser user)
        {
            var refreshResult = await _authenticationRemoteRepository.RefreshToken(user);
            if (refreshResult.Failure)
            {
                _logger.Warning(refreshResult.ErrorReason, "Failed to refresh token.");
                return new Result(refreshResult.ErrorReason);
            }

            var writeResult = await _userSecureRepository.Write(refreshResult.Data);
            if (writeResult.Failure)
            {
                _logger.Warning(writeResult.ErrorReason, "Failed to write user.");
                return new Result(writeResult.ErrorReason);
            }

            return Result.Ok();
        }

        public async Task<Result> Logout(AuthenticatedUser user)
        {
            var deleteResult = await _userSecureRepository.Delete(user);
            if (deleteResult.Failure)
            {
                _logger.Warning(deleteResult.ErrorReason, "Failed to delete user.");
                return new Result(deleteResult.ErrorReason);
            }

            return Result.Ok();
        }
    }
}
