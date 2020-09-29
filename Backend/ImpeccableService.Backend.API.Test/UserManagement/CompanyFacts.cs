using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.API.Test.Environment.Data;
using ImpeccableService.Backend.API.Test.Utility;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Backend.API.Test.UserManagement
{
    public class CompanyFacts
    {
        public class CreateCompany : IClassFixture<EnvironmentFactory>
        {
            private readonly EnvironmentFactory _factory;
            
            private PostCompanyDto _postCompanyDto = new PostCompanyDto
            {
                Name = "McDonald's"
            };

            public CreateCompany(EnvironmentFactory factory, ITestOutputHelper testOutputHelper)
            {
                _factory = factory;

                _factory.ConfigureServices(services => { services.AddTestLogger(testOutputHelper); });
            }

            public string Request => JsonConvert.SerializeObject(_postCompanyDto);
            
            [Fact]
            public async Task ReturnsForbiddenForNonProviderAdminUser()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestConsumerUser());
                
                // Act
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/company", requestContent);
                
                // Assert
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
            
            [Fact]
            public async Task ReturnsBadRequestForNonCompleteData()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserOne());
                
                // Act
                var requestContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/company", requestContent);
                
                // Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Fact]
            public async Task ReturnsValidCompanyIfSuccessful()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserOne());
                
                // Act
                var requestContent = new StringContent(Request, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/company", requestContent);
                var responseBody = await response.Content.ReadAsStringAsync();
                var getCompanyDto = JsonConvert.DeserializeObject<GetCompanyDto>(responseBody);

                // Assert
                Assert.Empty(ValidationHelper.ValidateModel(getCompanyDto));
            }

            [Fact]
            public async Task PersistsCompany()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserOne());
                
                using var scope = _factory.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var companyCount = await dbContext.Companies.CountAsync();
                
                // Act
                var requestContent = new StringContent(Request, Encoding.UTF8, "application/json");
                await client.PostAsync("/api/company", requestContent);
                
                // Assert
                var newCompanyCount = await dbContext.Companies.CountAsync();
                Assert.Equal(companyCount + 1, newCompanyCount);
            }

            [Fact]
            public async Task GrantsCompanyOwnershipToTheProviderAdministratorThatCreatedIt()
            {
                // Arrange
                var client = await _factory
                    .CreateClient()
                    .Authenticate(TestUserRegistry.ValidTestProviderAdminUserOne());

                // Act
                var requestContent = new StringContent(Request, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/company", requestContent);
                var responseBody = await response.Content.ReadAsStringAsync();
                var getCompanyDto = JsonConvert.DeserializeObject<GetCompanyDto>(responseBody);
                
                // Assert
                using var scope = _factory.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var companyEntity = await dbContext.Companies.FirstOrDefaultAsync(company => company.Id == getCompanyDto.Id);
                Assert.Equal(TestUserRegistry.ValidTestProviderAdminUserOne().Id, companyEntity.OwnerId);
            }
        }
    }
}