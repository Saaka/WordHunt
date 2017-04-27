using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WordHunt.Config;

namespace WordHunt.Data.Connection
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates new instance of IDbConnection. This instance is not scoped and have to be managed manually. Connection is opened by default.
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }

    public interface IDbConnectionProvider : IDisposable
    {
        /// <summary>
        /// Gets instance of IDbConnection. This instance will be disposed after the lifespan of the provider scope. Dont dispose it manually. 
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }

    public class DbConnectionProvider : IDbConnectionProvider, IDbConnectionFactory
    {
        private readonly IAppConfiguration appConfig;
        private IDbConnection connection;
        private object createLock = new object();
        private object openLock = new object();

        public DbConnectionProvider(IAppConfiguration appConfig)
        {
            this.appConfig = appConfig;
        }

        public IDbConnection GetConnection()
        {
            CreateScopedConnection();
            OpenConnection();

            return connection;
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(appConfig.DbConnectionString);
            conn.Open();

            return conn;
        }

        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                lock (openLock)
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                }
            }
        }

        private void CreateScopedConnection()
        {
            if (connection == null)
            {
                lock (createLock)
                {
                    if (connection == null)
                    {
                        connection = CreateConnection();
                    }
                }
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Dispose();
            }
        }
    }
}
