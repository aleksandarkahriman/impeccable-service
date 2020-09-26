using System;
using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering.Dependency;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public Task<ResultWithData<Venue>> CreateVenue(RequestContextWithModel<CreateVenueRequest> createVenueRequest)
        {
            var venue = new Venue(Guid.NewGuid().ToString(), createVenueRequest.Model.Name);
            return _venueRepository.Create(venue);
        }
    }
}