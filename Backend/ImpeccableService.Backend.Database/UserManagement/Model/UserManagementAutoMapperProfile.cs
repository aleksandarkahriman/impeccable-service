using AutoMapper;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.Database.UserManagement.Model
{
    internal class UserManagementAutoMapperProfile : Profile
    {
        public UserManagementAutoMapperProfile()
        {
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();
        }
    }
}
