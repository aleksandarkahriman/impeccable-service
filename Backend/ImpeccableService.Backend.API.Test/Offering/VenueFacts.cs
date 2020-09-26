using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.API.Test.Environment.Data;
using Newtonsoft.Json;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.API.Test.Offering
{
    public class VenueFacts
    {
        public class CreateVenue : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            public CreateVenue(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
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
                var body = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue", body);
                
                // Assert
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsBadRequestForNonCompleteData()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUser());
                
                // Act
                var body = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue", body);
                
                // Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsValidVenueIfSuccessful()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUser());
                
                // Act
                var postVenueDto = new PostVenueDto { Name = "Gentlemen" };
                var requestBody = JsonConvert.SerializeObject(postVenueDto);
                var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/venue", requestContent);
                var responseBody = await response.Content.ReadAsStringAsync();
                var getVenueDto = JsonConvert.DeserializeObject<GetVenueDto>(responseBody);
                
                // Assert
                Assert.NotEmpty(getVenueDto.Id);
            }
        }
    }
}