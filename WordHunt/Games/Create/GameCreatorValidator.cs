using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.DataInterfaces.Games.DTO;
using WordHunt.Services.Base;

namespace WordHunt.Games.Create
{
    public interface IGameCreatorValidator
    {
        Task<ValidatorResult> Validate(GameCreate game);
    }
    
    public class GameCreatorValidator : IGameCreatorValidator
    {
        public async Task<ValidatorResult> Validate(GameCreate game)
        {
            if (game.TeamCount != game.Teams.Count())
                return new ValidatorResult("Wrong team count");

            if (game.BoardHeight <= 5 || game.BoardWidth <= 5 
                || game.BoardHeight > 8 || game.BoardWidth > 8)
                return new ValidatorResult("Wrong board size");

            if (game.UserId <= 0)
                return new ValidatorResult("Game must be assigned to user");

            if (string.IsNullOrEmpty(game.Name))
                return new ValidatorResult("Game must have a name");

            if (game.Teams == null || !game.Teams.Any())
                return new ValidatorResult("Game must have teams");

            if (game.Teams.Select(x => x.Name.Trim()).Distinct().Count() != game.Teams.Count())
                return new ValidatorResult("Teams must have different names");



            return new ValidatorResult();
        }
    }
}
