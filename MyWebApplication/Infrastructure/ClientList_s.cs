using MyWebApplication.Exceptions;
using MyWebApplication.Entities;
using System;
using System.Collections.Generic;

namespace MyWebApplication.Infrastructure
{
    static public class ClientList_s
    {
        static public List<Client> clients;

        static public Client GetClientById(long id)
        {
            foreach (var item in clients)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            throw new NullRefExc($"Клиент с номером id {id} не найден!");
        }
    }
}