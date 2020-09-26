using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.API.Test.Environment.Data
{
    public static class TestUserRegistry
    {
        public static User ValidTestConsumerUser()
        {
            return new User(1, "frank@gmail.com", "MkXJOXJ0Wqit/kKiVHQNrwsFw8vOVVckX1npNw+2qIg=" /* 12345678 */, UserRole.Consumer, new DefaultUserProfileImage());
        }

        public static User ValidTestProviderAdminUser()
        {
            return new User(2, "charlie@gmail.com", "MkXJOXJ0Wqit/kKiVHQNrwsFw8vOVVckX1npNw+2qIg=" /* 12345678 */, UserRole.ProviderAdmin, new DefaultUserProfileImage());
        }
    }
}
