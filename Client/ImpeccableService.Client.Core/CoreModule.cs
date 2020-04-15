using System.Runtime.CompilerServices;
using ImpeccableService.Client.Core.UserManagement;
using ImpeccableService.Client.Core.UserManagement.Dependency;
using ImpeccableService.Client.Core.UserManagement.Dependency.Placeholder;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ImpeccableService.Client.Core.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ImpeccableService.Client.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<AuthenticationService>();

            services.AddPlaceholders();

            return services;
        }

        private static void AddPlaceholders(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationRemoteRepository, AuthenticationRemoteRepositoryPlaceholder>();
            services.AddSingleton<IUserSecureRepository, UserSecureRepositoryPlaceholder>();
        }
    }
}
