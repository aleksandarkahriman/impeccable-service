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
    public class VenueServiceFacts
    {
        public class GetVenueByIdMethod
        {
            private readonly Mock<IVenueRemoteRepository> _venueRemoteRepositoryMock;

            private readonly VenueService _venueService;

            public GetVenueByIdMethod(ITestOutputHelper testOutputHelper)
            {
                var services = new ServiceCollection();
                services.AddCore();
                services.AddTestLogger(testOutputHelper);

                _venueRemoteRepositoryMock = new Mock<IVenueRemoteRepository>();
                services.AddSingleton(builder => _venueRemoteRepositoryMock.Object);

                var provider = services.BuildServiceProvider();
                _venueService = provider.GetRequiredService<VenueService>();
            }

            [Fact]
            public async Task ForwardsWhatRepositoryReturns()
            {
                // Arrange
                var expectedVenue = OfferingModelProvider.ConstructTestVenue();
                _venueRemoteRepositoryMock.Setup(mock => mock.GetVenueById(expectedVenue.Id))
                    .ReturnsAsync(new ResultWithData<Venue>(expectedVenue));

                // Act
                var result = await _venueService.GetVenueById(expectedVenue.Id);

                // Assert
                Assert.Equal(expectedVenue, result.Data);
            }

            [Fact]
            public async Task IncludesMenuPreviews()
            {
                // Arrange
                var expectedVenue = OfferingModelProvider.ConstructTestVenue();
                _venueRemoteRepositoryMock.Setup(mock => mock.GetVenueById(expectedVenue.Id))
                    .ReturnsAsync(new ResultWithData<Venue>(expectedVenue));

                // Act
                var result = await _venueService.GetVenueById(expectedVenue.Id);

                // Assert
                Assert.NotEmpty(result.Data.Menus);
            }
        }
    }
}
