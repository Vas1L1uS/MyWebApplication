using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccountLib.Data
{
    public static class Repository
    {
        private static readonly UserContext _db;

        static Repository()
        {
            _db = new UserContext();
        }

        public static async Task Add(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public static async Task DeleteClient(int id)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public static async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _db.Users.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public static async Task<User> GetUser(User user)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}