using System;
using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Test.UserManagement.Stub
{
    public class CompanyRepositoryStub : ICompanyRepository
    {
        public Task<ResultWithData<Company>> Create(Company company, string ownerId) => 
            Task.FromResult(new ResultWithData<Company>(company));

        public Task<ResultWithData<Company>> ReadByOwner(string ownerId)
        {
            return Task.FromResult(new ResultWithData<Company>(new Company(Guid.NewGuid().ToString(), "McDonald's")));
        }
    }
}