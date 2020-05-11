using ImpeccableService.Backend.API.Configuration;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using Logger.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace ImpeccableService.Backend.API
{
    public static class ApiModule
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddScoped(typeof(ILogger<>), typeof(Logger<>));
            services.AddScoped<ISecurityEnvironmentVariables, SecurityEnvironmentVariables>();

            return services;
        }
    }
}
