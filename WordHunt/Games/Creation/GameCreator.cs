using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordHunt.Data.Connection;
using WordHunt.Games.Repository;
using WordHunt.Models.Games.Creation;
using System;
using WordHunt.Games.Broadcaster;

namespace WordHunt.Games.Creation
{
    public interface IGameCreator
    {
        Task<GameCreateResult> CreateGame(GameCreate game);
        Task<GameCreateResult> CreateGameBasedOnAnother(int gameId);
    }

    public class GameCreator : IGameCreator
    {
        private readonly IGameCreatorValidator validator;
        private readonly IDbTransactionProvider transactionProvider;
        private readonly IGameRepository gameRepository;
        private readonly IGameTeamRepository gameTeamRepository;
        private readonly IGameStatusRepository gameStatusRepository;
        private readonly IGameFieldsGenerator gameFieldsGenerator;
        private readonly IRandomWordRepository randomWordRepository;
        private readonly IGameFieldRepository gameFieldRepository;
        private readonly IGameTeamsGenerator gameTeamsGenerator;
        private readonly IEventBroadcaster eventBroadcaster;


        public GameCreator(IGameCreatorValidator validator,
            IDbTransactionProvider transactionProvider,
            IGameRepository gameRepository,
            IGameTeamRepository gameTeamRepository,
            IGameStatusRepository gameStatusRepository,
            IGameFieldsGenerator gameFieldsGenerator,
            IRandomWordRepository randomWordRepository,
            IGameFieldRepository gameFieldRepository,
            IGameTeamsGenerator gameTeamsGenerator,
            IEventBroadcaster eventBroadcaster)
        {
            this.validator = validator;
            this.transactionProvider = transactionProvider;
            this.gameRepository = gameRepository;
            this.gameTeamRepository = gameTeamRepository;
            this.gameStatusRepository = gameStatusRepository;
            this.gameFieldsGenerator = gameFieldsGenerator;
            this.randomWordRepository = randomWordRepository;
            this.gameFieldRepository = gameFieldRepository;
            this.gameTeamsGenerator = gameTeamsGenerator;
            this.eventBroadcaster = eventBroadcaster;
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            await validator.ValidateGame(game);
            var createdGame = await gameRepository.SaveNewGame(game);

            var teamsToCreate = await gameTeamsGenerator.GenerateTeams(createdGame.Id, game.Teams);
            await CreateGameData(createdGame, teamsToCreate);

            return new GameCreateResult()
            {
                GameId = createdGame.Id
            };
        }

        public async Task<GameCreateResult> CreateGameBasedOnAnother(int gameId)
        {
            var game = await gameRepository.CreatedBasedOnGame(gameId);

            var teamsToCreate = await gameTeamsGenerator.GenerateTeamsBasedOnGame(game.Id, gameId);
            await CreateGameData(game, teamsToCreate);

            eventBroadcaster.RestartGame(new Models.Events.GameRestarted()
            {
                GameId = game.Id,
                OldGameId = gameId
            });

            return new GameCreateResult()
            {
                GameId = game.Id
            };
        }

        private async Task CreateGameData(GameCreated game, IEnumerable<GameTeamCreate> teamsToCreate)
        {
            await validator.ValidateTeams(teamsToCreate);
            var teams = await gameTeamRepository.CreateGameTeams(teamsToCreate);

            var firstTeamId = await gameTeamRepository.GetFirstTeamId(game.Id);
            await gameStatusRepository.CreateInitialGameStatus(game.Id, firstTeamId);

            var fieldCount = game.BoardHeight * game.BoardWidth;
            var words = await randomWordRepository.GetRandomWords(game.LanguageId, fieldCount);
            var fields = gameFieldsGenerator.GenerateFields(game, teams, words);
            await validator.ValidateFields(fields);
            await gameFieldRepository.SaveGameFields(fields);
        }
    }
}
