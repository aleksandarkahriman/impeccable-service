using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Domain.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.Offering
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class MenuSectionController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuSectionController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }
        
        [HttpPost("menu/{menuId}/section")]
        [Authorize(Roles = UserRole.ProviderAdmin)]
        public async Task<IActionResult> CreateSectionForMenu(string menuId, PostMenuSectionDto postMenuDto)
        {
            var createSectionForMenuRequest = new CreateSectionForMenuRequest(menuId, postMenuDto.Name);
            var sectionResult = await _menuService.CreateSectionForMenu(
                    new RequestContextWithModel<CreateSectionForMenuRequest>(createSectionForMenuRequest));

            if (sectionResult.Failure)
            {
                return NotFound();
            }
            
            return Created(string.Empty, null);
        }
    }
}