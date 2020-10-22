using System.Configuration;
using System.IO;
using System.Web.Mvc;
using SportsStore.Domain.Databases.Dapper.Providers;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class InitDbController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public InitDbController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        public ActionResult Index()
        {
            return base.View(new InitDbModel());
        }

        [HttpPost]
        public ActionResult Index(bool initialize)
        {
            if (initialize)
            {
                this.InitializeNewDatabase();
            }

            return base.RedirectToAction("List", "Product", new { category = (string)null, page = 1 });
        }

        private void InitializeNewDatabase()
        {
            var init = bool.Parse(ConfigurationManager.AppSettings["Db.Initialize"]);

            if (init)
            {
                var fileName = Path.Combine(Server.MapPath("~"), "Scripts\\Sql\\DatabaseInitScript.sql");
                var initScript = new FileInfo(fileName).OpenText().ReadToEnd();

                var connectionString = ConfigurationManager.ConnectionStrings["ProductContext"].ConnectionString;
                _dataAccessProvider.SetConnectionForExecutors(connectionString);
                _dataAccessProvider.ExecuteNonQueryServerCommand(initScript);
            }
        }
    }
}