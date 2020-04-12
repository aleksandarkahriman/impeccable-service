using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement.Dependency
{
    public interface IAuthenticationRemoteRepository
    {
        Task<Result> RegisterWithEmail(EmailCredentials emailCredentials);

        Task<ResultWithData<User>> LoginWithEmail(EmailCredentials emailCredentials);
    }
}
