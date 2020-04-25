using System;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Database.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ImpeccableService.Backend.Database
{
    public static class DatabaseModule
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options
                .UseMySql("Server=localhost;Database=impeccable_service;User=root;Password=root;", mySqlOptions => mySqlOptions
                    .ServerVersion(new Version(8, 0, 19), ServerType.MySql)
                ));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
