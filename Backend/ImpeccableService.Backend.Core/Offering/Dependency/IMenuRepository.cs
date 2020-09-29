using System.Threading.Tasks;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering.Dependency
{
    public interface IMenuRepository
    {
        Task<ResultWithData<Menu>> ReadByVenueId(string venueId);
        
        Task<ResultWithData<Menu>> CreateForVenue(Menu menu, string venueId);
        
        Task<ResultWithData<Menu>> Read(string id);
        
        Task<Result> IsOwnedByCompany(string menuId, string companyId);
    }
}