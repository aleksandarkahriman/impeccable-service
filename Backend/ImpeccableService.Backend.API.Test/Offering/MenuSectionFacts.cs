using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.API.Test.Environment.Data;
using ImpeccableService.Backend.Database;
using ImpeccableService.Backend.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.API.Test.Offering
{
    public class MenuSectionFacts
    {
        public class CreateSectionForMenu : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;

            private readonly string _requestBody = JsonConvert.SerializeObject(new PostMenuSectionDto
            {
                Name = "Dinner"
            });

            public CreateSectionForMenu(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;

                _factory.ConfigureServices(services => { services.AddTestLogger(testOutputHelper); });
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
                var response = await client.PostAsync("/api/menu/5eds/section", body);
                
                // Assert
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
            
            [Theory]
            [ClassData(typeof(UsersThatDoNotWorkForCompany))]
            public async Task ReturnsForbiddenForIntruders(User user)
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(user);
                
                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/menu/5eds/section", requestContent);
                
                // Assert
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
            
            [Fact]
            public async Task ReturnsNotFoundForAnInvalidMenu()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserOne());
                
                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/menu/1nvl/section", requestContent);
                
                // Assert
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
            
            [Fact]
            public async Task ReturnCreatedIfSuccessful()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserOne());
                
                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/menu/5eds/section", requestContent);
                
                // Assert
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
            
            [Fact]
            public async Task PersistsSectionForMenu()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserOne());

                const string menuId = "5eds";

                using var scope = _factory.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var sectionsCount = await dbContext.MenuSections.CountAsync(section => section.MenuId == menuId);

                // Act
                var requestContent = new StringContent(_requestBody, Encoding.UTF8, "application/json");
                await client.PostAsync($"/api/menu/{menuId}/section", requestContent);

                // Assert
                var newSectionsCount = await dbContext.MenuSections.CountAsync(section => section.MenuId == menuId);
                Assert.True(newSectionsCount > sectionsCount);
            }

            private class UsersThatDoNotWorkForCompany : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator()
                {
                    yield return new object[] { TestUserRegistry.ValidTestProviderAdminUserWithoutCompany() };
                    yield return new object[] {TestUserRegistry.ValidTestProviderAdminUserTwo() };
                }
                
                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
        }
    }
}