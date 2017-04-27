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
        void SaveGame(string name, DbTransaction transaction = null);
    }

    public class GameRepository : IGameRepository
    {
        private readonly IDbConnectionProvider connectionProvider;
        public GameRepository(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public void SaveGame(string name, DbTransaction transaction = null)
        {
            var connection = connectionProvider.GetConnection();

            connection.Execute(@"INSERT INTO Words(LanguageId, Value) VALUES (@LanguageId, @Value)",
                new { LanguageId = 1, Value = name }, transaction?.GetTransaction());
        }
    }
}
