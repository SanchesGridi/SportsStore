using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Controllers;
using SportsStore.Domain.Databases.EntityFramework.Entities;
using SportsStore.Domain.Databases.EntityFramework.Repositories;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : ProductRepositoryController
    {
        public AdminController(IProductRepository productRepository) : base(productRepository)
        {
        }

        public ViewResult Index()
        {
            return base.View(_productRepository.Products);
        }

        public ViewResult Edit(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            return base.View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                await _productRepository.SaveProductAsync(product);

                TempData["ActionMessage"] = $"{product.Name} has been saved";

                return base.RedirectToAction("Index");
            }
            else
            {
                return base.View(product);
            }
        }

        public ViewResult Create()
        {
            return base.View("Edit", new Product());
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int productId)
        {
            var deletedProduct = await _productRepository.DeleteProductAsync(productId);

            if (deletedProduct != null)
            {
                TempData["ActionMessage"] = $"{deletedProduct.Name} was deleted";
            }

            return base.RedirectToAction("Index");
        }
    }
}