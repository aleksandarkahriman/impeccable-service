using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ImpeccableService.Backend.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    if (!hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        config.AddSecretsManager(configurator: options =>
                        {
                            options.SecretFilter = entry => entry.Name.StartsWith(config.Build().GetSection("ProjectId").Value);
                            options.KeyGenerator = (entry, key) => key.Split("---").Last();
                        });
                    }
                });
    }
}
