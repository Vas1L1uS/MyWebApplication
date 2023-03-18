using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApplication.DataContext;
using MyWebApplication.Entities;
using System.Runtime.ConstrainedExecution;

namespace MyWebApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();
            if (context.Clients.Any()) return;

            var sections = new List<Client>()
            {
                new Client(){Surname="Лебедев", Name="Иннокентий", Patronymic="Владимирович", NumberPhone=79632524312, Adress="Москва", Description="Программист C#"},
                new Client(){Surname="Кержакова", Name="Светлана", Patronymic="Николевна", NumberPhone=79172326688, Adress="Калининград", Description="Фротендер"},
                new Client(){Surname="Паровозов", Name="Аркадий", Patronymic="Степанович", NumberPhone=79035013674, Adress="Минск", Description="Дизайнер"},
            };
            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var section in sections)
                {
                    context.Clients.Add(section);
                }

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Clients] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Clients] OFF");
                trans.Commit();
            }


        }
    }
}