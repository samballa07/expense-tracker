using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Models;
using MySqlConnector;
using System.Data;
using System.Data.Common;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetTrackerController : ControllerBase
    {
        public DbContext Db;

        public BudgetTrackerController(DbContext conn)
        {
            Db = conn;
        }
        // GET: api/values
        [HttpGet]
        async public Task<IActionResult> Get()
        {
            await Db.Connection.OpenAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM `User`";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return new OkObjectResult(result);
        }

        private async Task<List<User>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<User>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    byte[] passwordsalt = new byte[4];
                    byte[] passwordhash = new byte[128];
                    var userid = reader.GetInt32(0);
                    var username = reader.GetString(1);
                    reader.GetBytes(2, 0, passwordhash, 0, 128);
                    reader.GetBytes(3, 0, passwordsalt, 0, 4);

                    var user = new User
                    {
                        UserId = userid,
                        Username = username,
                        PasswordHash = passwordhash,
                        PasswordSalt = passwordsalt
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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

