using WordHunt.Games.Moves.Validation;
using WordHunt.Games.Repository;

namespace WordHunt.Games.Moves
{
    public class GameMoveManager
    {
        private readonly IGameRepository gameRepository;
        private readonly IMoveValidatorFactory validationFactory;
        private readonly IGameTeamRepository gameTeamRepository;
        private readonly IGameStatusRepository gameStatusRepository;

        public GameMoveManager(IGameRepository gameRepository,
            IGameTeamRepository gameTeamRepository,
            IGameStatusRepository gameStatusRepository,
            IMoveValidatorFactory validationFactory)
        {
            this.gameRepository = gameRepository;
            this.validationFactory = validationFactory;
            this.gameTeamRepository = gameTeamRepository;
            this.gameStatusRepository = gameStatusRepository;
        }

        public async void PassTurn(int gameId, int userId)
        {
            var currentState = await gameRepository.GetCurrentGameState(gameId);

            var validator = validationFactory.GetMoveValidator(Base.Enums.Game.GameType.SingleDevice);
            validator.ValidatePassTurn(currentState, userId);

            var nextTeam = await gameTeamRepository.GetNextTeam(gameId);
            var newStatus = await gameStatusRepository.UpdateCurrentStatus(gameId, nextTeam.Id, Base.Enums.Game.Status.Ongoing);

            //Todo return state + emit change to clients
        }
    }
}
