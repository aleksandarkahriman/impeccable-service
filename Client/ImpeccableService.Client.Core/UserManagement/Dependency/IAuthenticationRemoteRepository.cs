using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement.Dependency
{
    public interface IAuthenticationRemoteRepository
    {
        Task<Result> RegisterWithEmail(EmailRegistration emailRegistration);

        Task<ResultWithData<AuthenticatedUser>> LoginWithEmail(EmailLogin emailLogin);

        Task<ResultWithData<AuthenticatedUser>> RefreshToken(AuthenticatedUser user);
    }
}
