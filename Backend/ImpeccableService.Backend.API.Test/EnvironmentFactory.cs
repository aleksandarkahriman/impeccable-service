using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

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
            });
        }
    }
}
