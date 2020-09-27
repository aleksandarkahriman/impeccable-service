using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.API.Test.Environment.Data;
using ImpeccableService.Backend.Database;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.API.Test.Offering
{
    public class VenueFacts
    {
        public class GetVenues : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            public GetVenues(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;

                _factory.ConfigureServices(services => { services.AddTestLogger(testOutputHelper); });
            }

            [Fact]
            public async Task ReturnsOk()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
                // Act
                var response = await client.GetAsync("/api/venue");
                
                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsListOfVenues()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
                // Act
                var response = await client.GetAsync("/api/venue");
                var responseBody = await response.Content.ReadAsStringAsync();
                var venues = JsonConvert.DeserializeObject<List<GetVenueDto>>(responseBody);
                
                // Assert
                Assert.NotEmpty(venues);
            }
        }

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

            [Fact]
            public async Task PersistsVenue()
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
                using var scope = _factory.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var venueEntity = dbContext.Venues.FirstOrDefault(venue => venue.Id == getVenueDto.Id);
                Assert.NotNull(venueEntity);
            }
        }
    }
}