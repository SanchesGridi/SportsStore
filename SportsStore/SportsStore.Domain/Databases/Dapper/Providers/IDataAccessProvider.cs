using Dapper.Abstractions;

namespace SportsStore.Domain.Databases.Dapper.Providers
{
    public interface IDataAccessProvider
    {
        void SetConnectionForExecutors(string connectionString);
        IDbExecutor CreateDataAccessor();
        int ExecuteNonQueryServerCommand(string fileScript);
    }
}
