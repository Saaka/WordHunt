using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Base.Exceptions;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Creation
{
    public interface IGameCreatorValidator
    {
        Task ValidateGame(GameCreate game);
        Task ValidateTeams(IEnumerable<GameTeamCreate> teams);
    }
    
    public class GameCreatorValidator : IGameCreatorValidator
    {
        public async Task ValidateGame(GameCreate gameModel)
        {
            if (gameModel == null)
                throw new ValidationFailedException("No data to create the game");

            if (gameModel.TeamCount != gameModel.Teams.Count())
                throw new ValidationFailedException("Wrong team count");

            if (gameModel.BoardHeight < 5 || gameModel.BoardWidth < 5 
                || gameModel.BoardHeight > 8 || gameModel.BoardWidth > 8)
                throw new ValidationFailedException("Wrong board size");

            if (gameModel.UserId <= 0)
                throw new ValidationFailedException("Game must be assigned to user");

            if (gameModel.LanguageId <= 0)
                throw new ValidationFailedException("Game must have selected language");

            if (string.IsNullOrEmpty(gameModel.Name))
                throw new ValidationFailedException("Game must have a name");

            if (gameModel.Teams == null || !gameModel.Teams.Any())
                throw new ValidationFailedException("Game must have teams");
        }

        public async Task ValidateTeams(IEnumerable<GameTeamCreate> teams)
        {
            if (teams.Select(x => x.Name.Trim()).Distinct().Count() != teams.Count())
                throw new ValidationFailedException("Teams must have different names");

            if (teams.Any(x => x.FieldCount == 0))
                throw new ValidationFailedException("Teams must have assigned field count");

            if (teams.Any(x => x.Order == 0))
                throw new ValidationFailedException("Teams must have assigned order");
        }
    }
}
