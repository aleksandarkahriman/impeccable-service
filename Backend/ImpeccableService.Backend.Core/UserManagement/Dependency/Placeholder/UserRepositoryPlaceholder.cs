using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement.Dependency.Placeholder
{
    internal class UserRepositoryPlaceholder : IUserRepository
    {
        public Task<ResultWithData<bool>> UserWithEmailExists(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResultWithData<User>> Save(EmailRegistration emailRegistration)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResultWithData<User>> Read(string email, string passwordHash)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result> Save(Authentication authentication)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResultWithData<User>> ReadByRefreshToken(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}