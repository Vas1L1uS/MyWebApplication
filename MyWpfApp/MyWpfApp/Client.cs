using System;

namespace MyWpfApp.Entities
{
    public class Client : ICloneable
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public ulong NumberPhone { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            var newClient = new Client();
            newClient.Id = Id;
            newClient.Surname = this.Surname;
            newClient.Name = this.Name;
            newClient.Patronymic = this.Patronymic;
            newClient.NumberPhone = this.NumberPhone;
            newClient.Adress = this.Adress;
            newClient.Description = this.Description;
            return newClient;
        }
    }
}