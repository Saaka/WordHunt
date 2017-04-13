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
        private readonly IWordUpdater wordUpdater;

        public WordsController(IWordProvider wordProvider,
            IWordCreator wordCreator,
            IWordUpdater wordUpdater)
        {
            this.wordProvider = wordProvider;
            this.wordCreator = wordCreator;
            this.wordUpdater = wordUpdater;
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("list")]
        public async Task<WordListGetResult> GetWordList([FromBody]WordListRequest request)
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
        public async Task<WordCreateResult> CreateWord([FromBody]WordCreateRequest request)
        {
            return await wordCreator.CreateWord(request);
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("update")]
        public async Task<WordUpdateResult> UpdateWord([FromBody]WordUpdateRequest request)
        {
            return await wordUpdater.UpdateWord(request);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
