﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyWebApplication.AuthClientApp;
using MyWebApplication.Data;
using MyWebApplication.DataContext;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly ClientDataApi _clientDataApi;
        private readonly UserManager<User> _userManager;



        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
            _clientDataApi = new ClientDataApi();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                bool result = _clientDataApi.AddUser(model);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Логин занят!");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Users()
        {
            ViewBag.Users = _clientDataApi.GetAllUsers();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            _clientDataApi.DeleteUser(id);
            return Redirect("~/");
        }
    }
}
