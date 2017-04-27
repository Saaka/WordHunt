using System;
using System.Threading.Tasks;
using WordHunt.Data.Connection;
using WordHunt.Games.Create.Repository;
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
        private readonly IDbTransactionProvider transactionProvider;
        private readonly IGameRepository gameRepository;

        public GameCreator(IGameCreatorValidator validator,
            IDbTransactionProvider transactionProvider,
            IGameRepository gameRepository)
        {
            this.validator = validator;
            this.transactionProvider = transactionProvider;
            this.gameRepository = gameRepository;
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            await validator.Validate(game);

            return new GameCreateResult();
        }
    }
}
