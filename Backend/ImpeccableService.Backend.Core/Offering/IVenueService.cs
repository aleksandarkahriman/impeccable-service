using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering
{
    public interface IVenueService
    {
        Task<ResultWithData<Venue>> CreateVenue(RequestContextWithModel<CreateVenueRequest> createVenueRequest);
    }
}