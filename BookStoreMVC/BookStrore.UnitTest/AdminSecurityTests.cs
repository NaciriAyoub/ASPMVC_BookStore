using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.WebUI.Infrastructure.Abstract;
using Moq;
using BookStore.WebUI.Models;
using BookStore.WebUI.Controllers;
using System.Web.Mvc;

namespace BookStrore.UnitTest
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_login_with_Valid_Credentials()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(s => s.Anthenticate("admin", "password")).Returns(true);
            LoginViewModel model = new LoginViewModel() {
                Username = "admin",
                Password="password"
            };
            AccountController target = new AccountController(mock.Object);

            //Act
            ActionResult result = target.Login(model, "/MyUrl");

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Can_login_with_InValid_Credentials()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(s => s.Anthenticate("userX", "passdX")).Returns(false);
            LoginViewModel model = new LoginViewModel()
            {
                Username = "userX",
                Password = "passdX"
            };
            AccountController target = new AccountController(mock.Object);

            //Act
            ActionResult result = target.Login(model, "/MyUrl");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }


    }
}
