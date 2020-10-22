namespace SportsStore.Domain.Databases.Dapper
{
    public interface IConnectionStringSettings
    {
        void SetConnectionString(string connectionString);
        string GetConnectionString();
    }
}
