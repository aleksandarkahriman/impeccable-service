using System.Threading.Tasks;

namespace ImpeccableService.Backend.Core.UserManagement.Dependency.Placeholder
{
    internal class SecurityEnvironmentVariablesPlaceholder : ISecurityEnvironmentVariables
    {
        public string PasswordHashSalt() => "lS6yIkXxqKHnkHdXZFwQBg==";

        public Task<string> SecurityCredentialsSecret() => Task.FromResult("SuperSecureWellGuardedSecret");

        public string DatabaseConnectionString() => "Server=localhost;Database=impeccable_service;User=root;Password=root;";
    }
}