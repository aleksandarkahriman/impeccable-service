using System.Threading.Tasks;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.Offering.Dependency;
using ImpeccableService.Backend.Domain.Offering;
using Logger.Abstraction;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.Offering
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger<MenuService> _logger;

        public MenuService(IMenuRepository menuRepository, ILogger<MenuService> logger)
        {
            _menuRepository = menuRepository;
            _logger = logger;
        }
        
        public Task<ResultWithData<Menu>> GetMenuForVenue(RequestContextWithModel<MenuForVenueRequest> menuForVenueRequest)
        {
            return _menuRepository.ReadByVenueId(menuForVenueRequest.Model.VenueId);
        }
    }
}