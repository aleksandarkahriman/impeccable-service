using ImpeccableService.Backend.Database;
using ImpeccableService.Backend.Database.UserManagement.Model;

namespace ImpeccableService.Backend.API.Test.Environment
{
    internal class DefaultTestDatabaseSetup : ITestDatabaseSetup
    {
        private readonly ApplicationDbContext _context;

        public DefaultTestDatabaseSetup(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var validTestUser = TestUserRegistry.ValidTestUser();
            _context.Users.Add(new UserEntity
            {
                Id = validTestUser.Id,
                Email = validTestUser.Email,
                PasswordHash = validTestUser.PasswordHash
            });

            _context.SaveChanges();
        }
    }
}
