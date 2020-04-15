using System;
using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement;
using ImpeccableService.Client.Core.UserManagement.Dependency;
using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Utility.Application.ResultContract;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Client.Core.Test.UserManagement
{
    public class AuthenticationServiceFacts
    {
        public class RegisterWithEmailMethod
        {
            private readonly Mock<IAuthenticationRemoteRepository> _authenticationRemoteRepositoryMock;

            private readonly AuthenticationService _authenticationService;

            public RegisterWithEmailMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _authenticationRemoteRepositoryMock = new Mock<IAuthenticationRemoteRepository>();
                services.AddSingleton(builder => _authenticationRemoteRepositoryMock.Object);

                var provider = services.BuildServiceProvider();

                _authenticationService = provider.GetRequiredService<AuthenticationService>();
            }

            [Theory]
            [InlineData("@user@gmail.com")]
            [InlineData("email")]
            [InlineData(".test@domain.com")]
            public async Task ReturnsErrorIfEmailIsInvalid(string invalidEmail)
            {
                // Arrange
                var emailRegistration = new EmailRegistration(invalidEmail, "password");

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailRegistration);

                // Assert
                Assert.True(result.State == ResultState.Error);
            }

            [Theory]
            [InlineData("")]
            [InlineData("pass")]
            public async Task ReturnsErrorIfPasswordIsInvalid(string invalidPassword)
            {
                // Arrange
                var emailRegistration = new EmailRegistration("user@domain.com", invalidPassword);

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailRegistration);

                // Assert
                Assert.True(result.State == ResultState.Error);
            }

            [Fact]
            public async Task RegistersUserRemotely()
            {
                // Arrange
                var emailRegistration = new EmailRegistration("user@domain.com", "password");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RegisterWithEmail(emailRegistration))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.RegisterWithEmail(emailRegistration);

                // Assert
                _authenticationRemoteRepositoryMock.Verify(mock => mock.RegisterWithEmail(emailRegistration), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromRemoteRepositoryRegistration()
            {
                // Arrange
                var emailRegistration = new EmailRegistration("user@domain.com", "password");
                var expectedError = new Exception("User already registered.");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RegisterWithEmail(emailRegistration))
                    .ReturnsAsync(new ResultWithData<User>(expectedError));

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailRegistration);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }
        }

        public class LoginWithEmailMethod
        {
            private static readonly AuthenticatedUser ValidUser =
                new AuthenticatedUser(1, new SecurityCredentials("accessToken", "refreshToken", "logoutToken"));

            private readonly Mock<IAuthenticationRemoteRepository> _authenticationRemoteRepositoryMock;

            private readonly Mock<IUserSecureRepository> _userSecureRepositoryMock;

            private readonly AuthenticationService _authenticationService;

            public LoginWithEmailMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _authenticationRemoteRepositoryMock = new Mock<IAuthenticationRemoteRepository>();
                services.AddSingleton(builder => _authenticationRemoteRepositoryMock.Object);

                _userSecureRepositoryMock = new Mock<IUserSecureRepository>();
                services.AddSingleton(builder => _userSecureRepositoryMock.Object);

                var provider = services.BuildServiceProvider();

                _authenticationService = provider.GetRequiredService<AuthenticationService>();
            }

            [Theory]
            [InlineData("@user@gmail.com")]
            [InlineData("email")]
            [InlineData(".test@domain.com")]
            public async Task ReturnsErrorIfEmailIsInvalid(string invalidEmail)
            {
                // Arrange
                var emailLogin = new EmailLogin(invalidEmail, "password");

                // Act
                var result = await _authenticationService.LoginWithEmail(emailLogin);

                // Assert
                Assert.True(result.State == ResultState.Error);
            }

            [Fact]
            public async Task AuthenticatesUserRemotely()
            {
                // Arrange
                var emailLogin = new EmailLogin("user@domain.com", "password");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.LoginWithEmail(emailLogin))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(ValidUser));
                _userSecureRepositoryMock.Setup(mock => mock.Write(ValidUser))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.LoginWithEmail(emailLogin);

                // Assert
                _authenticationRemoteRepositoryMock.Verify(mock => mock.LoginWithEmail(emailLogin), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromRemoteRepositoryLogin()
            {
                // Arrange
                var emailLogin = new EmailLogin("user@domain.com", "password");
                var expectedError = new Exception("User not found.");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.LoginWithEmail(emailLogin))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(expectedError));

                // Act
                var result = await _authenticationService.LoginWithEmail(emailLogin);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }

            [Fact]
            public async Task SavesUserInSecureStorage()
            {
                // Arrange
                var emailLogin = new EmailLogin("user@domain.com", "password");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.LoginWithEmail(emailLogin))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(ValidUser));
                _userSecureRepositoryMock.Setup(mock => mock.Write(ValidUser))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.LoginWithEmail(emailLogin);

                // Assert
                _userSecureRepositoryMock.Verify(mock => mock.Write(ValidUser), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromSecureRepositoryWrite()
            {
                // Arrange
                var emailLogin = new EmailLogin("user@domain.com", "password");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.LoginWithEmail(emailLogin))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(ValidUser));
                var expectedError = new Exception("User write failed.");
                _userSecureRepositoryMock.Setup(mock => mock.Write(ValidUser))
                    .ReturnsAsync(new Result(expectedError));

                // Act
                var result = await _authenticationService.LoginWithEmail(emailLogin);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }
        }

        public class RefreshTokenMethod
        {
            private static readonly AuthenticatedUser ValidUser =
                new AuthenticatedUser(1, new SecurityCredentials("accessToken", "refreshToken", "logoutToken"));

            private static readonly AuthenticatedUser RefreshedValidUser =
                new AuthenticatedUser(1, new SecurityCredentials("refreshedAccessToken", "refreshToken", "logoutToken"));

            private readonly Mock<IAuthenticationRemoteRepository> _authenticationRemoteRepositoryMock;

            private readonly Mock<IUserSecureRepository> _userSecureRepositoryMock;

            private readonly AuthenticationService _authenticationService;

            public RefreshTokenMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _authenticationRemoteRepositoryMock = new Mock<IAuthenticationRemoteRepository>();
                services.AddSingleton(builder => _authenticationRemoteRepositoryMock.Object);

                _userSecureRepositoryMock = new Mock<IUserSecureRepository>();
                services.AddSingleton(builder => _userSecureRepositoryMock.Object);

                var provider = services.BuildServiceProvider();

                _authenticationService = provider.GetRequiredService<AuthenticationService>();
            }

            [Fact]
            public async Task RequestsTokenRefreshRemotely()
            {
                // Arrange
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RefreshToken(ValidUser))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(RefreshedValidUser));
                _userSecureRepositoryMock.Setup(mock => mock.Write(RefreshedValidUser))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.RefreshToken(ValidUser);

                // Assert
                _authenticationRemoteRepositoryMock.Verify(mock => mock.RefreshToken(ValidUser), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromRemoteRepositoryRefresh()
            {
                // Arrange
                var expectedError = new Exception("Token refresh failed.");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RefreshToken(ValidUser))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(expectedError));

                // Act
                var result = await _authenticationService.RefreshToken(ValidUser);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }

            [Fact]
            public async Task SavesUserInSecureStorage()
            {
                // Arrange
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RefreshToken(ValidUser))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(RefreshedValidUser));
                _userSecureRepositoryMock.Setup(mock => mock.Write(RefreshedValidUser))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.RefreshToken(ValidUser);

                // Assert
                _userSecureRepositoryMock.Verify(mock => mock.Write(RefreshedValidUser), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromSecureRepositoryWrite()
            {
                // Arrange
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RefreshToken(ValidUser))
                    .ReturnsAsync(new ResultWithData<AuthenticatedUser>(RefreshedValidUser));
                var expectedError = new Exception("User write failed.");
                _userSecureRepositoryMock.Setup(mock => mock.Write(RefreshedValidUser))
                    .ReturnsAsync(new Result(expectedError));

                // Act
                var result = await _authenticationService.RefreshToken(ValidUser);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }
        }

        public class LogoutMethod
        {
            private static readonly AuthenticatedUser ValidUser =
                new AuthenticatedUser(1, new SecurityCredentials("accessToken", "refreshToken", "logoutToken"));

            private readonly Mock<IUserSecureRepository> _userSecureRepositoryMock;

            private readonly AuthenticationService _authenticationService;

            public LogoutMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _userSecureRepositoryMock = new Mock<IUserSecureRepository>();
                services.AddSingleton(builder => _userSecureRepositoryMock.Object);

                var provider = services.BuildServiceProvider();

                _authenticationService = provider.GetRequiredService<AuthenticationService>();
            }

            [Fact]
            public async Task DeletesUserFromSecureRepository()
            {
                // Arrange
                _userSecureRepositoryMock.Setup(mock => mock.Delete(ValidUser))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.Logout(ValidUser);

                // Assert
                _userSecureRepositoryMock.Verify(mock => mock.Delete(ValidUser), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromSecureRepositoryDelete()
            {
                // Arrange
                var expectedError = new Exception("User deletion failed.");
                _userSecureRepositoryMock.Setup(mock => mock.Delete(ValidUser))
                    .ReturnsAsync(new Result(expectedError));

                // Act
                var result = await _authenticationService.Logout(ValidUser);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }
        }
    }
}
