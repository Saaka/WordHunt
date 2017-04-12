using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.Data;
using Microsoft.AspNetCore.Authorization;
using WordHunt.DataInterfaces.Words;
using WordHunt.DataInterfaces.Words.DTO.Access;
using WordHunt.Config.Auth;

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
        public async Task<GetWordListResult> Get()
        {
            return await wordProvider.GetWordList(new WordListRequest()
            {
                LanguageId = 1, 
                Page = 1,
                PageSize = 5
            });
        }
    }
}
