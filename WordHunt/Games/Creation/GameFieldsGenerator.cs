using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Games.Repository;
using WordHunt.Models.Games.Creation;
using WordHunt;

namespace WordHunt.Games.Creation
{
    public interface IGameFieldsGenerator
    {
        IEnumerable<GameFieldCreate> GenerateFields(GameCreated createdGame, IEnumerable<GameTeamCreated> teams, IEnumerable<string> words);
    }

    public class GameFieldsGenerator : IGameFieldsGenerator
    {
        public IEnumerable<GameFieldCreate> GenerateFields(GameCreated createdGame, IEnumerable<GameTeamCreated> teams, IEnumerable<string> words)
        {
            var wordsArray = words.ToArray();
            var fieldsToReturn = CreateFields(createdGame, wordsArray);

            var fieldsToUpdate = fieldsToReturn.ToList();
            fieldsToUpdate = UpdateTrapFields(fieldsToUpdate, createdGame.TrapCount);
            fieldsToUpdate = UpdateTeamFields(fieldsToUpdate, teams);

            return fieldsToReturn;
        }

        private IEnumerable<GameFieldCreate> CreateFields(GameCreated createdGame, string[] wordsArray)
        {
            var fieldsToReturn = new GameFieldCreate[wordsArray.Count()];

            int wordIndex = 0;
            for (int r = 0; r < createdGame.BoardHeight; r++)
            {
                for (int c = 0; c < createdGame.BoardWidth; c++)
                {
                    var field = CreateField(createdGame.Id, wordsArray[wordIndex], r, c);
                    fieldsToReturn[wordIndex] = field;
                    wordIndex++;
                }
            }

            return fieldsToReturn;
        }

        private List<GameFieldCreate> UpdateTrapFields(List<GameFieldCreate> fields, int trapCount)
        {
            fields = fields.OrderBy(x => Guid.NewGuid()).ToList();
            
            var trapFields = fields.Split(trapCount);
            trapFields.ForEach(x => x.FieldType = Base.Enums.Game.FieldType.Trap);

            return fields;
        }

        private List<GameFieldCreate> UpdateTeamFields(List<GameFieldCreate> fields, IEnumerable<GameTeamCreated> teams)
        {
            fields = fields.OrderBy(x => Guid.NewGuid()).ToList();

            teams.ForEach(t =>
            {
                var teamFields = fields.Split(t.FieldCount);
                teamFields.ForEach(f =>
                {
                    f.TeamId = t.Id;
                    f.FieldType = Base.Enums.Game.FieldType.Team;
                });
            });

            return fields;
        }

        private GameFieldCreate CreateField(int id, string word, int rowIndex, int columnIndex)
        {
            return new GameFieldCreate()
            {
                GameId = id,
                RowIndex = rowIndex,
                ColumnIndex = columnIndex,
                Word = word,
                FieldType = Base.Enums.Game.FieldType.Empty
            };
        }
    }
}
