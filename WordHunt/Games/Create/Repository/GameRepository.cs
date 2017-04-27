using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WordHunt.Data.Connection;

namespace WordHunt.Games.Create.Repository
{
    public interface IGameRepository
    {
        void SaveGame(string name);
    }

    public class GameRepository : IGameRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        public GameRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void SaveGame(string name)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                connection.Execute(@"INSERT INTO Words(LanguageId, Value) VALUES (@LanguageId, @Value)",
                    new { LanguageId = 1, Value = name });
            }
        }
    }
}
