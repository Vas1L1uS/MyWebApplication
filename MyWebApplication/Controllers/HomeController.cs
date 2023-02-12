using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Exceptions;
using MyWebApplication.Infrastructure;
using MyWebApplication.Entities;
using System;
using System.Collections.Generic;
using MyWebApplication.Contexts;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace MyWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Clients = new ClientContext().Clients;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var clients = new ClientContext().Clients.Where(c => c.Id == id);
            return View(clients.First());
        }

        public IActionResult Delete(long id)
        {
            using (var db = new ClientContext())
            {
                var clients = db.Clients.Where(c => c.Id == id);
                db.Clients.Remove(clients.First());
                db.SaveChanges();
            }
            return Redirect("~/");
        }

        public IActionResult ClientData(long id)
        {
            try
            {
                var clients = new ClientContext().Clients.Where(c => c.Id == id);
                return View(clients.First());
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult EditClientDB(long id, string surname, string name, string patronymic, ulong numberPhone, string adress, string description)
        {
            using (var db = new ClientContext())
            {
                var clients = db.Clients.Where(c => c.Id == id);
                Client client = clients.First();
                client.Surname = surname;
                client.Name = name;
                client.Patronymic = patronymic;
                client.NumberPhone = numberPhone;
                client.Adress = adress;
                client.Description = description;

                db.Clients.Update(client);

                db.SaveChanges();
            }
            return Redirect("~/");
        }

        [HttpPost]
        public IActionResult GetDataFromViewField(string surname, string name, string patronymic, ulong numberPhone, string adress, string description)
        {
            using (var db = new ClientContext())
            {
                db.Clients.Add(
                    new Client()
                    {
                        Surname = surname,
                        Name = name,
                        Patronymic = patronymic,
                        NumberPhone = numberPhone,
                        Adress = adress,
                        Description = description
                    });

                db.SaveChanges();
            }
            return Redirect("~/");
        }
    }
}
