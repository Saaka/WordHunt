using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordHunt.Data.Connection;
using WordHunt.Games.Creation.Repository;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Creation
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
        private readonly IGameTeamRepository gameTeamRepository;

        public GameCreator(IGameCreatorValidator validator,
            IDbTransactionProvider transactionProvider,
            IGameRepository gameRepository,
            IGameTeamRepository gameTeamRepository)
        {
            this.validator = validator;
            this.transactionProvider = transactionProvider;
            this.gameRepository = gameRepository;
            this.gameTeamRepository = gameTeamRepository;
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            await validator.Validate(game);

            var gameId = await gameRepository.SaveNewGame(game);

            UpdateGameTeams(gameId, game.Teams);
            await gameTeamRepository.CreateGameTeams(game.Teams);

            return new GameCreateResult()
            {
                GameId = gameId
            };
        }

        private void UpdateGameTeams(int gameId, IEnumerable<GameTeamCreate> teams)
        {
            int order = 1;
            teams.ForEach(x =>
            {
                x.GameId = gameId;
                x.Order = order;
                order++;
            });
        }
    }
}
