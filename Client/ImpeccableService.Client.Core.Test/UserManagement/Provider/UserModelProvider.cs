using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Domain.UserManagement;

namespace ImpeccableService.Client.Core.Test.UserManagement.Provider
{
    public class UserModelProvider
    {
        public static AuthenticatedUser ConstructValidUser() => new AuthenticatedUser(1, "user@domain.com", 
            new SecurityCredentials("accessToken", "refreshToken", "logoutToken"));

        public static AuthenticatedUser ConstructRefreshedValidUser() => new AuthenticatedUser(1, "user@domain.com",
            new SecurityCredentials("refreshedAccessToken", "refreshToken", "logoutToken"));
    }
}
