using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.DataInterfaces.Words;
using Microsoft.AspNetCore.Authorization;
using WordHunt.Config.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordHunt.WebAPI.Controllers.Words
{
    [Route("api/[controller]")]
    public class WordsController : Controller
    {
        private IWordProvider wordProvider;

        public WordsController(IWordProvider wordProvider)
        {
            this.wordProvider = wordProvider;
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("list")]
        public Task<GetWordListResult> GetWordList([FromBody]WordListRequest request)
        {
            return wordProvider.GetWordList(request);
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
