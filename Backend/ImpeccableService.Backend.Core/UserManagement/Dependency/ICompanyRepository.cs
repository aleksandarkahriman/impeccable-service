using System.Threading.Tasks;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement.Dependency
{
    public interface ICompanyRepository
    {
        Task<ResultWithData<Company>> Create(Company company);
    }
}