using AutoMapper;
using ImpeccableService.Backend.Domain.Offering;

namespace ImpeccableService.Backend.Database.Offering.Model
{
    internal class OfferingAutoMapperProfile : Profile
    {
        public OfferingAutoMapperProfile()
        {
            CreateMap<MenuEntity, Menu>();
            CreateMap<MenuSectionEntity, MenuSection>();
            CreateMap<MenuItemEntity, MenuItem>();
        }
    }
}