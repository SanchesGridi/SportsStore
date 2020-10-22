using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Steps:
            //
            // 1) change db name in file DatabaseInitScript to "SportsStore"
            //
            // 2) add to webconfig: RecipientAddress, SenderAddress, SenderPassword
            //
            // 3) test this app 
            //

            //routes.MapRoute(
            //    name: "No name route",
            //    url: "",
            //    defaults: new { controller = "Product", action = "List", category = (string)null, page = 1 }
            //);

            routes.MapRoute(
                name: "Init db route",
                url: "",
                defaults: new { controller = "InitDb", action = "Index" }
            );

            routes.MapRoute(
                name: "Page num route",
                url: "Page{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Category route",
                url: "{category}",
                defaults: new { controller = "Product", action = "List", page = 1 }
            );

            routes.MapRoute(
                name: "Category with page route",
                url: "{category}/Page{page}",
                defaults: new { controller = "Product", action = "List" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Default route",
                url: "{controller}/{action}"
            );
        }
    }
}
