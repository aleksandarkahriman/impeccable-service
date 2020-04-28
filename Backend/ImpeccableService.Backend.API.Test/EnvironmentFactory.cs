using System;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace ImpeccableService.Backend.API.Test
{
    public class EnvironmentFactory : WebApplicationFactory<Startup>
    {
        private Action<IServiceCollection> _configureServicesAction;

        public void ConfigureServices(Action<IServiceCollection> configureServicesAction)
        {
            _configureServicesAction = configureServicesAction;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                _configureServicesAction(services);

                services.AddScoped<ISecurityEnvironmentVariables, TestSecureEnvironmentVariables>();

                var provider = services.BuildServiceProvider();
                using var context = provider.GetRequiredService<ApplicationDbContext>();
                
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            });

        }
    }
}
