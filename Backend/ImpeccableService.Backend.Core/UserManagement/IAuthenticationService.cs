using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    public interface IAuthenticationService
    {
        Task<Result> RegisterWithEmail(RequestContext<EmailRegistration> emailRegistrationRequest);

        Task<ResultWithData<SecurityCredentials>> LoginWithEmail(RequestContext<EmailLogin> emailLoginRequest);

        Task<ResultWithData<SecurityCredentials>> RefreshToken(RequestContext<RefreshToken> refreshTokenRequest);
    }
}