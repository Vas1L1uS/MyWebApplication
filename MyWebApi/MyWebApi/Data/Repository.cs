using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;
using MyWebApi.Models.AuthClientApp;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using System.Xml.Linq;

namespace MyWebApi.Data
{
    public class Repository
    {
        public async Task AddClient(Client client, ClientContext db)
        {
            client.Id = default;
            await db.Clients.AddAsync(client);
            await db.SaveChangesAsync();
        }

        public async Task DeleteClient(int id, ClientContext db)
        {
            Client client = await db.Clients.FirstOrDefaultAsync(c => c.Id == id);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
        }

        public async Task EditClient(Client client, ClientContext db)
        {
            var clients = db.Clients.Where(c => c.Id == client.Id);
            Client dbClient = clients.First();
            dbClient.Surname = client.Surname;
            dbClient.Name = client.Name;
            dbClient.Patronymic = client.Patronymic;
            dbClient.NumberPhone = client.NumberPhone;
            dbClient.Adress = client.Adress;
            dbClient.Description = client.Description;
            db.Clients.Update(client);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAll(ClientContext db)
        {
            var a = db.Clients.ToListAsync();
            return await db.Clients.ToListAsync();
        }


        public async Task<Client> GetById(int id, ClientContext db)
        {
            return await db.Clients.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllUsers(ClientContext db)
        {
            var a = db.Users.ToListAsync();
            return await db.Users.ToListAsync();
        }

        public async Task AddUser(User user, ClientContext db)
        {
            user.Id = default;
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<User> GetUserById(string id, ClientContext db)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUserName(string userName, ClientContext db)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<string> GetUserRoleById(string id, ClientContext db)
        {
            var userRole = await db.UserRoles.FirstOrDefaultAsync(x => x.UserId == id);
            return userRole?.RoleId;
        }

        public async Task<string> GetRoleNameByRoleId(string id, ClientContext db)
        {
            var role = await db.Roles.FirstOrDefaultAsync(x => x.Name == id);
            return role.Name;
        }

        public async Task DeleteUser(string id, ClientContext db)
        {
            User user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }
    }
}