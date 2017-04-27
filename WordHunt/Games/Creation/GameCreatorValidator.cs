using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Models.Games.Creation;
using WordHunt.Services.Exceptions;

namespace WordHunt.Games.Creation
{
    public interface IGameCreatorValidator
    {
        Task Validate(GameCreate game);
    }
    
    public class GameCreatorValidator : IGameCreatorValidator
    {
        public async Task Validate(GameCreate gameModel)
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

            if (string.IsNullOrEmpty(gameModel.Name))
                throw new ValidationFailedException("Game must have a name");

            if (gameModel.Teams == null || !gameModel.Teams.Any())
                throw new ValidationFailedException("Game must have teams");

            if (gameModel.Teams.Select(x => x.Name.Trim()).Distinct().Count() != gameModel.Teams.Count())
                throw new ValidationFailedException("Teams must have different names");
        }
    }
}
