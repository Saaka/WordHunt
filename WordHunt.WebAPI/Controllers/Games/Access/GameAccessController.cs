using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Games.Repository;
using WordHunt.Models.Games.Access;

namespace WordHunt.WebAPI.Controllers.Games.Access
{
    [Route("api/game")]
    public class GameAccessController : Controller
    {
        private readonly IGameFieldRepository gameProvider;

        public GameAccessController(IGameFieldRepository gameProvider)
        {
            this.gameProvider = gameProvider;
        }

        [HttpGet("field/{gameId}")]
        public async Task<IEnumerable<Field>> GetGameFields(int gameId)
        {
            return await gameProvider.GetSimplifiedGameFields(gameId);
        }
    }
}
