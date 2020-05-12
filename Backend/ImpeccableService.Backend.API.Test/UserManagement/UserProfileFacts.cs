using System.Threading.Tasks;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.API.UserManagement.Dto;
using Newtonsoft.Json;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.API.Test.UserManagement
{
    public class UserProfileFacts
    {
        public class GetProfile : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            public GetProfile(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;

                _factory.ConfigureServices(services =>
                {
                    services.AddTestLogger(testOutputHelper);
                });
            }

            [Fact]
            public async Task ReturnsCorrectEmailAddress()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestUser());

                // Act
                var response = await client.GetAsync("/api/user/me");
                var responseBody = await response.Content.ReadAsStringAsync();
                var getUserProfileDto =
                    JsonConvert.DeserializeObject<GetUserProfileDto>(responseBody);

                // Assert
                Assert.Equal("frank@gmail.com", getUserProfileDto.Email);
            }
        }
    }
}
