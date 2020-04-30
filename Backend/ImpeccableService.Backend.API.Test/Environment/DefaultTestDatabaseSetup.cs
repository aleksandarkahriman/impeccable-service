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

            const string passwordHash = "MkXJOXJ0Wqit/kKiVHQNrwsFw8vOVVckX1npNw+2qIg="; // 12345678 

            _context.Users.Add(new UserEntity
            {
                Id = 1,
                Email = "frank@gmail.com",
                PasswordHash = passwordHash
            });

            _context.SaveChanges();
        }
    }
}
