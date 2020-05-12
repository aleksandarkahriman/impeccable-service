using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    public interface IAuthenticationService
    {
        Task<Result> RegisterWithEmail(RequestContextWithModel<EmailRegistration> emailRegistrationRequest);

        Task<ResultWithData<SecurityCredentials>> LoginWithEmail(RequestContextWithModel<EmailLogin> emailLoginRequest);

        Task<ResultWithData<SecurityCredentials>> RefreshToken(RequestContextWithModel<RefreshToken> refreshTokenRequest);
    }
}