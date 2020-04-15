﻿using System.Runtime.CompilerServices;
using ImpeccableService.Backend.Core.UserManagement;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Core.UserManagement.Dependency.Placeholder;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ImpeccableService.Backend.Core.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ImpeccableService.Backend.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<AuthenticationService>();
            services.AddSingleton<IdentitySecurityFactory>();

            services.AddPlaceholders();

            return services;
        }

        private static void AddPlaceholders(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepositoryPlaceholder>();
            services.AddSingleton<ISecurityEnvironmentVariables, SecurityEnvironmentVariablesPlaceholder>();
        }
    }
}
