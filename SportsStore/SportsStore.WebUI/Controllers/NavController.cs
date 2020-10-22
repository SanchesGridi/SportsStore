using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Controllers;
using SportsStore.Domain.Databases.EntityFramework.Repositories;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : ProductRepositoryController
    {
        public NavController(IProductRepository productRepository) : base(productRepository)
        {
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            var categories = _productRepository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x);

            return base.PartialView("FlexMenu", categories);
        }
    }
}