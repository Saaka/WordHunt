using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.Data;
using Microsoft.AspNetCore.Authorization;
using WordHunt.DataInterfaces.Words;
using WordHunt.Config.Auth;
using WordHunt.DataInterfaces.Words.Result;
using WordHunt.DataInterfaces.Words.Request;

namespace WordHunt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IWordProvider wordProvider;

        public ValuesController(IWordProvider wordProvider)
        {
            this.wordProvider = wordProvider;
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpGet]
        public async Task<WordListGetResult> Get()
        {
            return await wordProvider.GetWordList(new WordListGetRequest()
            {
                LanguageId = 1, 
                Page = 1,
                PageSize = 5
            });
        }
    }
}
