using System;
using System.Threading.Tasks;
using AutoMapper;
using ImpeccableService.Backend.API.Offering.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Domain.Offering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImpeccableService.Backend.API.Offering
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [HttpGet("venue/{venueId}/menu")]
        public async Task<IActionResult> MenuForVenue(string venueId)
        {
            var menuResult = await _menuService.GetMenuForVenue(new RequestContextWithModel<MenuForVenueRequest>(new MenuForVenueRequest(venueId), 
                    _mapper.Map<Identity>(User)));
            return menuResult.Success
                ? Ok(_mapper.Map<GetMenuDto>(menuResult.Data))
                : (IActionResult)NotFound();
        }
    }
}