using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Models;
using BudgetTracker.Repositories.Interfaces;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetTracker.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public DbContext Db;
        public IUserRepository _userRepo;

        public AuthController(DbContext conn, IUserRepository userRepo)
        {
            Db = conn;
            _userRepo = userRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto request)
        {
            User? user = await this._userRepo.GetUserByIDAsync(7);
            // check if username is already in use
            //User? user = await this._userRepo.GetUserByUsernameAsync(request.Username);
            if (user is not null)
            {
                return Conflict($"User with username: {request.Username} already exists");
               
            }
            // if not, make a new user and add it to db
            // need to make a hash with salt
            return Ok(user);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<bool> PostAsync([FromBody] User user)
        {
            return await this._userRepo.AddUserAsync(user);
        }   

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

