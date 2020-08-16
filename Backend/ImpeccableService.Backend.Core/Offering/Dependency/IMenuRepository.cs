using System.Threading.Tasks;
using ImpeccableService.Backend.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering.Dependency
{
    public interface IMenuRepository
    {
        Task<ResultWithData<Menu>> ReadByVenueId(string venueId);
    }
}