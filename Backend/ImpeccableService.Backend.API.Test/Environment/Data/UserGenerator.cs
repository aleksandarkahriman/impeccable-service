using AutoMapper;
using ImpeccableService.Backend.Database;
using ImpeccableService.Backend.Database.UserManagement.Model;

namespace ImpeccableService.Backend.API.Test.Environment.Data
{
    internal static class UserGenerator
    {
        internal static void AddTestUsers(this ApplicationDbContext context, IMapper mapper)
        {
            var validTestUser = TestUserRegistry.ValidTestUser();
            context.Users.Add(mapper.Map<UserEntity>(validTestUser));
        }
    }
}