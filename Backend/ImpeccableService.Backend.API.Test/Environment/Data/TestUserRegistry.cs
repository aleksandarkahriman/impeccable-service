using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.API.Test.Environment.Data
{
    public static class TestUserRegistry
    {
        public static User ValidTestConsumerUser()
        {
            return new User("1q9j", "frank@gmail.com", "MkXJOXJ0Wqit/kKiVHQNrwsFw8vOVVckX1npNw+2qIg=" /* 12345678 */, UserRole.Consumer, new DefaultUserProfileImage());
        }

        public static User ValidTestProviderAdminUser()
        {
            return new User("7rti", "charlie@gmail.com", "MkXJOXJ0Wqit/kKiVHQNrwsFw8vOVVckX1npNw+2qIg=" /* 12345678 */, UserRole.ProviderAdmin, new DefaultUserProfileImage());
        }
        
        public static User ValidTestProviderAdminUserWithoutCompany()
        {
            return new User("8r5q", "ana@gmail.com", "MkXJOXJ0Wqit/kKiVHQNrwsFw8vOVVckX1npNw+2qIg=" /* 12345678 */, UserRole.ProviderAdmin, new DefaultUserProfileImage());
        }
    }
}
