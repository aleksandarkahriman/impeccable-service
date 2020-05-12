using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<ResultWithData<User>> GetProfile(RequestContext context)
        {
            return _userRepository.Read(context.Identity.Id);
        }
    }
}
