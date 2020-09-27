using System.Security.Claims;
using AutoMapper;
using ImpeccableService.Backend.Core.Context;
using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.API.UserManagement.Dto
{
    internal class UserManagementAutoMapperProfile : Profile
    {
        public UserManagementAutoMapperProfile()
        {
            CreateMap<EmailRegistrationDto, EmailRegistration>();
            CreateMap<EmailLoginDto, EmailLogin>();
            CreateMap<SecurityCredentials, AuthenticationCredentialsDto>();

            CreateMap<ClaimsPrincipal, Identity>()
                .ConstructUsing((principal, context) =>
                {
                    var id = principal.FindFirstValue(ClaimTypes.PrimarySid);
                    var email = principal.FindFirstValue(ClaimTypes.Email);

                    return new Identity(id, email);
                });

            CreateMap<User, GetUserProfileDto>();

            CreateMap<Company, GetCompanyDto>();
        }
    }
}
