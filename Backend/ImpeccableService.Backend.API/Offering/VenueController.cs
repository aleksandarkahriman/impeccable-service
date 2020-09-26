using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.Offering
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;
        private readonly IMapper _mapper;

        public VenueController(IVenueService venueService, IMapper mapper)
        {
            _venueService = venueService;
            _mapper = mapper;
        }
        
        [HttpPost("venue")]
        [Authorize(Roles = UserRole.ProviderAdmin)]
        public async Task<IActionResult> CreateVenue(PostVenueDto postVenueDto)
        {
            var createVenueRequest = new RequestContextWithModel<CreateVenueRequest>(new CreateVenueRequest(postVenueDto.Name));
            var venueResult = await _venueService.CreateVenue(createVenueRequest);
            return Created(string.Empty, _mapper.Map<GetVenueDto>(venueResult.Data));
        }
    }
}