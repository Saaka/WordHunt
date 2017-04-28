using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordHunt.Data.Connection;
using WordHunt.Games.Repository;
using WordHunt.Models.Games.Creation;
using System;

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
        private readonly IGameStatusRepository gameStatusRepository;
        private readonly IGameFieldsCreator gameFieldsCreator;

        public GameCreator(IGameCreatorValidator validator,
            IDbTransactionProvider transactionProvider,
            IGameRepository gameRepository,
            IGameTeamRepository gameTeamRepository,
            IGameStatusRepository gameStatusRepository,
            IGameFieldsCreator gameFieldsCreator)
        {
            this.validator = validator;
            this.transactionProvider = transactionProvider;
            this.gameRepository = gameRepository;
            this.gameTeamRepository = gameTeamRepository;
            this.gameStatusRepository = gameStatusRepository;
            this.gameFieldsCreator = gameFieldsCreator;
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            await validator.ValidateGame(game);
            var createdGame = await gameRepository.SaveNewGame(game);

            UpdateGameTeams(createdGame.Id, game.Teams);
            await validator.ValidateTeams(game.Teams);
            var teams = await gameTeamRepository.CreateGameTeams(game.Teams);

            var firstTeamId = await gameTeamRepository.GetFirstTeamId(createdGame.Id);

            await gameStatusRepository.CreateInitialGameStatus(createdGame.Id, firstTeamId);

            await gameFieldsCreator.CreateFields(createdGame, teams);

            return new GameCreateResult()
            {
                GameId = createdGame.Id
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
