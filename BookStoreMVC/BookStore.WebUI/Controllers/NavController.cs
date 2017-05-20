using BookStore.Domain.Abstract;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IBookRepository repository;

        public NavController(IBookRepository bookRep)
        {
            repository = bookRep;
        }
        public PartialViewResult Menu(string specilization = null)
        {
            ViewBag.SelectedSpec = specilization;
            IEnumerable<string> Books = repository.Books
                       .Select(b => b.Specialization)
                       .Distinct();
            //string viewName = mobileLayout ? "MenuHorizontal":"Menu";
            //return PartialView(viewName, Books);
            return PartialView(Books);
        }
    }
}