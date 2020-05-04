using System;
using ImpeccableService.Backend.API.Test.Environment;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
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

        private Type _testDatabaseSetupType = typeof(DefaultTestDatabaseSetup);

        public void ConfigureServices(Action<IServiceCollection> configureServicesAction) => _configureServicesAction = configureServicesAction;

        public void UseTestDatabase<T>() where T : ITestDatabaseSetup => _testDatabaseSetupType = typeof(T);

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                _configureServicesAction(services);

                services.AddScoped<ISecurityEnvironmentVariables, TestSecureEnvironmentVariables>();
                services.AddScoped(typeof(ITestDatabaseSetup), _testDatabaseSetupType);

                var databaseSetup = services.BuildServiceProvider().GetRequiredService<ITestDatabaseSetup>();
                databaseSetup.Initialize();
            });
        }
    }
}
