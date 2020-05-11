using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using Microsoft.Extensions.Configuration;

namespace ImpeccableService.Backend.API.Configuration
{
    public class SecurityEnvironmentVariables : ISecurityEnvironmentVariables
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        private readonly DataAccessConfiguration _dataAccessConfiguration;

        public SecurityEnvironmentVariables(IConfiguration configuration)
        {
            _authenticationConfiguration =
                configuration.GetSection("Authentication").Get<AuthenticationConfiguration>();
            _dataAccessConfiguration = configuration.GetSection("DataAccess").Get<DataAccessConfiguration>();
        }

        public string PasswordHashSalt() => _authenticationConfiguration.PasswordHashSalt;

        public Task<string> SecurityCredentialsSecret() => Task.FromResult(_authenticationConfiguration.Secret);

        public string SecurityCredentialsIssuer() => _authenticationConfiguration.Issuer;

        public string DatabaseConnectionString() => _dataAccessConfiguration.DatabaseConnectionString;
    }
}
