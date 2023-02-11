using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Infrastructure;
using MyWebApplication.Models;
using System;
using System.Collections.Generic;

namespace MyWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClientData(uint id)
        {
            Client client = ClientList_s.GetClientById(id);
            
            if (client == null)
            {
                return NotFound();
            }
            else
            {
                return View(client);
            }
        }
    }
}
