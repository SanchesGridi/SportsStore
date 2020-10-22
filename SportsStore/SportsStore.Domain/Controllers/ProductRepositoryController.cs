using System.Web.Mvc;
using SportsStore.Domain.Databases.EntityFramework.Repositories;

namespace SportsStore.Domain.Controllers
{
    public class ProductRepositoryController : Controller
    {
        protected readonly IProductRepository _productRepository;

        public ProductRepositoryController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
