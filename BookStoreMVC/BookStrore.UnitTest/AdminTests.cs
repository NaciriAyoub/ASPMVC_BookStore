using System;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.WebUI.Controllers;
using BookStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStrore.UnitTest
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //Arrange
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
            });
            AdminController target = new AdminController(mock.Object);
            
            //Act
            //Book[] result = ((IEnumerable<Book>)target.Index()).ToArray();
            Book[] result = ((IEnumerable<Book>)target.Index().Model).ToArray();

            //Asset
            Assert.AreEqual(result.Length, 12);
            Assert.AreEqual(result[0].title, "1 SQL SERVER");
            Assert.AreEqual(result[11].title, "A3 Linux OS");
        }

        [TestMethod]
        public void Can_Edit_Book()
        {
            //Arrange
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
            });
            AdminController target = new AdminController(mock.Object);

            //Act
            //Bad Test
            Book bookNotFound = target.Edit(2003).Model as Book;
            //Good Test
            Book book1 = (Book)target.Edit(121).Model;
            Book book2 = (Book)target.Edit(126).Model;
            Book book3 = (Book)target.Edit(132).Model;


            //Asset
            //Bad Test
            Assert.IsNull(bookNotFound);
            //Good Test
            Assert.AreEqual(book1.ISBN, 121);
            Assert.AreEqual(book2.title, "6 ASP.net");
            Assert.AreEqual(book3.ISBN, 132);
           
        }


        [TestMethod]
        public void Can_Update_Book()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
            {
              new Book { title = "1 SQL SERVER", ISBN = 121, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "2 ASP.net", ISBN = 122, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "3 Web Server", ISBN = 123, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "4 Linux OS", ISBN = 124, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" }
            });
            AdminController target = new AdminController(mock.Object);
            Book book = new Book { title = "Updated title", ISBN = 132, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" };
            
            //Act
            ActionResult result = target.Edit(book);

            //Asset
            mock.Verify(b=> b.SaveBook(book));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));

        }


        [TestMethod]
        public void Can_Not_Update_Book()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]
            {
              new Book { title = "1 SQL SERVER", ISBN = 121, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" },
              new Book { title = "2 ASP.net", ISBN = 122, Price = 59.99M, Description = "Learn ASP.net in 30 days", Specialization = "WebForms" },
              new Book { title = "3 Web Server", ISBN = 123, Price = 20M, Description = "Learn Ubuntu", Specialization = "OS" },
              new Book { title = "4 Linux OS", ISBN = 124, Price = 159.99M, Description = "Learn Linux", Specialization = "OS" }
            });
            AdminController target = new AdminController(mock.Object);
            Book book = new Book { title = "Updated title", ISBN = 132, Price = 130.23M, Description = "Learn SQL SERVER in 60 days", Specialization = "DATABASE" };
            target.ModelState.AddModelError("Error", "Error");
            //Act
            ActionResult result = target.Edit(book);

            //Asset
            mock.Verify(b => b.SaveBook(It.IsAny<Book>()), Times.Never);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

    }
}
