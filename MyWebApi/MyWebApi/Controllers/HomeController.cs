using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Models.AuthClientApp;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ClientContext _db;
        private readonly Repository _repository;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ClientContext db)
        {
            _repository = new Repository();
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        [HttpGet]
        [Route("GetClient/{id}")]
        public async Task<Client> Get(int id)
        {
            return await _repository.GetById(id, _db);
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> GetAll(int id)
        {
            return await _repository.GetAll(_db);
        }

        [HttpPost]
        public async Task Add([FromBody] Client client)
        {
            await _repository.AddClient(client, _db);
        }

        [HttpPut]
        public async Task Edit([FromBody] Client client)
        {
            await _repository.EditClient(client, _db);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.DeleteClient(id, _db);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repository.GetAllUsers(_db);
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task Delete(string id)
        {
            await _repository.DeleteUser(id, _db);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<User> Login([FromBody] UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    return await _repository.GetUserByUserName(model.LoginProp, _db);
                }
            }
            return null;
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<bool> AddUser([FromBody]UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User { UserName = model.LoginProp };
                    var createResult = await _userManager.CreateAsync(user, model.Password);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        [Route("GetRoleName/{id}")]
        [HttpGet]
        public async Task<string> GetRoleNameById(string id)
        {
            return await _repository.GetRoleNameByRoleId(id, _db);
        }

        [Route("GetUserRole/{id}")]
        [HttpGet]
        public async Task<string> GetUserRoleById(string id)
        {
            return await _repository.GetUserRoleById(id, _db);
        }
    }
}
