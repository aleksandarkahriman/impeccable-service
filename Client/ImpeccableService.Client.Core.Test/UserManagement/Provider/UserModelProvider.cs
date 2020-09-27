using ImpeccableService.Client.Core.UserManagement.Model;
using ImpeccableService.Client.Domain.UserManagement;

namespace ImpeccableService.Client.Core.Test.UserManagement.Provider
{
    public class UserModelProvider
    {
        public static AuthenticatedUser ConstructValidUser() => new AuthenticatedUser("33er", "user@domain.com", 
            new SecurityCredentials("accessToken", "refreshToken", "logoutToken"));

        public static AuthenticatedUser ConstructRefreshedValidUser() => new AuthenticatedUser("2t67", "user@domain.com",
            new SecurityCredentials("refreshedAccessToken", "refreshToken", "logoutToken"));
    }
}
