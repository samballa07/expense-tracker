using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgetTracker.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetTrackerController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Item> Get()
        {

            return new Item[]{new Item
            {
                Amount = 2,
                Category = ItemCategory.Wants,
                DateAdded = new DateOnly(),
                Id = Guid.NewGuid(),
                Name = "Coffee"
            }};
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

