using Logger.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Utility.Test
{
    public static class UtilityTestModule
    {
        public static IServiceCollection AddTestLogger(this IServiceCollection services, ITestOutputHelper testOutputHelper)
        {
            services.AddSingleton(builder => testOutputHelper);
            services.AddSingleton(typeof(ILogger<>), typeof(TestLogger<>));
            return services;
        }
    }
}
