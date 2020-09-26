using System.Runtime.CompilerServices;
using ImpeccableService.Backend.Core.Offering;
using ImpeccableService.Backend.Core.UserManagement;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ImpeccableService.Backend.Core.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ImpeccableService.Backend.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationService>();
            services.AddScoped<IAuthenticationService>(provider => provider.GetService<AuthenticationService>());
            services.AddScoped<IdentitySecurityFactory>();

            services.AddScoped<UserService>();
            services.AddScoped<IUserService>(provider => provider.GetService<UserService>());

            services.AddScoped<MenuService>();
            services.AddScoped<IMenuService>(provider => provider.GetService<MenuService>());

            services.AddScoped<VenueService>();
            services.AddScoped<IVenueService>(provider => provider.GetService<VenueService>());

            return services;
        }
    }
}
