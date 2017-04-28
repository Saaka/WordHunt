using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Connection;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Repository
{
    public interface IGameFieldRepository
    {
        Task SaveGameFields(IEnumerable<GameFieldCreate> fields);
    }

    public class GameFieldRepository : IGameFieldRepository
    {
        private const string CreateGameFieldQuery = @"INSERT INTO GameFields ([GameId], [Word], [Type], [TeamId], [ColumnIndex], [RowIndex], [Checked])
                                            VALUES (@GameId, @Word, @FieldType, @TeamId, @ColumnIndex, @RowIndex, 0)";

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
    }
}
