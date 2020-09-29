using AutoMapper;
using ImpeccableService.Backend.Database;
using ImpeccableService.Backend.Database.UserManagement.Model;

namespace ImpeccableService.Backend.API.Test.Environment.Data
{
    internal static class UserGenerator
    {
        internal static void AddTestUsers(this ApplicationDbContext context, IMapper mapper)
        {
            var validTestConsumerUser = TestUserRegistry.ValidTestConsumerUser();
            context.Users.Add(mapper.Map<UserEntity>(validTestConsumerUser));

            var validTestProviderAdminUser = TestUserRegistry.ValidTestProviderAdminUser();
            context.Users.Add(mapper.Map<UserEntity>(validTestProviderAdminUser));

            var validTestProviderAdminUserWithoutCompany = TestUserRegistry.ValidTestProviderAdminUserWithoutCompany();
            context.Users.Add(mapper.Map<UserEntity>(validTestProviderAdminUserWithoutCompany));
            
            var validTestProviderCompanyEntity = new CompanyEntity
            {
                Id = "39tt",
                Name = "McDonald's",
                OwnerId = validTestProviderAdminUser.Id
            };
            context.Companies.Add(validTestProviderCompanyEntity);
        }
    }
}