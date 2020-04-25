using ImpeccableService.Backend.Database.UserManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace ImpeccableService.Backend.Database
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
