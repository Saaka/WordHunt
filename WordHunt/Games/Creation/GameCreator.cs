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
        private readonly IGameFieldsGenerator gameFieldsGenerator;
        private readonly IRandomWordRepository randomWordRepository;
        private readonly IGameFieldRepository gameFieldRepository;
        private readonly IGameTeamsGenerator gameTeamsGenerator;
        
        public GameCreator(IGameCreatorValidator validator,
            IDbTransactionProvider transactionProvider,
            IGameRepository gameRepository,
            IGameTeamRepository gameTeamRepository,
            IGameStatusRepository gameStatusRepository,
            IGameFieldsGenerator gameFieldsGenerator,
            IRandomWordRepository randomWordRepository,
            IGameFieldRepository gameFieldRepository,
            IGameTeamsGenerator gameTeamsGenerator)
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
        }

        public async Task<GameCreateResult> CreateGame(GameCreate game)
        {
            await validator.ValidateGame(game);
            var createdGame = await gameRepository.SaveNewGame(game);

            var teamsToCreate = await gameTeamsGenerator.GenerateTeams(createdGame.Id, game.Teams);
            await validator.ValidateTeams(teamsToCreate);
            var teams = await gameTeamRepository.CreateGameTeams(teamsToCreate);

            var firstTeamId = await gameTeamRepository.GetFirstTeamId(createdGame.Id);
            await gameStatusRepository.CreateInitialGameStatus(createdGame.Id, firstTeamId);
            
            var fieldCount = createdGame.BoardHeight * createdGame.BoardWidth;
            var words = await randomWordRepository.GetRandomWords(createdGame.LanguageId, fieldCount);
            var fields =  gameFieldsGenerator.GenerateFields(createdGame, teams, words);
            await validator.ValidateFields(fields);
            await gameFieldRepository.SaveGameFields(fields);

            return new GameCreateResult()
            {
                GameId = createdGame.Id
            };
        }
    }
}
