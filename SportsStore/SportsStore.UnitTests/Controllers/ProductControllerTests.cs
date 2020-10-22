using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Databases.EntityFramework.Entities;
using SportsStore.Domain.Databases.EntityFramework.Repositories;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void List_Invoke_CanPaginate()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
                new Product { Id = 4, Name = "P4" },
                new Product { Id = 5, Name = "P5" },
            });

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            // Act
            var result = ((ProductsListViewModel)controller.List(null, 2).Model).Products;

            // Assert
            Assert.IsTrue(
                result.Count() == 2 &&
                result.ElementAt(0).Name == "P4" &&
                result.ElementAt(1).Name == "P5"
            );
        }

        [TestMethod]
        public void List_Invoke_CanSendPaginationViewModel()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" },
                new Product { Id = 4, Name = "P4" },
                new Product { Id = 5, Name = "P5" },
            });

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            // Act
            var actualResult = (controller.List(null, 2).Model as ProductsListViewModel).PagingInfo;

            // Assert
            Assert.IsTrue(
                actualResult.CurrentPage == 2 &
                actualResult.ItemsPerPage == 3 &&
                actualResult.TotalItems == 5 &&
                actualResult.TotalPages == 2
            );
        }

        [TestMethod]
        public void List_Invoke_CanFilterProducts()
        {
            // Arrange
            var expectedCategory = "Cat2";

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1", Category = "Cat1" },
                new Product { Id = 2, Name = "P2", Category = expectedCategory },
                new Product { Id = 3, Name = "P3", Category = "Cat1" },
                new Product { Id = 4, Name = "P4", Category = expectedCategory },
                new Product { Id = 5, Name = "P5", Category = "Cat3" },
            });

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            // Act
            var actualResult = ((ProductsListViewModel)controller.List(expectedCategory, 1).Model).Products.ToArray();

            // Assert
            Assert.IsTrue(
                actualResult.Length == 2 &
                actualResult[0].Name == "P2" && actualResult[0].Category == expectedCategory &&
                actualResult[1].Name == "P4" && actualResult[1].Category == expectedCategory
            );
        }

        [TestMethod]
        public void List_Invoke_GenerateCategorySpecificProductCount()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1", Category = "Cat1" },
                new Product { Id = 2, Name = "P2", Category = "Cat2" },
                new Product { Id = 3, Name = "P3", Category = "Cat1" },
                new Product { Id = 4, Name = "P4", Category = "Cat2" },
                new Product { Id = 5, Name = "P5", Category = "Cat3" },
            });

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            // Act
            var res1 = (controller.List("Cat1").Model as ProductsListViewModel).PagingInfo.TotalItems;
            var res2 = (controller.List("Cat2").Model as ProductsListViewModel).PagingInfo.TotalItems;
            var res3 = (controller.List("Cat3").Model as ProductsListViewModel).PagingInfo.TotalItems;
            var resAll = (controller.List(null).Model as ProductsListViewModel).PagingInfo.TotalItems;

            // Assert
            Assert.IsTrue(
                res1 == 2 &&
                res2 == 2 &&
                res3 == 1 &&
                resAll == 5
            );
        }

        [TestMethod]
        public void GetImage_Invoke_CanRetrieveImageData()
        {
            // Arrange
            var p = new Product
            {
                Id = 2,
                Name = "Test",
                ImageData = new byte[] { 1, 0, 1, 0, 1, 0, 1 },
                ImageMimeType = "image/png"
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1" },
                p,
                new Product { Id = 3, Name = "P3" }
            });
            mock.Setup(x => x.GetProductById(p.Id)).Returns(p).Verifiable();

            var controller = new ProductController(mock.Object);

            // Act
            var result = controller.GetImage(p.Id);

            // Assert
            mock.Verify();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(p.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void GetImage_Invoke_CannotRetrieveImageDataForInvalidId()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[] { new Product { Id = 377 } });

            var controller = new ProductController(mock.Object);

            // Act
            var result = controller.GetImage(733);

            // Assert
            Assert.IsNull(result);
        }
    }
}
