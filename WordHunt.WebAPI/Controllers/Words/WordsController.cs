using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.DataInterfaces.Words;
using Microsoft.AspNetCore.Authorization;
using WordHunt.Config.Auth;
using WordHunt.DataInterfaces.Words.Result;
using WordHunt.DataInterfaces.Words.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordHunt.WebAPI.Controllers.Words
{
    [Route("api/[controller]")]
    public class WordsController : Controller
    {
        private readonly IWordProvider wordProvider;
        private readonly IWordCreator wordCreator;

        public WordsController(IWordProvider wordProvider,
            IWordCreator wordCreator)
        {
            this.wordProvider = wordProvider;
            this.wordCreator = wordCreator;
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("list")]
        public async Task<GetWordListResult> GetWordList([FromBody]WordListRequest request)
        {
            return await wordProvider.GetWordList(request);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("add")]
        public async Task<CreateWordResult> CreateWord([FromBody]WordCreateRequest request)
        {
            return await wordCreator.CreateWord(request);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
