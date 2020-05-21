using ImpeccableService.Backend.Core.UserManagement.Model;
using ImpeccableService.Backend.Domain.UserManagement;

namespace ImpeccableService.Backend.API.Test.Environment
{
    public static class TestUserRegistry
    {
        public static User ValidTestUser()
        {
            return new User(1, "frank@gmail.com", "MkXJOXJ0Wqit/kKiVHQNrwsFw8vOVVckX1npNw+2qIg=" /* 12345678 */, UserRole.Consumer, new DefaultUserProfileImage());
        }
    }
}
