using Microsoft.EntityFrameworkCore;

namespace AccountLib
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              @"Server=(localdb)\MSSQLLocalDB;
                DataBase=Users;
                Trusted_Connection=True;"
            );
        }
    }
}