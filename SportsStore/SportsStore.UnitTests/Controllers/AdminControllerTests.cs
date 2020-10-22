using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Databases.EntityFramework.Entities;
using SportsStore.Domain.Databases.EntityFramework.Repositories;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Index_Invoke_ContainsAllProducts()
        {
            // Arrange
            var expectedProducts = new Product[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" }
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(expectedProducts);

            var controller = new AdminController(mock.Object);

            // Act
            var result = controller.Index().ViewData.Model as IEnumerable<Product>;


            // Assert
            CollectionAssert.AreEqual(expectedProducts.Select(x => x.Name).ToArray(), result.Select(x => x.Name).ToArray());
        }

        [TestMethod]
        public void Edit_Invoke_CanGetTheRequiredProduct()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" }
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(expectedProducts);
            mock.Setup(x => x.GetProductById(1)).Returns(expectedProducts.FirstOrDefault(p => p.Id == 1));
            mock.Setup(x => x.GetProductById(2)).Returns(expectedProducts.FirstOrDefault(p => p.Id == 2));
            mock.Setup(x => x.GetProductById(3)).Returns(expectedProducts.FirstOrDefault(p => p.Id == 3));

            var controller = new AdminController(mock.Object);
            var actualProducts = new List<Product>();

            // Act
            foreach (var id in expectedProducts.Select(x => x.Id))
            {
                actualProducts.Add(controller.Edit(id).ViewData.Model as Product);
            }

            // Assert
            CollectionAssert.AreEqual(expectedProducts, actualProducts);
        }

        [TestMethod]
        public void Edit_Invoke_CannotEditNotExistingProduct()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[] { new Product { Id = 999 } });

            var controller = new AdminController(mock.Object);

            // Act
            var actualResult = controller.Edit(666).ViewData.Model as Product;

            // Assert
            Assert.IsNull(actualResult);
        }

        [TestMethod]
        public async Task Edit_InvokeHttpPostVersion_CanSaveValidChanges()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            var controller = new AdminController(mock.Object);
            var product = new Product { Name = "Test" };

            // Act
            var result = await controller.Edit(product);

            // Assert
            mock.Verify(x => x.SaveProductAsync(product), Times.Once());
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_InvokeHttpPostVersion_CannotSaveInvalidChanges()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            var controller = new AdminController(mock.Object);
            var product = new Product { Name = "Test" };
            controller.ModelState.AddModelError("SomeError", "Model Get Error");

            // Act
            var result = await controller.Edit(product);

            // Assert
            mock.Verify(x => x.SaveProductAsync(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Delete_Invoke_CanDeleteValidProducts()
        {
            // Arrange
            var p = new Product { Id = 2, Name = "P2" };
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1" },
                p,
                new Product { Id = 3, Name = "P3" }
            });
            var controller = new AdminController(mock.Object);

            // Act
            await controller.Delete(p.Id);

            // Assert
            mock.Verify(x => x.DeleteProductAsync(p.Id), Times.Once());
        }
    }
}
