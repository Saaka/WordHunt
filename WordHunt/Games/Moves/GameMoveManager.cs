using System.Threading.Tasks;
using WordHunt.Data.Events;
using WordHunt.Games.Broadcaster;
using WordHunt.Games.Moves.Validation;
using WordHunt.Games.Repository;

namespace WordHunt.Games.Moves
{
    public interface IGameMoveManager
    {
        Task<TeamChanged> SkipRound(int gameId, int userId);
    }

    public class GameMoveManager : IGameMoveManager
    {
        private readonly IGameRepository gameRepository;
        private readonly IMoveValidatorFactory validationFactory;
        private readonly IGameTeamRepository gameTeamRepository;
        private readonly IGameStatusRepository gameStatusRepository;
        private readonly IEventBroadcaster eventBroadcaster;
        private readonly IGameMoveRepository gameMoveRepository;
        private readonly IGameFieldRepository gameFieldRepository;

        public GameMoveManager(IGameRepository gameRepository,
            IGameTeamRepository gameTeamRepository,
            IGameStatusRepository gameStatusRepository,
            IMoveValidatorFactory validationFactory,
            IEventBroadcaster eventBroadcaster,
            IGameMoveRepository gameMoveRepository,
            IGameFieldRepository gameFieldRepository)
        {
            this.gameRepository = gameRepository;
            this.validationFactory = validationFactory;
            this.gameTeamRepository = gameTeamRepository;
            this.gameStatusRepository = gameStatusRepository;
            this.eventBroadcaster = eventBroadcaster;
            this.gameMoveRepository = gameMoveRepository;
            this.gameFieldRepository = gameFieldRepository;
        }

        public async Task CheckField(int gameId, int userId, int fieldId)
        {
            var gameState = await gameRepository.GetCurrentGameState(gameId);
            var fieldState = await gameFieldRepository.GetFieldState(fieldId);

            var validator = validationFactory.GetMoveValidator(gameState.Type);
            validator.ValidateFieldCheck(gameState, fieldState, userId);

        }

        public async Task<TeamChanged> SkipRound(int gameId, int userId)
        {
            var currentState = await gameRepository.GetCurrentGameState(gameId);

            var validator = validationFactory.GetMoveValidator(currentState.Type);
            validator.ValidateRoundSkip(currentState, userId);

            var nextTeam = await gameTeamRepository.GetNextTeam(gameId);
            var newStatus = await gameStatusRepository.UpdateCurrentStatus(gameId, nextTeam.Id, Base.Enums.Game.Status.Ongoing);

            var teamChanged = new TeamChanged();
            teamChanged.ChangeReason = Base.Enums.Events.TeamChangeReason.SkipRound;
            teamChanged.GameId = gameId;
            teamChanged.LastTeamId = currentState.CurrentTeamId;
            teamChanged.NewTeamId = newStatus.CurrentTeamId;

            await gameMoveRepository.SaveMove(gameId, Base.Enums.Game.MoveType.SkipRound, currentState.CurrentTeamId);

            eventBroadcaster.TeamChanged(teamChanged);

            return teamChanged;
        }
    }
}
