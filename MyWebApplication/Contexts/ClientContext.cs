using Microsoft.EntityFrameworkCore;
using MyWebApplication.Entities;

namespace MyWebApplication.Contexts
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              @"Server=(localdb)\MSSQLLocalDB;
                DataBase=_Clients;
                Trusted_Connection=True;"
            );
        }
    }
}
