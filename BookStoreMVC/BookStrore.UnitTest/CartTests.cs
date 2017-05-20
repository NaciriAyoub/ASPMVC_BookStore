using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Domain.Entities;
using System.Linq;
using BookStore.Domain.Abstract;
using Moq;
using BookStore.WebUI.Controllers;
using System.Web.Mvc;
using BookStore.WebUI.Models;

namespace BookStrore.UnitTest
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_add_new_Lines()
        {
            //Arrange
            Book book1 = new Book() { ISBN = 1, title="ASP.net"};
            Book book2 = new Book() { ISBN = 2, title = "C#" };

            //Act
            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 3);
            CartLine[] result = cart.Lines.ToArray();

            //Assert 
            Assert.AreEqual(result[0].Book, book1);
            Assert.AreEqual(result[1].Book, book2);
        }

        [TestMethod]
        public void Can_add_quantity_For_existante_Line()
        {
            //Arrange
            Book book1 = new Book() { ISBN = 1, title = "ASP.net" };
            Book book2 = new Book() { ISBN = 2, title = "C#" };

            //Act
            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 3);
            cart.AddItem(book1, 2);

            CartLine[] result = cart.Lines.OrderBy(cl=> cl.Book.ISBN).ToArray();

            //Assert 
            Assert.AreEqual(result[0].Quantity, 3);
            Assert.AreEqual(result[1].Quantity, 3);
        }

        [TestMethod]
        public void Can_remove_Line()
        {
            //Arrange
            Book book1 = new Book() { ISBN = 1, title = "ASP.net" };
            Book book2 = new Book() { ISBN = 2, title = "C#" };
            Book book3 = new Book() { ISBN = 3, title = "SQL" };
           
            //Act
            Cart cart = new Cart();
            cart.AddItem(book1, 1);
            cart.AddItem(book2, 3);
            cart.AddItem(book3, 5);
            cart.AddItem(book2, 1);

            cart.RemoveItem(book2);
            
            //action1
            CartLine[] result = cart.Lines.OrderBy(cl => cl.Book.ISBN).ToArray();
            //action2
            int Quantity_book2 = cart.Lines.Where(cl => cl.Book == book2).Count();

            //Assert 
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Quantity, 1);
            Assert.AreEqual(result[1].Quantity, 5);
            
        }


        [TestMethod]
        public void Can_Count_Total_Cart()
        {
            //Arrange
            Book book1 = new Book() { ISBN = 1, title = "ASP.net", Price=10.99m };
            Book book2 = new Book() { ISBN = 2, title = "C#", Price = 20.99m };
            Book book3 = new Book() { ISBN = 3, title = "SQL", Price = 5.00m };

            //Act 1
            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book3, 10);

            decimal total_cart = cart.ComputeTotalLine();

            //Assert 
            Assert.AreEqual(total_cart, 81.98m);


            //Act 2
            cart.ClearAll();
            total_cart = cart.ComputeTotalLine();
            //Assert 
            Assert.AreEqual(total_cart, 0);
        }


        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //Arrage
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
            {
              new Book { title = "1 SQL SERVER", ISBN = 121, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "2 ASP.net", ISBN = 122, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "3 Web Server", ISBN = 123, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "4 Linux OS", ISBN = 124, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
              new Book { title = "5 SQL SERVER", ISBN = 125, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "6 ASP.net", ISBN = 126, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "7 Web Server", ISBN = 127, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "8 Linux OS", ISBN = 128, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
              new Book { title = "9 SQL SERVER", ISBN = 129, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "A1 ASP.net", ISBN = 130, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "A2 Web Server", ISBN = 131, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "A3 Linux OS", ISBN = 132, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
            }.AsQueryable());
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, null);

            //Act
            cartController.AddToCart(cart, 121, null);
            cartController.AddToCart(cart, 122, null);
            //RedirectToRouteResult result = cartController.AddToCart(cart, 122, "myUrl");
            //Assert
            Assert.AreEqual(cart.Lines.ToArray()[0].Book.ISBN, 121);
            Assert.AreEqual(cart.Lines.ToArray()[0].Quantity, 1);
        }

        [TestMethod]
        public void Adding_to_cart_goes_to_cart_screen()
        {
            //Arrage
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
            {
              new Book { title = "1 SQL SERVER", ISBN = 121, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "2 ASP.net", ISBN = 122, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "3 Web Server", ISBN = 123, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "4 Linux OS", ISBN = 124, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
              new Book { title = "5 SQL SERVER", ISBN = 125, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "6 ASP.net", ISBN = 126, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "7 Web Server", ISBN = 127, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "8 Linux OS", ISBN = 128, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
              new Book { title = "9 SQL SERVER", ISBN = 129, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "A1 ASP.net", ISBN = 130, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "A2 Web Server", ISBN = 131, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "A3 Linux OS", ISBN = 132, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
            }.AsQueryable());
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, null);
            //Act
            RedirectToRouteResult result = cartController.AddToCart(cart, 122, "myUrl");
            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnU"], "myUrl");
        }

        [TestMethod]
        public void Can_View_cart_content()
        {
            //Arrage
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
            {
              new Book { title = "1 SQL SERVER", ISBN = 121, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "2 ASP.net", ISBN = 122, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "3 Web Server", ISBN = 123, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "4 Linux OS", ISBN = 124, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
              new Book { title = "5 SQL SERVER", ISBN = 125, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "6 ASP.net", ISBN = 126, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "7 Web Server", ISBN = 127, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "8 Linux OS", ISBN = 128, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
              new Book { title = "9 SQL SERVER", ISBN = 129, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "A1 ASP.net", ISBN = 130, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "A2 Web Server", ISBN = 131, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "A3 Linux OS", ISBN = 132, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" },
            }.AsQueryable());
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, null);
            //Act
            CartIndexViewModel result = (CartIndexViewModel)cartController.Index(cart, "myUrl").Model;
            //Assert
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnU, "myUrl");
        }


        [TestMethod]
        public void Cannot_CheckOut_empty_Cart()
        {
            //Arrage
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController cartController = new CartController(null, mock.Object);

            //Act
            ViewResult result = cartController.CheckOut(cart, shippingDetails);

            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(result.ViewData.ModelState.IsValid, false);
        }

        [TestMethod]
        public void Can_CheckOut_Invalid_checkout_details()
        {
            //Arrage
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Book(), 1);

            ShippingDetails shippingDetails = new ShippingDetails();
            CartController cartController = new CartController(null, mock.Object);
            cartController.ModelState.AddModelError("Error", "error");
            //Act
            ViewResult result = cartController.CheckOut(cart, shippingDetails);

            //Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),Times.Never());
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(result.ViewData.ModelState.IsValid, false);
        }

        [TestMethod]
        public void Can_CheckOut_And_Submit()
        {
            //Arrage
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Book(), 1);

            ShippingDetails shippingDetails = new ShippingDetails();
            CartController cartController = new CartController(null, mock.Object);
            //cartController.ModelState.AddModelError("Error", "error");
            
            //Act
            ViewResult result = cartController.CheckOut(cart, shippingDetails);

            //Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            Assert.AreEqual(result.ViewName, "Completed");
            Assert.AreEqual(result.ViewData.ModelState.IsValid, true);
        }

    }
}
