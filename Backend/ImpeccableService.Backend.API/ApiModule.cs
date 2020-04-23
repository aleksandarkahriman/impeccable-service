using Logger.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace ImpeccableService.Backend.API
{
    public static class ApiModule
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();

            return services;
        }
    }
}
