using MyWpfApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWpfApp.AuthClientApp
{
    public class User : ICloneable
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public object Clone()
        {
            var newClient = new User();
            newClient.Id = this.Id;
            newClient.UserName = this.UserName;
            newClient.Role = this.Role;
            return newClient;
        }
    }
}