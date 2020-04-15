using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement.Dependency
{
    public interface IUserRepository
    {
        Task<ResultWithData<bool>> UserWithEmailExists(string email);

        Task<ResultWithData<User>> Save(EmailRegistration emailRegistration);

        Task<ResultWithData<User>> Read(string email, string passwordHash);

        Task<Result> Save(Authentication authentication);
        Task<ResultWithData<User>> ReadByRefreshToken(string refreshToken);
    }
}
