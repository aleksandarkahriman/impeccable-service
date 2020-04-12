using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement
{
    public interface IUserService
    {
        ResultWithData<User> RegisterWithEmail(EmailRegistration emailRegistration);
    }
}
