using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.AuthClientApp;
using MyWebApplication.Entities;

namespace MyWebApplication.DataContext
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
    }
}