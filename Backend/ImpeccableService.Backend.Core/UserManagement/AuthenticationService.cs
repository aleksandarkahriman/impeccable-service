using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Core.UserManagement.Error;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Logger.Abstraction;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    internal class AuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IdentitySecurityFactory _identitySecurityFactory;
        private readonly ILogger _logger;

        public AuthenticationService(
            IUserRepository userRepository,
            IdentitySecurityFactory identitySecurityFactory,
            ILogger logger)
        {
            _userRepository = userRepository;
            _identitySecurityFactory = identitySecurityFactory;
            _logger = logger;
        }

        public async Task<Result> RegisterWithEmail(RequestContext<EmailRegistration> emailRegistrationRequest)
        {
            var emailRegistration = emailRegistrationRequest.Model;

            var userExistsResult = await _userRepository.UserWithEmailExists(emailRegistration.Email);
            if (userExistsResult.Failure)
            {
                _logger.Warning<AuthenticationService>(userExistsResult.ErrorReason,
                    $"User email existence check failed for user with email {emailRegistration.Email}.");
                return new Result(userExistsResult.ErrorReason);
            }

            if (userExistsResult.Data)
            {
                _logger.Info<AuthenticationService>($"User with email {emailRegistration.Email} already exists.");
                return new Result(new RegisterWithEmailException(RegisterWithEmailException.ErrorCause.EmailExists));
            }

            var createResult = await _userRepository.Save(emailRegistration);
            if (createResult.Failure)
            {
                _logger.Warning<AuthenticationService>(createResult.ErrorReason, $"Creation of user with email {emailRegistration.Email} failed.");
                return new Result(createResult.ErrorReason);
            }
            
            return Result.Ok();
        }

        public async Task<ResultWithData<SecurityCredentials>> LoginWithEmail(RequestContext<EmailLogin> emailLoginRequest)
        {
            var emailLogin = emailLoginRequest.Model;

            var passwordHash = _identitySecurityFactory.HashPassword(emailLogin.Password);

            var userResult = await _userRepository.Read(emailLogin.Email, passwordHash);
            if (userResult.Failure)
            {
                _logger.Warning<AuthenticationService>(userResult.ErrorReason, "User not found.");
                return new ResultWithData<SecurityCredentials>(userResult.ErrorReason);
            }

            var user = userResult.Data;

            return await GenerateAndPersistNewSecurityCredentials(user);
        }

        

        public async Task<ResultWithData<SecurityCredentials>> RefreshToken(RequestContext<RefreshToken> refreshTokenRequest)
        {
            var refreshToken = refreshTokenRequest.Model;

            var userResult = await _userRepository.ReadByRefreshToken(refreshToken.Token);
            if (userResult.Failure)
            {
                _logger.Warning<AuthenticationService>(userResult.ErrorReason, "User not found.");
                return new ResultWithData<SecurityCredentials>(userResult.ErrorReason);
            }

            var user = userResult.Data;

            return await GenerateAndPersistNewSecurityCredentials(user);
        }

        private async Task<ResultWithData<SecurityCredentials>> GenerateAndPersistNewSecurityCredentials(User user)
        {
            var generateCredentialsResult = await _identitySecurityFactory.GenerateCredentials(user);
            if (generateCredentialsResult.Failure)
            {
                _logger.Warning<AuthenticationService>(generateCredentialsResult.ErrorReason,
                    $"Failed to generate credentials for user {user.Id}");
                return new ResultWithData<SecurityCredentials>(generateCredentialsResult.ErrorReason);
            }

            var securityCredentials = generateCredentialsResult.Data;

            var authentication = new Authentication(user, securityCredentials);
            var authenticationSaveResult = await _userRepository.Save(authentication);
            if (authenticationSaveResult.Failure)
            {
                _logger.Warning<AuthenticationService>(authenticationSaveResult.ErrorReason, "Failed to save authentication.");
                return new ResultWithData<SecurityCredentials>(authenticationSaveResult.ErrorReason);
            }

            return new ResultWithData<SecurityCredentials>(generateCredentialsResult.Data);
        }
    }
}