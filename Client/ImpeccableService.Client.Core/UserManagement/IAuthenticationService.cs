using System.Threading.Tasks;
using ImpeccableService.Client.Core.UserManagement.Model;
using Utility.Application.ResultContract;

namespace ImpeccableService.Client.Core.UserManagement
{
    public interface IAuthenticationService
    {
        Task<Result> RegisterWithEmail(EmailCredentials emailCredentials);
    }
}
