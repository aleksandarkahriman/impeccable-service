using System;
using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Model;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement.Dependency.Placeholder
{
    internal class UserSecureRepositoryPlaceholder : IUserSecureRepository
    {
        public Task<Result> Write(AuthenticatedUser user)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(AuthenticatedUser user)
        {
            throw new NotImplementedException();
        }
    }
}
