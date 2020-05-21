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
                Assert.Equal(TestUserRegistry.ValidTestUser().Email, getUserProfileDto.Email);
            }

            [Fact]
            public async Task ReturnsCorrectRole()
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
                Assert.Equal(TestUserRegistry.ValidTestUser().Role, getUserProfileDto.Role);
            }

            [Fact]
            public async Task ReturnsValidProfileImageUrl()
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
                Assert.Contains("Amz", getUserProfileDto.ProfileImage.Url);
                Assert.Contains("Credential", getUserProfileDto.ProfileImage.Url);
                Assert.Contains("Signature", getUserProfileDto.ProfileImage.Url);
            }
        }
    }
}
