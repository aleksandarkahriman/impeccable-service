using System.Runtime.CompilerServices;
using ImpeccableService.Client.Core.UserManagement;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ImpeccableService.Client.Core.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ImpeccableService.Client.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<UserService>();
            services.AddSingleton<IUserService, UserService>(builder =>
                builder.GetRequiredService<UserService>());

            return services;
        }
    }
}
