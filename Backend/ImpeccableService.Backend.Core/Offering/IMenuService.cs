using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering
{
    public interface IMenuService
    {
        Task<ResultWithData<Menu>> GetMenuForVenue(RequestContextWithModel<GetMenuForVenueRequest> createMenuForVenueRequest);

        Task<ResultWithData<Menu>> CreateMenuForVenue(RequestContextWithModel<CreateMenuForVenueRequest> createMenuForVenueRequest);
    }
}