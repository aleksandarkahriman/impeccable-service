using AutoMapper;
using ImpeccableService.Backend.Domain.Offering;

namespace ImpeccableService.Backend.API.Offering.Dto
{
    public class OfferingAutoMapperProfile : Profile
    {
        public OfferingAutoMapperProfile()
        {
            CreateMap<Menu, GetMenuDto>();
            CreateMap<MenuSection, GetMenuSectionDto>();
            CreateMap<MenuItem, GetMenuItemDto>();
        }
    }
}