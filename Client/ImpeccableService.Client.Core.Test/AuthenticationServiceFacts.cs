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

namespace ImpeccableService.Client.Core.Test
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
                var emailCredentials = new EmailCredentials(invalidEmail, "password");

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailCredentials);

                // Assert
                Assert.True(result.State == ResultState.Error);
            }

            [Theory]
            [InlineData("")]
            [InlineData("pass")]
            public async Task ReturnsErrorIfPasswordIsInvalid(string invalidPassword)
            {
                // Arrange
                var emailCredentials = new EmailCredentials("user@domain.com", invalidPassword);

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailCredentials);

                // Assert
                Assert.True(result.State == ResultState.Error);
            }

            [Fact]
            public async Task RegistersUserRemotely()
            {
                // Arrange
                var emailCredentials = new EmailCredentials("user@domain.com", "password");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RegisterWithEmail(emailCredentials))
                    .ReturnsAsync(Result.Ok);

                // Act
                await _authenticationService.RegisterWithEmail(emailCredentials);

                // Assert
                _authenticationRemoteRepositoryMock.Verify(mock => mock.RegisterWithEmail(emailCredentials), Times.Once);
            }

            [Fact]
            public async Task ForwardsErrorReturnedFromRemoteRepositoryRegistration()
            {
                // Arrange
                var emailCredentials = new EmailCredentials("user@domain.com", "password");
                var expectedError = new Exception("User already registered");
                _authenticationRemoteRepositoryMock.Setup(mock => mock.RegisterWithEmail(emailCredentials))
                    .ReturnsAsync(new ResultWithData<User>(expectedError));

                // Act
                var result = await _authenticationService.RegisterWithEmail(emailCredentials);

                // Assert
                Assert.Equal(expectedError, result.ErrorReason);
            }
        }
    }
}
