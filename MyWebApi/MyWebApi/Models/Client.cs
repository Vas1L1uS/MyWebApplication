namespace MyWebApi.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public ulong NumberPhone { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
    }
}
