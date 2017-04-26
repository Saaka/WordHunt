using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WordHunt.Config.Auth;
using WordHunt.Services.Languages;
using System.Collections.Generic;
using WordHunt.Models.Languages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordHunt.WebAPI.Controllers.Languages
{
    [Route("api/[controller]")]
    public class LanguagesController : Controller
    {
        private readonly ILanguageProvider languageProvider;

        public LanguagesController(ILanguageProvider languageProvider)
        {
            this.languageProvider = languageProvider;
        }

        [Authorize(Policy = SystemPolicies.AdminOnly)]
        [HttpGet("list")]
        public async Task<IEnumerable<LanguageModel>> Get()
        {
            return await languageProvider.GetLanguageList();
        }
    }
}
