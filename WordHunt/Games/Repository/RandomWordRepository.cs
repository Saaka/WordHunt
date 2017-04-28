using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Connection;

namespace WordHunt.Games.Repository
{
    public interface IRandomWordRepository
    {
        Task<IEnumerable<string>> GetRandomWords(int languageId, int count);
    }

    public class RandomWordRepository : IRandomWordRepository
    {
        private const string RandomWordsQuery = @"SELECT TOP (@Count)  [Value] FROM Words WHERE [LanguageId] = @LanguageId ORDER BY NEWID()";

        private readonly IDbConnectionFactory connectionFactory;

        public RandomWordRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<string>> GetRandomWords(int languageId, int count)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<string>(RandomWordsQuery, new { @Count = count, @LanguageId = languageId });
            }
        }
    }
}
