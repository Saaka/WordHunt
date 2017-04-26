using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Interfaces.Games;
using WordHunt.Interfaces.Games.DTO;
using WordHunt.Interfaces.Games.Result;

namespace WordHunt.Games.Create
{
    public class GameCreator : IGameCreator
    {
        private readonly IGameCreatorValidator validator;
        public GameCreator(IGameCreatorValidator validator)
        {
            this.validator = validator;
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            try
            {
                var result = await validator.Validate(game);
                if (!result.IsSuccess) return new GameCreateResult(result.Error);


                
                return new GameCreateResult();
            }
            catch(Exception ex)
            {
                return new GameCreateResult(ex.ToString());
            }
        }
    }
}
