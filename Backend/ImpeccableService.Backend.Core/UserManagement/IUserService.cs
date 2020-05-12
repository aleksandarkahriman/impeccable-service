using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    public interface IUserService
    {
        Task<ResultWithData<User>> GetProfile(RequestContext context);
    }
}