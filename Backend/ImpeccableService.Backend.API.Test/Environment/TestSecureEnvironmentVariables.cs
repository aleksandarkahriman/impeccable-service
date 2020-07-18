﻿using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Dependency;

namespace ImpeccableService.Backend.API.Test.Environment
{
    public class TestSecureEnvironmentVariables : ISecurityEnvironmentVariables
    {
        public string PasswordHashSalt() => "lS6yIkXxqKHnkHdXZFwQBg==";

        public Task<string> SecurityCredentialsSecret() => Task.FromResult("SuperSecureWellGuardedSecret");

        public string SecurityCredentialsIssuer() => "ImpeccableService";

        public string DatabaseConnectionString() => "Server=localhost;Database=impeccable_service_test;User=root;Password=rootroot;";
    }
}
