using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests.Helpers
{
    [TestClass]
    public class HtmlHelpersTests
    {
        [TestMethod]
        public void GeneratePageLinks_Invoke_CanGeneratePageLinks()
        {
            // Arrange
            var helper = null as HtmlHelper;
            var info = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            var actualResult = helper.GeneratePageLinks(info, pageUrlDelegate);

            // Assert
            Assert.AreEqual(
                @"<a class=""btn btn-default"" href=""Page1"">1</a>" +
                @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" +
                @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                actualResult.ToString()
            );
        }
    }
}
