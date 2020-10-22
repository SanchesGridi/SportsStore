using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Authentication;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Login_InvokeHttpPostVersion_CanLoginWithValidCredentials()
        {
            // Arrange
            var model = new LoginViewModel
            {
                UserName = "someName",
                Password = "somePass"
            };
            var expectedReturnUrl = "/URL";

            var mock = new Mock<IAuthProvider>();
            mock.Setup(x => x.Authenticate(model.UserName, model.Password)).Returns(true);

            var controller = new AccountController(mock.Object);

            // Act
            var result = controller.Login(model, expectedReturnUrl);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual(expectedReturnUrl, (result as RedirectResult).Url);
        }

        [TestMethod]
        public void Login_InvokeHttpPostVersion_CannotLoginWithInvalidCredentials()
        {
            // Arrange
            var model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };

            var mock = new Mock<IAuthProvider>();
            mock.Setup(x => x.Authenticate(model.UserName, model.Password)).Returns(false);

            var controller = new AccountController(mock.Object);

            // Act
            var result = controller.Login(model, "/URL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse((result as ViewResult).ViewData.ModelState.IsValid);
        }
    }
}
