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

            var validTestProviderAdminUserOne = TestUserRegistry.ValidTestProviderAdminUserOne();
            context.Users.Add(mapper.Map<UserEntity>(validTestProviderAdminUserOne));
            
            var validTestProviderAdminUserTwo = TestUserRegistry.ValidTestProviderAdminUserTwo();
            context.Users.Add(mapper.Map<UserEntity>(validTestProviderAdminUserTwo));

            var validTestProviderAdminUserWithoutCompany = TestUserRegistry.ValidTestProviderAdminUserWithoutCompany();
            context.Users.Add(mapper.Map<UserEntity>(validTestProviderAdminUserWithoutCompany));
            
            var validTestProviderCompanyEntityOne = new CompanyEntity
            {
                Id = "39tt",
                Name = "McDonald's",
                OwnerId = validTestProviderAdminUserOne.Id
            };
            context.Companies.Add(validTestProviderCompanyEntityOne);
            
            var validTestProviderCompanyEntityTwo = new CompanyEntity
            {
                Id = "2j6f",
                Name = "AK",
                OwnerId = validTestProviderAdminUserTwo.Id
            };
            context.Companies.Add(validTestProviderCompanyEntityTwo);
        }
    }
}