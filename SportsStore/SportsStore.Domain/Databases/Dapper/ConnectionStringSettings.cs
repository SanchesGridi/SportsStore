namespace SportsStore.Domain.Databases.Dapper
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        private string _connectionString;

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString.Clone().ToString();
        }
    }
}
