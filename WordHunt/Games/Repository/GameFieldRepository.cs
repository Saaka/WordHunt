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

    class GameFieldRepository : IGameFieldRepository
    {        
        private readonly IDbConnectionFactory connectionFactory;

        public GameFieldRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task SaveGameFields(IEnumerable<GameFieldCreate> fields)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(CreationQueries.CreateGameFieldQuery, fields);
            }
        }

        public async Task<IEnumerable<Field>> GetSimplifiedGameFields(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Field>(AccessQueries.GetGameFieldsQuery, new { GameId = gameId });
            }
        }
    }
}
