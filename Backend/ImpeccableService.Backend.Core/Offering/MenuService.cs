using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering.Dependency;
using ImpeccableService.Backend.Core.Offering.Model;
using ImpeccableService.Backend.Domain.Offering;
using Logger.Abstraction;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly ILogger<MenuService> _logger;

        public MenuService(IMenuRepository menuRepository, IVenueRepository venueRepository, ILogger<MenuService> logger)
        {
            _menuRepository = menuRepository;
            _venueRepository = venueRepository;
            _logger = logger;
        }
        
        public Task<ResultWithData<Menu>> GetMenuForVenue(RequestContextWithModel<GetMenuForVenueRequest> createMenuForVenueRequest)
        {
            return _menuRepository.ReadByVenueId(createMenuForVenueRequest.Model.VenueId);
        }

        public async Task<ResultWithData<Menu>> CreateMenuForVenue(RequestContextWithModel<CreateMenuForVenueRequest> createMenuForVenueRequest)
        {
            var venueResult = await _venueRepository.Read(createMenuForVenueRequest.Model.VenueId);
            if (venueResult.Failure)
            {
                return new ResultWithData<Menu>(venueResult.ErrorReason);
            }
            
            var menu = new Menu(Guid.NewGuid().ToString(), new List<MenuSection>());
            return await _menuRepository.CreateForVenue(menu, venueResult.Data.Id);
        }
    }
}