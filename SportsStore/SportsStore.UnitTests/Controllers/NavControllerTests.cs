using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Databases.EntityFramework.Entities;
using SportsStore.Domain.Databases.EntityFramework.Repositories;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class NavControllerTests
    {
        [TestMethod]
        public void Menu_Invoke_CanCreateCategories()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1", Category = "Apples" },
                new Product { Id = 2, Name = "P2", Category = "Apples" },
                new Product { Id = 3, Name = "P3", Category = "Plums" },
                new Product { Id = 4, Name = "P4", Category = "Oranges" },
            });

            var controller = new NavController(mock.Object);

            // Act
            var actualResult = controller.Menu().Model as IOrderedEnumerable<string>;

            // Assert
            Assert.IsTrue(
                actualResult.Count() == 3 &&
                actualResult.ElementAt(0) == "Apples" &&
                actualResult.ElementAt(1) == "Oranges" &&
                actualResult.ElementAt(2) == "Plums"
            );
        }

        [TestMethod]
        public void Menu_Invoke_IndicatesSelectedCategory()
        {
            // Arrange
            var expectedCategory = "Apples";
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { Id = 1, Name = "P1", Category = "Apples" },
                new Product { Id = 4, Name = "P2", Category = "Oranges" }
            });

            var controller = new NavController(mock.Object);

            // Act
            var actualCategory = controller.Menu(expectedCategory).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(expectedCategory, actualCategory);
        }
    }
}
