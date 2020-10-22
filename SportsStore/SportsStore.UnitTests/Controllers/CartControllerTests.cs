using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Databases.EntityFramework.Entities;
using SportsStore.Domain.Databases.EntityFramework.Repositories;
using SportsStore.Domain.Models;
using SportsStore.Domain.Processors;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void AddToCart_CanAddToCart_And_SuccessfullyRedirectsToIndexWithReturningExpectedUrl()
        {
            // Arrange
            var expectedReturnUrl = "expected/url";
            var product = new Product { Id = 1, Name = "P1", Category = "Apples" };

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{ product });
            mock.Setup(m => m.GetProductById(1)).Returns(product);

            var cart = new Cart();

            var controller = new CartController(mock.Object, null);

            // Act
            var actualResult = controller.AddToCart(cart, 1, expectedReturnUrl);

            // Assert
            Assert.IsTrue(
                cart.GetCartLinesCount() == 1 &&
                cart.GetCartLines().ElementAt(0).Product.Id == product.Id &&
                (string)actualResult.RouteValues["action"] == "Index" &&
                (string)actualResult.RouteValues["returnUrl"] == expectedReturnUrl
            );
        }

        [TestMethod]
        public void Index_Invoke_CanViewCartContents()
        {
            // Arrange
            var expectedUrl = "RETURN_URL";

            var cart = new Cart();
            var controller = new CartController(null, null);

            // Act
            var actualResult = controller.Index(cart, expectedUrl).ViewData.Model as CartIndexViewModel;

            // Assert
            Assert.IsTrue(actualResult.Cart == cart && actualResult.ReturnUrl == expectedUrl);
        }

        [TestMethod]
        public async Task Checkout_InvokeHttpPostVersion_CannotCompleteOrderWithEmptyCart()
        {
            // Arrange
            var expectedViewName = string.Empty;
            var expectedModelStateVerification = false;

            var cart = new Cart();
            var shippingDetails = new ShippingDetails();

            var mock = new Mock<IOrderProcessor>();
            var controller = new CartController(null, mock.Object);

            // Act
            var actualViewResult = await controller.Checkout(cart, shippingDetails);

            // Assert
            Assert.IsTrue(
                expectedViewName == actualViewResult.ViewName &&
                expectedModelStateVerification == actualViewResult.ViewData.ModelState.IsValid
            );

            mock.Verify(op => op.ProcessOrderAsync(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
        }

        [TestMethod]
        public async Task Checkout_InvokeHttpPostVersion_CannotCompleteOrderWithInvalidShippingDetails()
        {
            // Arrange
            var expectedViewName = string.Empty;
            var expectedModelStateVerification = false;

            var mock = new Mock<IOrderProcessor>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var shippingDetails = new ShippingDetails();

            var controller = new CartController(null, mock.Object);
            controller.ModelState.AddModelError("Error coming from HttpGet version of the method [Checkout]", "Error related to filling out the shipping details");

            // Act
            var actualViewResult = await controller.Checkout(cart, shippingDetails);

            // Assert
            Assert.IsTrue(
                expectedViewName == actualViewResult.ViewName &&
                expectedModelStateVerification == actualViewResult.ViewData.ModelState.IsValid
            );

            mock.Verify(op => op.ProcessOrderAsync(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
        }

        [TestMethod]
        public async Task Checkout_InvokeHttpPostVersion_CanCompleteOrder()
        {
            // Arrange
            var expectedViewName = "OrderCompleted";
            var expectedModelStateVerification = true;

            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var shippingDetails = new ShippingDetails();

            var mock = new Mock<IOrderProcessor>();
            var controller = new CartController(null, mock.Object);

            // Act
            var actualViewResult = await controller.Checkout(cart, shippingDetails);

            // Assert
            Assert.IsTrue(
                expectedViewName == actualViewResult.ViewName &&
                expectedModelStateVerification == actualViewResult.ViewData.ModelState.IsValid
            );

            mock.Verify(op => op.ProcessOrderAsync(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
        }
    }
}
