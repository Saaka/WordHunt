using System.Threading.Tasks;
using WordHunt.Models.Events;
using WordHunt.Games.Broadcaster;
using WordHunt.Games.Moves.Validation;
using WordHunt.Games.Repository;
using System;
using WordHunt.Base.Enums.Game;
using WordHunt.Models.Games.Access;
using WordHunt.Games.Moves.Helpers;

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
        private readonly INextTeamProvider nextTeamProvider;
        private readonly IEndGameChecker endGameChecker;
        
        public GameMoveManager(IGameRepository gameRepository,
            IGameTeamRepository gameTeamRepository,
            IGameStatusRepository gameStatusRepository,
            IMoveValidatorFactory validationFactory,
            IEventBroadcaster eventBroadcaster,
            IGameMoveRepository gameMoveRepository,
            IGameFieldRepository gameFieldRepository,
            INextTeamProvider nextTeamProvider,
            IEndGameChecker endGameChecker)
        {
            this.gameRepository = gameRepository;
            this.validationFactory = validationFactory;
            this.gameTeamRepository = gameTeamRepository;
            this.gameStatusRepository = gameStatusRepository;
            this.eventBroadcaster = eventBroadcaster;
            this.gameMoveRepository = gameMoveRepository;
            this.gameFieldRepository = gameFieldRepository;
            this.nextTeamProvider = nextTeamProvider;
            this.endGameChecker = endGameChecker;
        }

        public async Task<FieldChecked> CheckField(int gameId, int userId, int fieldId)
        {
            var gameState = await gameRepository.GetCurrentGameState(gameId);
            var fieldState = await gameFieldRepository.GetFieldState(fieldId);

            var validator = validationFactory.GetMoveValidator(gameState.Type);
            validator.ValidateFieldCheck(gameState, fieldState, userId);

            var fieldChecked = CreateFieldCheckedEvent(gameId, fieldId, fieldState);

            var endGame = await endGameChecker.VerifyEndGame(gameState, fieldChecked);

            if(endGame.GameEnded)
            {
                await gameRepository.EndGame(gameId, endGame.WinningTeamId.Value);
                if (endGame.WinningTeamId.Value == gameState.CurrentTeamId)
                    await gameTeamRepository.DecrementRemainingFieldCount(endGame.WinningTeamId.Value);
                else
                {
                    var teamChanged = CreateTeamChangedEvent(gameId, gameState.CurrentTeamId, endGame.WinningTeamId.Value, Base.Enums.Events.TeamChangeReason.WonGame);
                    eventBroadcaster.TeamChanged(teamChanged);
                }

                eventBroadcaster.EndGame(new GameEnded()
                {
                    GameId = gameId,
                    WinningTeamId = endGame.WinningTeamId.Value
                });
            }
            else
            {
                if(fieldChecked.Type == FieldType.Team && fieldChecked.TeamId == gameState.CurrentTeamId)
                {
                    await gameTeamRepository.DecrementRemainingFieldCount(fieldChecked.TeamId);
                }
                else
                {
                    if (fieldChecked.Type == FieldType.Team)
                        await gameTeamRepository.DecrementRemainingFieldCount(fieldChecked.TeamId);
                    var nextTeam = await nextTeamProvider.GetNextTeam(gameId, gameState.EndMode);
                    var newStatus = await gameStatusRepository.UpdateCurrentStatus(gameId, nextTeam.Id, Status.Ongoing);

                    var teamChanged = CreateTeamChangedEvent(gameId, gameState.CurrentTeamId, nextTeam.Id, Base.Enums.Events.TeamChangeReason.WrongGuess);
                    eventBroadcaster.TeamChanged(teamChanged);
                }
            }

            await gameFieldRepository.CheckField(fieldChecked.FieldId, gameState.CurrentTeamId);
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

            var nextTeam = await nextTeamProvider.GetNextTeam(gameId, currentState.EndMode);
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
