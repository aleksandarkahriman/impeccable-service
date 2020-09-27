using System;
using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        
        public Task<ResultWithData<Company>> CreateCompany(RequestContextWithModel<CreateCompanyRequest> createCompanyRequest)
        {
            var company = new Company(Guid.NewGuid().ToString(), createCompanyRequest.Model.Name);
            return _companyRepository.Create(company, createCompanyRequest.Identity.Id);
        }
    }
}