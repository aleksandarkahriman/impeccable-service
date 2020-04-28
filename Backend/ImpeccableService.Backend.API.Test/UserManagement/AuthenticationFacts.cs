using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.UserManagement.Dto;
using Newtonsoft.Json;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.API.Test.UserManagement
{
    public class AuthenticationFacts
    {
        public class EmailRegistration : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            public EmailRegistration(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;

                _factory.ConfigureServices(services =>
                {
                    services.AddTestLogger(testOutputHelper);
                });
            }

            [Fact]
            public async Task ReturnsCreatedStatus()
            {
                // Arrange
                var client = _factory.CreateClient();

                var emailRegistration = new EmailRegistrationDto("user@domain.com", "password");
                var body = JsonConvert.SerializeObject(emailRegistration);
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync("/api/authentication/register", content);

                // Assert
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        public class EmailLogin : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            public EmailLogin(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;

                _factory.ConfigureServices(services =>
                {
                    services.AddTestLogger(testOutputHelper);
                });
            }

            [Fact]
            public async Task ReturnsOkStatus()
            {
                // Arrange
                var client = _factory.CreateClient();

                var emailLogin = new EmailLoginDto("user@domain.com", "password");
                var body = JsonConvert.SerializeObject(emailLogin);
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync("/api/authentication/login", content);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
