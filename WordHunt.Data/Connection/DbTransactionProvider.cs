using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WordHunt.Data.Connection
{
    public interface IDbTransactionProvider
    {
        DbTransaction CreateTransaction();
    }

    public class DbTransactionProvider : IDbTransactionProvider
    {
        private IDbConnectionProvider connectionProvider;

        public DbTransactionProvider(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public DbTransaction CreateTransaction()
        {
            var transaction = new DbTransaction(connectionProvider.GetConnection());

            return transaction;
        }
    }

    public class DbTransaction : IDisposable
    {
        private readonly IDbTransaction transaction;
        private bool committed = false;

        public DbTransaction(IDbConnection connection)
        {
            transaction = connection.BeginTransaction();
        }

        public IDbTransaction GetTransaction()
        {
            return transaction;
        }

        public void CommitTransaction()
        {
            transaction.Commit();
            committed = true;
        }

        public void Dispose()
        {
            if (!committed)
                transaction.Rollback();

            transaction.Dispose();
        }
    }
}
