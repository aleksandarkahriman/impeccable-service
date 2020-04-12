using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Logger.Abstraction;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement
{
    internal class UserService : IUserService
    {
        private readonly ILogger _logger;

        public UserService(ILogger logger)
        {
            _logger = logger;
        }

        public ResultWithData<User> RegisterWithEmail(EmailRegistration emailRegistration)
        {
            throw new System.NotImplementedException();
        }
    }
}
