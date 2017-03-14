using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.Data;
using Microsoft.AspNetCore.Authorization;

namespace WordHunt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private AppDbContext dbContext;

        public ValuesController(Data.AppDbContext ctx)
        {
            this.dbContext = ctx;
        }
        
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return dbContext.Users.Select(x => x.Email).ToList();
            //return new string[] { "value1", "value2", "value3" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
