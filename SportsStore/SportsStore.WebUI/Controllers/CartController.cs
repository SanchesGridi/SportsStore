using System.Threading.Tasks;
using System.Web.Mvc;
using SportsStore.Domain.Controllers;
using SportsStore.Domain.Databases.EntityFramework.Repositories;
using SportsStore.Domain.Models;
using SportsStore.Domain.Processors;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : ProductRepositoryController
    {
        private readonly IOrderProcessor _orderProcessor;

        public CartController(IProductRepository productRepository, IOrderProcessor orderProcessor) : base(productRepository)
        {
            _orderProcessor = orderProcessor;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return base.View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            var product = _productRepository.GetProductById(productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return base.RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            var product = _productRepository.GetProductById(productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return base.RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemovePositionFromCartLine(Cart cart, int productId, string returnUrl)
        {
            var product = _productRepository.GetProductById(productId);

            if (product != null)
            {
                cart.RemovePositionFromCartLine(product.Id);
            }

            return base.RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return base.PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return base.View(new ShippingDetails());
        }

        [HttpPost]
        public async Task<ViewResult> Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.GetCartLinesCount() == 0)
            {
                ModelState.AddModelError("OrderCompletionError", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                await _orderProcessor.ProcessOrderAsync(cart, shippingDetails);

                cart.Clear();
                 
                return base.View("OrderCompleted");
            }
            else
            {
                return base.View(shippingDetails);
            }
        }
    }
}