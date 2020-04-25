using AutoMapper;
using ImpeccableService.Backend.Database.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;

namespace ImpeccableService.Backend.Database
{
    public class DatabaseAutoMapperProfile : Profile
    {
        public DatabaseAutoMapperProfile()
        {
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();
        }
    }
}
