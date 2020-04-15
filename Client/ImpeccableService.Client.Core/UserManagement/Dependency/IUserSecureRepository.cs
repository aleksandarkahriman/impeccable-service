using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Model;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement.Dependency
{
    public interface IUserSecureRepository
    {
        Task<Result> Write(AuthenticatedUser user);

        Task<Result> Delete(AuthenticatedUser user);
    }
}
