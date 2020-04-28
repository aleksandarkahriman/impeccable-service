using System;
using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Client.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement.Dependency.Placeholder
{
    internal class AuthenticationRemoteRepositoryPlaceholder : IAuthenticationRemoteRepository
    {
        public Task<Result> RegisterWithEmail(EmailRegistration emailRegistration)
        {
            throw new NotImplementedException();
        }

        public Task<ResultWithData<AuthenticatedUser>> LoginWithEmail(EmailLogin emailLogin)
        {
            throw new NotImplementedException();
        }

        public Task<ResultWithData<AuthenticatedUser>> RefreshToken(AuthenticatedUser user)
        {
            throw new NotImplementedException();
        }
    }
}
