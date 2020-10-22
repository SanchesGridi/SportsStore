using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Controllers;
using SportsStore.Domain.Databases.EntityFramework.Repositories;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : ProductRepositoryController
    {
        public int PageSize { get; set; } = 4;

        public ProductController(IProductRepository productRepository) : base(productRepository)
        {
        }

        public ViewResult List(string category, int page = 1)
        {
            var model = new ProductsListViewModel
            {
                Products = _productRepository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        _productRepository.Products.Count() :
                        _productRepository.Products.Where(p => p.Category == category).Count()
                },

                CurrentCategory = category
            };

            return base.View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            var product = _productRepository.GetProductById(productId);

            if (product != null)
            {
                return base.File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
