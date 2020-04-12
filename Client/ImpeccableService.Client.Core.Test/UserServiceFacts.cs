using ImpeccableService.Client.Core.UserManagement;
using ImpeccableService.Client.Core.UserManagement.Model;
using Microsoft.Extensions.DependencyInjection;
using Utility.Application.ResultContract;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Client.Core.Test
{
    public class UserServiceFacts
    {
        public class RegisterWithEmailMethod
        {
            private readonly UserService _userService;

            public RegisterWithEmailMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);
                var provider = services.BuildServiceProvider();

                _userService = provider.GetRequiredService<UserService>();
            }

            [Fact]
            public void ReturnsErrorIfEmailIsInvalid()
            {
                // Arrange
                var emailRegistration = new EmailRegistration("user@gmail.com", "password");

                // Act
                var result = _userService.RegisterWithEmail(emailRegistration);

                // Assert
                Assert.True(result.State == ResultState.Error);
            }
        }
    }
}
