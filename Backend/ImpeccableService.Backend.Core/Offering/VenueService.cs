using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering.Dependency;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        private readonly ICompanyRepository _companyRepository;

        public VenueService(IVenueRepository venueRepository, ICompanyRepository companyRepository)
        {
            _venueRepository = venueRepository;
            _companyRepository = companyRepository;
        }

        public Task<ResultWithData<List<Venue>>> GetVenues()
        {
            return _venueRepository.Read();
        }

        public async Task<ResultWithData<Venue>> CreateVenue(RequestContextWithModel<CreateVenueRequest> createVenueRequest)
        {
            var companyResult = await _companyRepository.ReadByOwner(createVenueRequest.Identity.Id);
            var venue = new Venue(Guid.NewGuid().ToString(), createVenueRequest.Model.Name, companyResult.Data.Id);
            return await _venueRepository.Create(venue);
        }
    }
}