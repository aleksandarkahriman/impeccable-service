using System.Threading.Tasks;
using ImpeccableService.Client.Domain.Offering;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.Offering.Dependency
{
    public interface IMenuRemoteRepository
    {
        Task<ResultWithData<Menu>> GetMenuById(string id);
    }
}
