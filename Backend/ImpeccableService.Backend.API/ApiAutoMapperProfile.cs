using AutoMapper;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.API
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<EmailRegistrationDto, EmailRegistration>();
        }
    }
}
