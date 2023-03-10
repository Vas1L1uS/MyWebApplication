using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApplication.Entities
{
    public class Client
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public ulong NumberPhone { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
    }
}