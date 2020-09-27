using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.UserManagement
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        
        [HttpPost("company")]
        [Authorize(Roles = UserRole.ProviderAdmin)]
        public async Task<IActionResult> CreateCompany(PostCompanyDto postCompanyDto)
        {
            var createCompanyRequest = new RequestContextWithModel<CreateCompanyRequest>(
                new CreateCompanyRequest(postCompanyDto.Name), _mapper.Map<Identity>(User));
            var companyResult = await _companyService.CreateCompany(createCompanyRequest);
            return Created(string.Empty, _mapper.Map<GetCompanyDto>(companyResult.Data));
        }
    }
}