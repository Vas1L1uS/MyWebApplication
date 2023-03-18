using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Exceptions;
using MyWebApplication.Infrastructure;
using MyWebApplication.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;
using MyWebApplication.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace MyWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientDataApi _clientDataApi;

        public HomeController()
        {
            _clientDataApi = new ClientDataApi();
        }

        public IActionResult Index()
        {
            ViewBag.Clients = _clientDataApi.GetAll();
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_clientDataApi.Get(id));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _clientDataApi.Delete(id);
            return Redirect("~/");
        }

        [Authorize]
        public IActionResult ClientData(int id)
        {
            try
            {
                return View(_clientDataApi.Get(id));
            }
            catch
            {
                return NotFound();
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditClientDB(int id, string surname, string name, string patronymic, ulong numberPhone, string adress, string description)
        {
            Client client = _clientDataApi.Get(id);
            client.Surname = surname;
            client.Name = name;
            client.Patronymic = patronymic;
            client.NumberPhone = numberPhone;
            client.Adress = adress;
            client.Description = description;

            _clientDataApi.Edit(client);

            return Redirect("~/");
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetDataFromViewField(string surname, string name, string patronymic, ulong numberPhone, string adress, string description)
        {
            _clientDataApi.AddClient(
                new Client()
                {
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    NumberPhone = numberPhone,
                    Adress = adress,
                    Description = description
                });
            return Redirect("~/");
        }
    }
}
