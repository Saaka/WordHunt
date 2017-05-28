using System.Threading.Tasks;
using WordHunt.Models.Events;
using WordHunt.Games.Broadcaster;
using WordHunt.Games.Moves.Validation;
using WordHunt.Games.Repository;
using System;
using WordHunt.Base.Enums.Game;
using WordHunt.Models.Games.Access;

namespace WordHunt.Games.Moves
{
    public interface IGameMoveManager
    {
        Task<TeamChanged> SkipRound(int gameId, int userId);
        Task<FieldChecked> CheckField(int gameId, int userId, int fieldId);
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

        public async Task<FieldChecked> CheckField(int gameId, int userId, int fieldId)
        {
            var gameState = await gameRepository.GetCurrentGameState(gameId);
            var fieldState = await gameFieldRepository.GetFieldState(fieldId);

            var validator = validationFactory.GetMoveValidator(gameState.Type);
            validator.ValidateFieldCheck(gameState, fieldState, userId);

            var fieldChecked = CreateFieldCheckedEvent(gameId, fieldId, fieldState);



            eventBroadcaster.FieldChecked(fieldChecked);

            return fieldChecked;
        }

        private FieldChecked CreateFieldCheckedEvent(int gameId, int fieldId, CurrentFieldState state)
        {
            FieldChecked fieldChecked = new FieldChecked();
            fieldChecked.FieldId = fieldId;
            fieldChecked.GameId = gameId;
            fieldChecked.Checked = true;
            fieldChecked.Type = state.Type;
            if (state.Type == FieldType.Team)
                fieldChecked.TeamId = state.TeamId.Value;

            return fieldChecked;
        }

        public async Task<TeamChanged> SkipRound(int gameId, int userId)
        {
            var currentState = await gameRepository.GetCurrentGameState(gameId);

            var validator = validationFactory.GetMoveValidator(currentState.Type);
            validator.ValidateRoundSkip(currentState, userId);

            var nextTeam = await gameTeamRepository.GetNextTeam(gameId);
            var newStatus = await gameStatusRepository.UpdateCurrentStatus(gameId, nextTeam.Id, Status.Ongoing);

            var teamChanged = CreateTeamChangedEvent(gameId, currentState.CurrentTeamId, newStatus.CurrentTeamId, Base.Enums.Events.TeamChangeReason.SkipRound);

            await gameMoveRepository.SaveMove(gameId, Base.Enums.Game.MoveType.SkipRound, currentState.CurrentTeamId);

            eventBroadcaster.TeamChanged(teamChanged);

            return teamChanged;
        }

        private static TeamChanged CreateTeamChangedEvent(int gameId, int currentTeamId, int newTeamId, Base.Enums.Events.TeamChangeReason changeReason)
        {
            var teamChanged = new TeamChanged();
            teamChanged.ChangeReason = changeReason;
            teamChanged.GameId = gameId;
            teamChanged.LastTeamId = currentTeamId;
            teamChanged.NewTeamId = newTeamId;
            return teamChanged;
        }
    }
}
