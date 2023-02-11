using System;

namespace MyWebApplication.Models
{
    public class Client
    {
        public uint Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public ulong NumberPhone { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }

        public Tuple<uint, string, string, string, ulong, string, string> GetTurple()
        {
            return Tuple.Create(Id, Surname, Name, Patronymic, NumberPhone, Adress, Description);
        }
    }
}
