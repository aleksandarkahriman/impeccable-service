using Amazon.S3;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImpeccableService.Backend.FileStorage
{
    public static class FileStorageModule
    {
        public static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddSingleton<IFileStorage, FileStorage>();

            return services;
        }
    }
}
