using System;
using System.Runtime.CompilerServices;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Database.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

[assembly: InternalsVisibleTo("ImpeccableService.Backend.API.Test")]
namespace ImpeccableService.Backend.Database
{
    public static class DatabaseModule
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>((provider, options) =>
            {
                var connectionString = services.BuildServiceProvider().GetRequiredService<ISecurityEnvironmentVariables>().DatabaseConnectionString();

                options
                    .UseMySql(connectionString, mySqlOptions =>
                        mySqlOptions
                            .ServerVersion(new Version(8, 0, 19), ServerType.MySql)
                    );
            });

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
