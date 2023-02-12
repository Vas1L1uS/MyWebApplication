using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using MyWebApplication.Entities;
using System.IO;
using System.Linq;

namespace MyWebApplication.Infrastructure
{
    public class LoadJson
    {
        public List<Client> Load()
        {
            string json;
            var clients = new List<Client>();

            try
            {
                json = File.ReadAllText("clients.json");

                JToken[] jArrayClients = JObject.Parse(json)["Clients"].ToArray();

                foreach (var item in jArrayClients)
                {
                    long id = item["id"].ToObject<long>();
                    string surname = item["surname"].ToObject<string>();
                    string name = item["name"].ToObject<string>();
                    string patronymic = item["patronymic"].ToObject<string>();
                    ulong numberPhone = item["numberPhone"].ToObject<ulong>();
                    string adress = item["adress"].ToObject<string>();
                    string description = item["description"].ToObject<string>();


                    Client client = new Client()
                    {
                        Id = id,
                        Surname = surname,
                        Name = name,
                        Patronymic = patronymic,
                        NumberPhone = numberPhone,
                        Adress = adress,
                        Description = description
                    };

                    clients.Add(client);
                }
            }
            catch
            {
                Console.WriteLine("Файл не существует.");
            }
            return clients;
        }
    }
}
