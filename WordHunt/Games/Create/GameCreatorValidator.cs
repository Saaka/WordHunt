using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordHunt.Models.Games.Creation;
using WordHunt.Services.Exceptions;

namespace WordHunt.Games.Create
{
    public interface IGameCreatorValidator
    {
        Task Validate(GameCreate game);
    }
    
    public class GameCreatorValidator : IGameCreatorValidator
    {
        public async Task Validate(GameCreate game)
        {
            if (game == null)
                throw new ValidationFailedException("No data to create the game");

            if (game.TeamCount != game.Teams.Count())
                throw new ValidationFailedException("Wrong team count");

            if (game.BoardHeight < 5 || game.BoardWidth < 5 
                || game.BoardHeight > 8 || game.BoardWidth > 8)
                throw new ValidationFailedException("Wrong board size");

            if (game.UserId <= 0)
                throw new ValidationFailedException("Game must be assigned to user");

            if (string.IsNullOrEmpty(game.Name))
                throw new ValidationFailedException("Game must have a name");

            if (game.Teams == null || !game.Teams.Any())
                throw new ValidationFailedException("Game must have teams");

            if (game.Teams.Select(x => x.Name.Trim()).Distinct().Count() != game.Teams.Count())
                throw new ValidationFailedException("Teams must have different names");
        }
    }
}
