using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;
using MyWebApi.Models.AuthClientApp;

public class ClientContext : IdentityDbContext<User>
{
    public ClientContext(DbContextOptions options) : base(options) { }
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