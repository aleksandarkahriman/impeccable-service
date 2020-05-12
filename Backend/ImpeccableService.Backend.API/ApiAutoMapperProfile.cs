using System.Security.Claims;
using AutoMapper;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.API
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<EmailRegistrationDto, EmailRegistration>();
            CreateMap<EmailLoginDto, EmailLogin>();
            CreateMap<SecurityCredentials, AuthenticationCredentialsDto>();

            CreateMap<ClaimsPrincipal, Identity>()
                .ConstructUsing((principal, context) =>
                {
                    var id = int.Parse(principal.FindFirstValue(ClaimTypes.PrimarySid));
                    var email = principal.FindFirstValue(ClaimTypes.Email);

                    return new Identity(id, email);
                });

            CreateMap<User, GetUserProfileDto>();
        }
    }
}
