using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.WebUI.Controllers;
using BookStore.WebUI.HtmlHelper;
using BookStore.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace BookStrore.UnitTest
{
    [TestClass]
    public class BookCatalogTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrage
            Mock<IBookRepository> mock= new Mock<IBookRepository>();
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
            BookController controller = new BookController(mock.Object);
            controller.pageSize = 4;
            //Act
           // IEnumerable<Book> result = (IEnumerable<Book>)controller.List(2).Model;
            BookListViewModel result = (BookListViewModel)controller.List(null,2).Model;
            //Assert
            Book[] bookArray = result.Books.ToArray();
            Assert.IsTrue(bookArray.Length == 4);
            Assert.AreEqual(bookArray[0].title, "5 SQL SERVER");
            Assert.AreEqual(bookArray[1].title, "6 ASP.net");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrage
            HtmlHelper myHelper = null;
            PagingInfo myPageInfo = new PagingInfo() { ItemsPerPage = 5, CurrentPage = 2, TotalItem =30 };
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            string result1 = @"<a class=""btn btn-default"" href=""Page1"">1</a><a class=""btn btn-default btn-Primary Selected"" href=""Page2"">2</a><a class=""btn btn-default"" href=""Page3"">3</a><a class=""btn btn-default"" href=""Page4"">4</a><a class=""btn btn-default"" href=""Page5"">5</a><a class=""btn btn-default"" href=""Page6"">6</a>";
            //Act
            string resultMethode = myHelper.PageLinksHelper(myPageInfo, pageUrlDelegate).ToString();
            //Asset
            Assert.AreEqual(result1, resultMethode);
        }

        [TestMethod]
        public void Can_Send_pagination_View_Model()
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
            });
            BookController controller = new BookController(mock.Object);
            controller.pageSize = 4;
            //Act
            BookListViewModel result = (BookListViewModel)controller.List(null,2).Model;
            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage,2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 4);
            Assert.AreEqual(pageInfo.TotalItem, 12);
            Assert.AreEqual(pageInfo.TotalPages, 3);
        }

        [TestMethod]
        public void Can_Filter_Books()
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
            });
            BookController controller = new BookController(mock.Object);
            controller.pageSize = 4;
            //Act
            BookListViewModel result = (BookListViewModel)controller.List("OS",1).Model;
            Book[] Books = result.Books.ToArray();
            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 1);
            Assert.AreEqual(pageInfo.ItemsPerPage, 4);
            Assert.AreEqual(pageInfo.TotalItem, 6);
            Assert.AreEqual(pageInfo.TotalPages, 2);

            Assert.AreEqual(Books.Length, 4);
            Assert.IsTrue(Books[0].title == "3 Web Server");

        }

        [TestMethod]
        public void Can_Filter_Specilization()
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
            });
            NavController controller = new NavController(mock.Object);
            //Act
            PartialViewResult result = controller.Menu();
            string[] Books = ((IEnumerable<string>)result.Model).ToArray();
            //Assert
            Assert.AreEqual(Books.Length, 3);
            Assert.IsTrue(Books[0] == "DATABASE");

        }

        [TestMethod]
        public void Can_Indicates_Selected_Specilization()
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
            });
            NavController controllerNAv = new NavController(mock.Object);
            //Act
            PartialViewResult result = controllerNAv.Menu("DATABASE");
            string SlectedSpecMenu = result.ViewData["SelectedSpec"].ToString();
            //Assert
            Assert.AreEqual(SlectedSpecMenu, "DATABASE");
        }
    }
}
