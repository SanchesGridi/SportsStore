using System;
using System.Data.SqlClient;
using Dapper.Abstractions;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SportsStore.Domain.Databases.Dapper.Providers
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly IConnectionStringSettings _connectionStringSettings;

        public DataAccessProvider(IConnectionStringSettings connectionStringSettings)
        {
            _connectionStringSettings = connectionStringSettings;
        }

        public void SetConnectionForExecutors(string connectionString)
        {
            _connectionStringSettings.SetConnectionString(connectionString);
        }
        public IDbExecutor CreateDataAccessor()
        {
            var connection = this.VerifyAndGetConnection();
            connection.Open();

            var executor = new SqlExecutor(connection);
            return executor;
        }
        public int ExecuteNonQueryServerCommand(string fileScript)
        {
            if (string.IsNullOrWhiteSpace(fileScript))
            {
                throw new ApplicationException("DataAccessProviderError: File script equals null!");
            }

            var connection = this.VerifyAndGetConnection();
            var server = new Server(new ServerConnection(connection));
            var affectedLines = server.ConnectionContext.ExecuteNonQuery(fileScript);

            return affectedLines;
        }

        private SqlConnection VerifyAndGetConnection()
        {
            var connectionString = _connectionStringSettings.GetConnectionString();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ApplicationException("DataAccessProviderError: Set connection string before usage!");
            }

            return new SqlConnection(connectionString);
        }
    }
}
