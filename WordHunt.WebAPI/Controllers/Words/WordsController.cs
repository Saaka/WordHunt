using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WordHunt.Config.Auth;
using WordHunt.Services.Words;
using WordHunt.Models.Words.Creation;
using WordHunt.Models.Words.Access;
using WordHunt.Models.Words.Modification;

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
        public async Task<WordListGetResult> GetWordList([FromBody]WordListGet request)
        {
            return await wordProvider.GetWordList(request);
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("add")]
        public async Task<WordCreateResult> CreateWord([FromBody]WordCreate request)
        {
            return await wordCreator.CreateWord(request);
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpPost("update")]
        public async Task<WordUpdateResult> UpdateWord([FromBody]WordUpdate request)
        {
            return await wordUpdater.UpdateWord(request);
        }
    }
}
