using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.API.Test.Environment.Data;
using ImpeccableService.Backend.API.Test.Utility;
using ImpeccableService.Backend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
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
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
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
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
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
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
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
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());

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

        public class CreateMenuForVenue : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;
            
            private readonly string _requestBody = JsonConvert.SerializeObject(new PostMenuDto
            {
                Name = "Default"
            });

            public CreateMenuForVenue(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;
                
                _factory.ConfigureServices(services =>
                {
                    services.AddTestLogger(testOutputHelper);
                });
            }

            [Fact]
            public async Task ReturnsForbiddenForNonProviderAdminUser()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
                // Act
                var body = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue/4ccb/menu", body);
                
                // Assert
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsForbiddenForVenueThatIsNotOwnedByUser()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserWithoutCompany());
                
                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue/4ccb/menu", requestContent);
                
                // Assert
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsNotFoundForAnInvalidVenue()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUser());
                
                // Act
                var body = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue/8er2/menu", body);
                
                // Assert
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Fact]
            public async Task ReturnCreatedIfSuccessful()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUser());
                
                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue/4ccb/menu", requestContent);
                
                // Assert
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsValidEmptyMenuIfSuccessful()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUser());
                
                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue/4ccb/menu", requestContent);
                var responseBody = await response.Content.ReadAsStringAsync();
                var getMenuDto = JsonConvert.DeserializeObject<GetMenuDto>(responseBody);
                
                // Assert
                Assert.NotEmpty(getMenuDto.Id);
            }

            [Fact]
            public async Task PersistsMenuForVenue()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUser());

                const string venueId = "4ccb";

                using var scope = _factory.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var menuCount = await dbContext.Menus.CountAsync(menu => menu.VenueId == venueId);

                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                await client.PostAsync($"/api/venue/{venueId}/menu", requestContent);

                // Assert
                var newMenuCount = await dbContext.Menus.CountAsync(menu => menu.VenueId == venueId);
                Assert.True(newMenuCount > menuCount);
            }
        }
    }
}