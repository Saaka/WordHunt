using System;
using System.Threading.Tasks;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Create
{
    public interface IGameCreator
    {
        Task<GameCreateResult> CreateGame(GameCreate game);
    }

    public class GameCreator : IGameCreator
    {
        private readonly IGameCreatorValidator validator;
        public GameCreator(IGameCreatorValidator validator)
        {
            this.validator = validator;
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            await validator.Validate(game);
            

            return new GameCreateResult();
        }
    }
}
