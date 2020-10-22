using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Databases.EntityFramework.Entities;
using SportsStore.Domain.Models;

namespace SportsStore.UnitTests.Models
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void AddItem_InvokeTwice_CanAddNewLines()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };

            var cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            // Assert
            Assert.IsTrue(
                cart.GetCartLinesCount() == 2 &&
                cart.GetCartLines().ElementAt(0).Product == p1 &&
                cart.GetCartLines().ElementAt(1).Product == p2
            );
        }

        [TestMethod]
        public void AddItem_InvokeThreeTimes_CanAddQuantityForExistingLines()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };

            var cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);

            // Assert
            Assert.IsTrue(
                cart.GetCartLinesCount() == 2 &&
                cart.GetCartLines().ElementAt(0).Quantity == 11 &&
                cart.GetCartLines().ElementAt(1).Quantity == 1
            );
        }

        [TestMethod]
        public void RemoveLine_Invoke_CanRemoveLine()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };
            var p3 = new Product { Id = 3, Name = "P3" };

            var cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);
            cart.AddItem(p2, 1);

            // Act
            cart.RemoveLine(p2);

            // Assert
            Assert.IsTrue(
                cart.GetCartLines().Where(cl => cl.Product == p2).Count() == 0 &&
                cart.GetCartLinesCount() == 2
            );
        }

        [TestMethod]
        public void ComputeTotalValue_Invoke_CalculateCartTotal()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1", Price = 100m };
            var p2 = new Product { Id = 2, Name = "P2", Price = 50m };

            var cart = new Cart();

            cart.AddItem(p1, 4);
            cart.AddItem(p2, 1);

            // Act
            var actualResult = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(450m, actualResult);
        }

        [TestMethod]
        public void Clear_Invoke_CanClearCartContent()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1", Price = 100m };
            var p2 = new Product { Id = 2, Name = "P2", Price = 50m };

            var cart = new Cart();

            cart.AddItem(p1, 4);
            cart.AddItem(p2, 1);

            // Act
            cart.Clear();

            // Assert
            Assert.AreEqual(cart.GetCartLinesCount(), 0);
        }

        [TestMethod]
        public void RemovePositionFromCartLine_Invoke_CanRemoveOnePositionFromCartLine()
        {
            // Arrange
            var product = new Product { Id = 896, Name = "P", Price = 3m };
            
            var cart = new Cart();
            cart.AddItem(product, 111);

            // Act
            cart.RemovePositionFromCartLine(product.Id);

            // Assert
            Assert.IsTrue(
                cart.GetCartLinesCount() == 1 &&
                cart.ComputeTotalValue() == product.Price * 110m
            );
        }

        [TestMethod]
        public void RemovePositionFromCartLine_Invoke_CanRemoveLineFromCartIfQuantityEqualsZero()
        {
            // Arrange
            var product = new Product { Id = 896, Name = "P", Price = 3m };

            var cart = new Cart();
            cart.AddItem(product, 1);

            // Act
            cart.RemovePositionFromCartLine(product.Id);

            // Assert
            Assert.IsTrue(
                0 == cart.GetCartLinesCount() &&
                0m == cart.ComputeTotalValue()
            );
        }
    }
}
