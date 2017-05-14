using WordHunt.Games.Moves.Validation;
using WordHunt.Games.Repository;

namespace WordHunt.Games.Moves
{
    public class GameMoveManager
    {
        private readonly IGameRepository gameRepository;
        private readonly IMoveValidatorFactory validationFactory;
        public GameMoveManager(IGameRepository gameRepository,
            IMoveValidatorFactory validationFactory)
        {
            this.gameRepository = gameRepository;
            this.validationFactory = validationFactory;
        }

        public async void PassTurn(int gameId, int userId)
        {
            var currentStatus = await gameRepository.GetCurrentGameState(gameId);

            var validator = validationFactory.GetMoveValidator(Base.Enums.Game.GameType.SingleDevice);
            validator.ValidatePassTurn(currentStatus, userId);
        }
    }
}
