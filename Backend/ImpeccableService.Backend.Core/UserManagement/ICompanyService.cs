using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    public interface ICompanyService
    {
        Task<ResultWithData<Company>> CreateCompany(RequestContextWithModel<CreateCompanyRequest> createCompanyRequest);
    }
}