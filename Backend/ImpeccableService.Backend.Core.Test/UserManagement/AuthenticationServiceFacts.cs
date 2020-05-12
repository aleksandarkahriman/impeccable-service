using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Test.UserManagement.Provider;
using ImpeccableService.Backend.Core.UserManagement;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Core.UserManagement.Error;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Logger.Abstraction;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Utility.Application.ResultContract;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.Core.Test.UserManagement
{
    public class AuthenticationServiceFacts
    {
        public class RegisterWithEmailMethod
        {
            private static readonly User ValidUser = UserModelProvider.ConstructTestUser();

            private readonly Mock<IUserRepository> _userRepositoryMock;

            private readonly AuthenticationService _authenticationService;

            public RegisterWithEmailMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddUserManagementStubs();
                services.AddTestLogger(testOutputHelper);

                _userRepositoryMock = new Mock<IUserRepository>();
                services.AddSingleton(builder => _userRepositoryMock.Object);

                var provider = services.BuildServiceProvider();

                _authenticationService = provider.GetRequiredService<AuthenticationService>();
            }

            [Fact]
            public async Task ReturnsErrorIfEmailIsAlreadyRegistered()
            {
                // Arrange
                var emailRegistrationRequest = new RequestContextWithModel<EmailRegistration>(new EmailRegistration("user@domain.com", "password"));
                _userRepositoryMock.Setup(mock => mock.UserWithEmailExists(emailRegistrationRequest.Model.Email))
                    .ReturnsAsync(new ResultWithData<bool>(true));

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailRegistrationRequest);

                // Assert
                Assert.Equal(RegisterWithEmailException.ErrorCause.EmailExists, ((RegisterWithEmailException)result.ErrorReason).Cause);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromUserRepositoryEmailCheck()
            {
                // Arrange
                var emailRegistrationRequest = new RequestContextWithModel<EmailRegistration>(new EmailRegistration("user@domain.com", "password"));
                var expectedError = new Exception("Email check failed.");
                _userRepositoryMock.Setup(mock => mock.UserWithEmailExists(emailRegistrationRequest.Model.Email))
                    .ReturnsAsync(new ResultWithData<bool>(expectedError));

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailRegistrationRequest);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }

            [Fact]
            public async Task CreatesAndSavesNewUser()
            {
                // Arrange
                var emailRegistrationRequest = new RequestContextWithModel<EmailRegistration>(new EmailRegistration("user@domain.com", "password"));
                _userRepositoryMock.Setup(mock => mock.UserWithEmailExists(emailRegistrationRequest.Model.Email))
                    .ReturnsAsync(new ResultWithData<bool>(false));

                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<User>()))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));
            
                // Act
                await _authenticationService.RegisterWithEmail(emailRegistrationRequest);
            
                // Assert
                _userRepositoryMock.Verify(mock => mock.Create(It.IsAny<User>()), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromUserRepositorySave()
            {
                // Arrange
                var emailRegistrationRequest = new RequestContextWithModel<EmailRegistration>(new EmailRegistration("user@domain.com", "password"));
                _userRepositoryMock.Setup(mock => mock.UserWithEmailExists(emailRegistrationRequest.Model.Email))
                    .ReturnsAsync(new ResultWithData<bool>(false));

                var expectedError = new Exception("Email check failed.");
                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<User>()))
                    .ReturnsAsync(new ResultWithData<User>(expectedError));

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailRegistrationRequest);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }
        }

        public class LoginWithEmailMethod
        {
            private const string PasswordSalt = "lS6yIkXxqKHnkHdXZFwQBg==";

            private const string SecurityCredentialsSecret = "SuperSecureWellGuardedSecret";

            private static readonly User ValidUser = UserModelProvider.ConstructTestUser();

            private readonly Mock<IUserRepository> _userRepositoryMock;

            private readonly Mock<ISecurityEnvironmentVariables> _securityEnvironmentVariablesMock;

            private readonly AuthenticationService _authenticationService;

            private readonly ILogger<LoginWithEmailMethod> _logger;

            public LoginWithEmailMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _userRepositoryMock = new Mock<IUserRepository>();
                services.AddSingleton(builder => _userRepositoryMock.Object);

                _securityEnvironmentVariablesMock = new Mock<ISecurityEnvironmentVariables>();
                services.AddSingleton(builder => _securityEnvironmentVariablesMock.Object);

                var provider = services.BuildServiceProvider();

                _authenticationService = provider.GetRequiredService<AuthenticationService>();
                _logger = provider.GetRequiredService<ILogger<LoginWithEmailMethod>>();
            }

            [Fact]
            public async Task FindsUserMatchingEmailAndPasswordHash()
            {
                // Arrange
                var emailLoginRequest = new RequestContextWithModel<EmailLogin>(new EmailLogin("user@domain.com", "password"));

                _securityEnvironmentVariablesMock.Setup(mock => mock.PasswordHashSalt())
                    .Returns(PasswordSalt);

                var passwordHash = HashPassword(emailLoginRequest.Model.Password);

                _userRepositoryMock.Setup(mock => mock.Read(emailLoginRequest.Model.Email, passwordHash))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<Authentication>()))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.LoginWithEmail(emailLoginRequest);

                // Assert
                _userRepositoryMock.Verify(mock => mock.Read(emailLoginRequest.Model.Email, passwordHash), Times.AtLeastOnce);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromUserRepositoryRead()
            {
                // Arrange
                var emailLoginRequest = new RequestContextWithModel<EmailLogin>(new EmailLogin("user@domain.com", "password"));

                _securityEnvironmentVariablesMock.Setup(mock => mock.PasswordHashSalt())
                    .Returns(PasswordSalt);

                var passwordHash = HashPassword(emailLoginRequest.Model.Password);

                var expectedError = new Exception("User doesn't exist.");
                _userRepositoryMock.Setup(mock => mock.Read(emailLoginRequest.Model.Email, passwordHash))
                    .ReturnsAsync(new ResultWithData<User>(expectedError));

                // Act
                var result = await _authenticationService.LoginWithEmail(emailLoginRequest);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }

            [Fact]
            public async Task ReturnsValidAccessTokenIfEverythingGoesOk()
            {
                // Arrange
                var emailLoginRequest = new RequestContextWithModel<EmailLogin>(new EmailLogin("user@domain.com", "password"));

                _securityEnvironmentVariablesMock.Setup(mock => mock.PasswordHashSalt())
                    .Returns(PasswordSalt);

                _userRepositoryMock.Setup(mock => mock.Read(emailLoginRequest.Model.Email, It.IsAny<string>()))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<Authentication>()))
                    .ReturnsAsync(Result.Ok);

                // Act
                var result = await _authenticationService.LoginWithEmail(emailLoginRequest);

                // Assert
                var handler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SuperSecureWellGuardedSecret")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
                var claims = handler.ValidateToken(result.Data.AccessToken, tokenValidationParameters, out _);
                Assert.True(claims.Identity.IsAuthenticated);
            }

            [Fact]
            public async Task ReturnsRefreshAndLogoutTokensAlongWithAccessToken()
            {
                // Arrange
                var emailLoginRequest = new RequestContextWithModel<EmailLogin>(new EmailLogin("user@domain.com", "password"));

                _securityEnvironmentVariablesMock.Setup(mock => mock.PasswordHashSalt())
                    .Returns(PasswordSalt);

                _userRepositoryMock.Setup(mock => mock.Read(emailLoginRequest.Model.Email, It.IsAny<string>()))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<Authentication>()))
                    .ReturnsAsync(Result.Ok);

                // Act
                var result = await _authenticationService.LoginWithEmail(emailLoginRequest);

                // Assert
                Assert.NotEmpty(result.Data.RefreshToken);
                Assert.NotEmpty(result.Data.LogoutToken);
            }

            [Fact]
            public async Task SavesAuthentication()
            {
                // Arrange
                var emailLoginRequest = new RequestContextWithModel<EmailLogin>(new EmailLogin("user@domain.com", "password"));

                _securityEnvironmentVariablesMock.Setup(mock => mock.PasswordHashSalt())
                    .Returns(PasswordSalt);

                _userRepositoryMock.Setup(mock => mock.Read(emailLoginRequest.Model.Email, It.IsAny<string>()))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<Authentication>()))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.LoginWithEmail(emailLoginRequest);

                // Assert
                _userRepositoryMock.Verify(mock => mock.Create(It.IsAny<Authentication>()), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromAuthenticationSave()
            {
                // Arrange
                var emailLoginRequest = new RequestContextWithModel<EmailLogin>(new EmailLogin("user@domain.com", "password"));

                _securityEnvironmentVariablesMock.Setup(mock => mock.PasswordHashSalt())
                    .Returns(PasswordSalt);

                _userRepositoryMock.Setup(mock => mock.Read(emailLoginRequest.Model.Email, It.IsAny<string>()))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                var expectedError = new Exception("Failed to save authentication.");
                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<Authentication>()))
                    .ReturnsAsync(new Result(expectedError));

                // Act
                var result = await _authenticationService.LoginWithEmail(emailLoginRequest);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }

            private string HashPassword(string password)
            {
                const string salt = PasswordSalt;

                _logger.Info($"Salt for password hash is: {salt}");
            
                var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: Convert.FromBase64String(salt),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                _logger.Info($"Hashed password is {hashedPassword}");

                return hashedPassword;
            }
        }

        public class RefreshTokenMethod
        {
            private const string SecurityCredentialsSecret = "SuperSecureWellGuardedSecret";

            private const string InvalidRefreshToken =
                "EomDBo1erZH+2590rE3CYaZfWUnSOyg0Xqjs/A88KDbinw8wgdYKvY2G72JtRdOT1YB64FGzQ==";

            private const string ValidRefreshToken =
                "EomDBo1erZH+2590rE3CYaZfWUnSOytku0Amd43Qcuyg0Xqjs/A88KDbinw8wgdYKvY2G72JtRdOT1YB64FGzQ==";

            private static readonly User ValidUser = UserModelProvider.ConstructTestUser();

            private readonly Mock<IUserRepository> _userRepositoryMock;

            private readonly Mock<ISecurityEnvironmentVariables> _securityEnvironmentVariablesMock;

            private readonly AuthenticationService _authenticationService;

            public RefreshTokenMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _userRepositoryMock = new Mock<IUserRepository>();
                services.AddSingleton(builder => _userRepositoryMock.Object);

                _securityEnvironmentVariablesMock = new Mock<ISecurityEnvironmentVariables>();
                services.AddSingleton(builder => _securityEnvironmentVariablesMock.Object);

                var provider = services.BuildServiceProvider();

                _authenticationService = provider.GetRequiredService<AuthenticationService>();
            }

            [Fact]
            public async Task ReturnsErrorIfTheRefreshTokenDoesNotExist()
            {
                // Arrange
                var refreshTokenRequest = new RequestContextWithModel<RefreshToken>(new RefreshToken(InvalidRefreshToken));
                var expectedError = new Exception("User with token not found.");
                _userRepositoryMock.Setup(mock => mock.ReadByRefreshToken(InvalidRefreshToken))
                    .ReturnsAsync(new ResultWithData<User>(expectedError));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                // Act
                var result = await _authenticationService.RefreshToken(refreshTokenRequest);
            
                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }

            [Fact]
            public async Task GeneratesNewNonEmptyRefreshToken()
            {
                // Arrange
                var refreshTokenRequest = new RequestContextWithModel<RefreshToken>(new RefreshToken(ValidRefreshToken));
                
                _userRepositoryMock.Setup(mock => mock.ReadByRefreshToken(ValidRefreshToken))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<Authentication>()))
                    .ReturnsAsync(Result.Ok);

                // Act
                var result = await _authenticationService.RefreshToken(refreshTokenRequest);

                // Assert
                Assert.NotEmpty(result.Data.RefreshToken);
                Assert.NotEqual(ValidRefreshToken, result.Data.RefreshToken);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromAuthenticationSave()
            {
                // Arrange
                var refreshTokenRequest = new RequestContextWithModel<RefreshToken>(new RefreshToken(ValidRefreshToken));

                _userRepositoryMock.Setup(mock => mock.ReadByRefreshToken(ValidRefreshToken))
                    .ReturnsAsync(new ResultWithData<User>(ValidUser));

                _securityEnvironmentVariablesMock.Setup(mock => mock.SecurityCredentialsSecret())
                    .ReturnsAsync(SecurityCredentialsSecret);

                var expectedError = new Exception("Failed to save authentication.");
                _userRepositoryMock.Setup(mock => mock.Create(It.IsAny<Authentication>()))
                    .ReturnsAsync(new Result(expectedError));

                // Act
                var result = await _authenticationService.RefreshToken(refreshTokenRequest);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }
        }
    }
}
