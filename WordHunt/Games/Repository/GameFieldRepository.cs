using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Connection;
using WordHunt.Models.Games.Access;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Repository
{
    public interface IGameFieldRepository
    {
        Task SaveGameFields(IEnumerable<GameFieldCreate> fields);
        Task<IEnumerable<Field>> GetSimplifiedGameFields(int gameId);
    }

    public class GameFieldRepository : IGameFieldRepository
    {
        private const string CreateGameFieldQuery = @"INSERT INTO GameFields ([GameId], [Word], [Type], [TeamId], [ColumnIndex], [RowIndex], [Checked])
                                            VALUES (@GameId, @Word, @FieldType, @TeamId, @ColumnIndex, @RowIndex, 0)";

        private const string GetGameFieldsQuery = @"SELECT [Id], [Word], [Checked], [CheckedByTeamId], [ColumnIndex], [RowIndex],
                                                    CASE WHEN [CheckedByTeamId] = [TeamId] AND [Checked] = 1 THEN 1 ELSE 0 END AS [CheckedByRightTeam]
                                                    FROM GameFields
                                                    WHERE [GameId] = @GameId";

        private readonly IDbConnectionFactory connectionFactory;

        public GameFieldRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task SaveGameFields(IEnumerable<GameFieldCreate> fields)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(CreateGameFieldQuery, fields);
            }
        }

        public async Task<IEnumerable<Field>> GetSimplifiedGameFields(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Field>(GetGameFieldsQuery, new { GameId = gameId } );
            }
        }
    }
}
