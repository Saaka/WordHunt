using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WordHunt.Config;

namespace WordHunt.Data.Connection
{
    public interface IDbConnectionProvider : IDisposable
    {
        /// <summary>
        /// Gets instance of IDbConnection. This instance will be disposed after the lifespan of the scope. Dont dispose it manually. 
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }

    public class DbConnectionProvider : IDbConnectionProvider
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
            CreateConnection();
            OpenConnection();

            return connection;
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

        private void CreateConnection()
        {
            if (connection == null)
            {
                lock (createLock)
                {
                    if (connection == null)
                    {
                        connection = new SqlConnection(appConfig.DbConnectionString);
                        connection.Open();
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
