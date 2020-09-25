using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.API.Test.Environment.Data;
using ImpeccableService.Backend.API.Test.Utility;
using Newtonsoft.Json;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.API.Test.Offering
{
    public class MenuFacts
    {
        public class GetMenuForVenue : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            public GetMenuForVenue(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;
                
                _factory.ConfigureServices(services =>
                {
                    services.AddTestLogger(testOutputHelper);
                });
            }

            [Fact]
            public async Task ReturnsValidResponse()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestUser());
                
                // Act
                var response = await client.GetAsync("/api/venue/4ccb/menu");
                var responseBody = await response.Content.ReadAsStringAsync();
                var getMenuDto = JsonConvert.DeserializeObject<GetMenuDto>(responseBody);
                
                // Assert
                Assert.Empty(ValidationHelper.ValidateModel(getMenuDto));
            }

            [Fact]
            public async Task ReturnsNotFoundForAnInvalidVenue()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestUser());
                
                // Act
                var response = await client.GetAsync("/api/venue/8er2/menu");
                
                // Assert
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsMenuWithMultipleSections()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestUser());
                
                // Act
                var response = await client.GetAsync("/api/venue/4ccb/menu");
                var responseBody = await response.Content.ReadAsStringAsync();
                var getMenuDto = JsonConvert.DeserializeObject<GetMenuDto>(responseBody);

                // Assert
                Assert.NotEmpty(getMenuDto.Sections);
            }
            
            [Fact]
            public async Task ReturnsMenuWithMultipleItemsInBreakfastSection()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestUser());
                
                // Act
                var response = await client.GetAsync("/api/venue/4ccb/menu");
                var responseBody = await response.Content.ReadAsStringAsync();
                var getMenuDto = JsonConvert.DeserializeObject<GetMenuDto>(responseBody);

                // Assert
                Assert.NotEmpty(getMenuDto.Sections.FirstOrDefault(section => section.Name == "Breakfast").Items);
            }
            
            [Fact]
            public async Task ReturnsValidMenuItemImageUrls()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestUser());

                // Act
                var response = await client.GetAsync("/api/venue/4ccb/menu");
                var responseBody = await response.Content.ReadAsStringAsync();
                var getMenuDto =
                    JsonConvert.DeserializeObject<GetMenuDto>(responseBody);

                // Assert
                Assert.All(getMenuDto.Sections.SelectMany(section => section.Items), item =>
                {
                    Assert.Contains("Amz", item.Thumbnail.Url);
                    Assert.Contains("Credential", item.Thumbnail.Url);
                    Assert.Contains("Signature", item.Thumbnail.Url);
                });
            }
        }
    }
}