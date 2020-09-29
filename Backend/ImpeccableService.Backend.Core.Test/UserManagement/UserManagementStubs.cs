using ImpeccableService.Backend.Core.Test.UserManagement.Stub;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace ImpeccableService.Backend.Core.Test.UserManagement
{
    public static class UserManagementStubs
    {
        public static IServiceCollection AddUserManagementStubs(this IServiceCollection services)
        {
            services.AddScoped<ISecurityEnvironmentVariables, SecurityEnvironmentVariablesStub>();
            services.AddScoped<ICompanyRepository, CompanyRepositoryStub>();

            return services;
        }
    }
}
