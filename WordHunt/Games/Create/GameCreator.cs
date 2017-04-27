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
        private readonly IDbTransactionFactory transactionFactory;
        private readonly IGameRepository gameRepository;

        public GameCreator(IGameCreatorValidator validator,
            IDbTransactionFactory transactionFactory,
            IGameRepository gameRepository)
        {
            this.validator = validator;
            this.transactionFactory = transactionFactory;
            this.gameRepository = gameRepository;
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            await validator.Validate(game);

            using (var transaction = transactionFactory.CreateTransaction())
            {
                gameRepository.SaveGame("Saved", transaction);
                transaction.CommitTransaction();
            }
            gameRepository.SaveGame("Saved2");

            return new GameCreateResult();
        }
    }
}
