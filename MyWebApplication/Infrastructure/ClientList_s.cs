using MyWebApplication.Models;
using System.Collections.Generic;

namespace MyWebApplication.Infrastructure
{
    static public class ClientList_s
    {
        static public List<Client> clients;

        static public Client GetClientById(uint id)
        {
            foreach (var item in clients)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
