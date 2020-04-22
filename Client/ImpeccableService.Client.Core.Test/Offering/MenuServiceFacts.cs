using System.Threading.Tasks;
using ImpeccableService.Client.Core.Offering;
using ImpeccableService.Client.Core.Offering.Dependency;
using ImpeccableService.Client.Core.Test.Offering.Provider;
using ImpeccableService.Domain.Offering;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Utility.Application.ResultContract;
using Utility.Test;
using Xunit;
using Xunit.Abstractions;

namespace ImpeccableService.Client.Core.Test.Offering
{
    public class MenuServiceFacts
    {
        public class GetMenuByIdMethod
        {
            private readonly Mock<IMenuRemoteRepository> _menuRemoteRepositoryMock;

            private readonly MenuService _menuService;

            public GetMenuByIdMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _menuRemoteRepositoryMock = new Mock<IMenuRemoteRepository>();
                services.AddSingleton(builder => _menuRemoteRepositoryMock.Object);

                var provider = services.BuildServiceProvider();
                _menuService = provider.GetRequiredService<MenuService>();
            }

            [Fact]
            public async Task ForwardsWhatRepositoryReturns()
            {
                // Arrange
                var expectedMenu = OfferingModelProvider.ConstructTestMenu();
                _menuRemoteRepositoryMock.Setup(mock => mock.GetMenuById(expectedMenu.Id))
                    .ReturnsAsync(new ResultWithData<Menu>(expectedMenu));

                // Act
                var result = await _menuService.GetMenuById(expectedMenu.Id);

                // Assert
                Assert.Equal(expectedMenu, result.Data);
            }
        }
    }
}
