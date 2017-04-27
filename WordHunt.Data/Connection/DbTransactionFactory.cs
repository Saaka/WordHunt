using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WordHunt.Data.Connection
{
    public interface IDbTransactionFactory
    {
        DbTransaction CreateTransaction();
    }

    public class DbTransactionFactory : IDbTransactionFactory
    {
        private IDbConnectionProvider connectionProvider;

        public DbTransactionFactory(IDbConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public DbTransaction CreateTransaction()
        {
            return new DbTransaction(connectionProvider.GetConnection());
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
