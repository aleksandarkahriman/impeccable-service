using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.Core.Test.UserManagement.Provider
{
    public class UserModelProvider
    {
        public static User ConstructTestUser() => new User("2e56", "user@domain.com", "passwordHash", UserRole.Consumer, new DefaultUserProfileImage());
    }
}
