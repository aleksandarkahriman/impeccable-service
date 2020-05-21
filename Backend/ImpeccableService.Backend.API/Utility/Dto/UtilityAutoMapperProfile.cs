using AutoMapper;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Domain.Utility;

namespace ImpeccableService.Backend.API.Utility.Dto
{
    public class UtilityAutoMapperProfile : Profile
    {
        public UtilityAutoMapperProfile()
        {
        }

        public UtilityAutoMapperProfile(IFileStorage fileStorage)
        {
            CreateMap<Image, ImageDto>()
                .ForMember(destination => destination.Url,
                    options => 
                        options.MapFrom((source, destination) => fileStorage.Sign(source)));
        }
    }
}
