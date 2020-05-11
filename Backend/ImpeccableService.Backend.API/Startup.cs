using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using ImpeccableService.Backend.Core;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ImpeccableService.Backend.API
{
    public class Startup
    {
        private ISecurityEnvironmentVariables _securityEnvironmentVariables;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddCore();
            services.AddDatabase();
            services.AddApi();
            services.AddControllers(options => options.Filters.Add(new UnhandledExceptionFilter()));
            services.AddAutoMapper(new List<Assembly>
            {
                typeof(ApiModule).Assembly,
                typeof(DatabaseModule).Assembly
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_securityEnvironmentVariables.SecurityCredentialsSecret().Result)),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = _securityEnvironmentVariables.SecurityCredentialsIssuer(),
                    ValidateAudience = false
                };
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISecurityEnvironmentVariables securityEnvironmentVariables)
        {
            _securityEnvironmentVariables = securityEnvironmentVariables;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
