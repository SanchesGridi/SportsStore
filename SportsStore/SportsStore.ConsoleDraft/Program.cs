using System;
using System.IO;
using System.Threading.Tasks;
using SportsStore.Domain.Databases.Dapper;
using SportsStore.Domain.Databases.Dapper.Providers;

namespace SportsStore.ConsoleDraft
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() =>
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "SqlCommand.sql");
                var initScript = new FileInfo(fileName).OpenText().ReadToEnd();

                var provider = new DataAccessProvider(new ConnectionStringSettings());
                provider.SetConnectionForExecutors(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SportsStore;Integrated Security=True");
                provider.ExecuteNonQueryServerCommand(initScript);
            });

            Console.Read();
        }
    }
}
