using System.Threading.Tasks;
using ImpeccableService.Client.Core.Offering.Dependency;
using ImpeccableService.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.Offering
{
    public class VenueService
    {
        private readonly IVenueRemoteRepository _venueRemoteRepository;

        public VenueService(IVenueRemoteRepository venueRemoteRepository)
        {
            _venueRemoteRepository = venueRemoteRepository;
        }

        public Task<ResultWithData<Venue>> GetVenueById(string id) => _venueRemoteRepository.GetVenueById(id);
    }
}
