using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Database.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Database.UserManagement
{
    internal class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<ResultWithData<Company>> Create(Company company, string ownerId)
        {
            var companyEntity = _mapper.Map<CompanyEntity>(company);
            companyEntity.OwnerId = ownerId;
            await _dbContext.Companies.AddAsync(companyEntity);
            await _dbContext.SaveChangesAsync();
            return new ResultWithData<Company>(company);
        }
    }
}