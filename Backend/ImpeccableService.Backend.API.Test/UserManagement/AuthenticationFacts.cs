using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.Test.Environment;
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
                var requestBody = JsonConvert.SerializeObject(emailRegistration);
                var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync("/api/authentication/register", requestContent);

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

                var emailLogin = new EmailLoginDto("frank@gmail.com", "12345678");
                var requestBody = JsonConvert.SerializeObject(emailLogin);
                var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync("/api/authentication/login", requestContent);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsValidJwtAccessToken()
            {
                // Arrange
                var client = _factory.CreateClient();

                var emailLogin = new EmailLoginDto("frank@gmail.com", "12345678");
                var requestBody = JsonConvert.SerializeObject(emailLogin);
                var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync("/api/authentication/login", requestContent);
                var responseBody = await response.Content.ReadAsStringAsync();
                var authenticationCredentials =
                    JsonConvert.DeserializeObject<AuthenticationCredentialsDto>(responseBody);

                // Assert
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(authenticationCredentials.AccessToken);
                Assert.True(DateTime.UtcNow < token.ValidTo);
            }
        }

        public class AuthenticationMiddleware : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            public AuthenticationMiddleware(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;

                _factory.ConfigureServices(services =>
                {
                    services.AddTestLogger(testOutputHelper);
                });
            }

            [Fact]
            public async Task ReturnsUnauthorizedIfNoAccessTokenIsProvided()
            {
                // Arrange
                var client = _factory.CreateClient();

                // Act
                var response = await client.GetAsync("/api/user/me");

                // Assert
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }

            [Fact]
            public async Task LetsTheRequestPassIfValidAccessTokenIsProvided()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestUser());

                // Act
                var response = await client.GetAsync("/api/user/me");

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsUnauthorizedIfInvalidAccessTokenIsProvided()
            {
                // Arrange
                var client = _factory.CreateClient();
                const string expiredTokenWithNoIssuerOrAudience = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJwcmltYXJ5c2lkIjoiMSIsIm5iZiI6MTU4OTExNTE2OCwiZXhwIjoxNTg5MTE1MjI4LCJpYXQiOjE1ODkxMTUxNjh9.1LvUdjX8q_Qb9bwaLEXCpWBxXM2-pszzeURuKU2sVOU";
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", expiredTokenWithNoIssuerOrAudience);

                // Act
                var response = await client.GetAsync("/api/user/me");

                // Assert
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}
