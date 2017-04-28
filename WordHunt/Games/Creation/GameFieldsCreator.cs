using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Games.Repository;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Creation
{
    public interface IGameFieldsCreator
    {
        Task CreateFields(GameCreated createdGame, IEnumerable<GameTeamCreated> teams);
    }

    public class GameFieldsCreator : IGameFieldsCreator
    {
        private readonly IRandomWordRepository randomWordRepository;
        private readonly IGameFieldRepository gameFieldRepository;

        public GameFieldsCreator(IRandomWordRepository randomWordRepository,
            IGameFieldRepository gameFieldRepository)
        {
            this.randomWordRepository = randomWordRepository;
            this.gameFieldRepository = gameFieldRepository;
        }

        public  async Task CreateFields(GameCreated createdGame, IEnumerable<GameTeamCreated> teams)
        {
            //TODO PUT THIS TO GAME CREATOR
            var fieldCount = createdGame.BoardHeight * createdGame.BoardWidth;
            var words = (await randomWordRepository.GetRandomWords(createdGame.LanguageId, fieldCount)).ToArray();

            var fields = MapToCreateModel(createdGame, words);

            await gameFieldRepository.SaveGameFields(fields);
        }

        private IEnumerable<GameFieldCreate> MapToCreateModel(GameCreated createdGame, string[] words)
        {
            var fields = new List<GameFieldCreate>();
            int wordIndex = 0;
            for (int r = 0; r < createdGame.BoardHeight; r++)
            {
                for (int c = 0; c < createdGame.BoardWidth; c++)
                {
                    var field = CreateField(createdGame.Id, words[wordIndex], r, c);
                    fields.Add(field);
                    wordIndex++;
                }
            }

            return fields;
        }

        private GameFieldCreate CreateField(int id, string word, int rowIndex, int columnIndex)
        {
            return new GameFieldCreate()
            {
                GameId = id,
                RowIndex = rowIndex,
                ColumnIndex = columnIndex,
                Word = word ,
                FieldType = Base.Enums.Game.FieldType.Empty,
                TeamId = 1
            };
        }
    }
}
