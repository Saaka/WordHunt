using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordHunt.DataInterfaces.Games.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordHunt.WebAPI.Controllers.Game
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        // POST api/values
        [HttpPost]
        public Task<GameCreate> Post([FromBody]GameCreate model)
        {
            return System.Threading.Tasks.Task.FromResult(model);
        }
    }
}
