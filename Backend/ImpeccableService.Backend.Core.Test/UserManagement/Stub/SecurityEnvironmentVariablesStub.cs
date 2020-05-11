using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Dependency;

namespace ImpeccableService.Backend.Core.Test.UserManagement.Stub
{
    internal class SecurityEnvironmentVariablesStub : ISecurityEnvironmentVariables
    {
        public string PasswordHashSalt() => "lS6yIkXxqKHnkHdXZFwQBg==";

        public Task<string> SecurityCredentialsSecret() => Task.FromResult("SuperSecureWellGuardedSecret");

        public string SecurityCredentialsIssuer() => "ImpeccableService";

        public string DatabaseConnectionString() => "Server=localhost;Database=impeccable_service;User=root;Password=root;";
    }
}
