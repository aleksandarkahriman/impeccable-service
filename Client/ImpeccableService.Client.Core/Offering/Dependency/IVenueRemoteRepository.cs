using System.Threading.Tasks;
using ImpeccableService.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.Offering.Dependency
{
    public interface IVenueRemoteRepository
    {
        Task<ResultWithData<Venue>> GetVenueById(string id);
    }
}
