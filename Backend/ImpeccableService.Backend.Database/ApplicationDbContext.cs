using ImpeccableService.Backend.Database.UserManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace ImpeccableService.Backend.Database
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<SessionEntity> Sessions { get; set; }
    }
}
