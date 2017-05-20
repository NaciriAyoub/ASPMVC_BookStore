using BookStore.Domain.Abstract;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private IBookRepository repository;
        public int pageSize = 4;
        public BookController(IBookRepository bookRep)
        {
            repository = bookRep;
        }

        public ViewResult List(string specilization, int pageN=1)
        {
            BookListViewModel model = new BookListViewModel() {
                Books = repository.Books
                       .Where(b => specilization ==null || b.Specialization == specilization)
                       .OrderBy(b => b.ISBN)
                       .Skip((pageN - 1) * pageSize)
                       .Take(pageSize),
                PagingInfo = new PagingInfo() { CurrentPage = pageN, ItemsPerPage = pageSize, TotalItem =specilization == null ? repository.Books.Count(): repository.Books.Where(b => b.Specialization == specilization).Count() },
                CurrentSpecilization = specilization
            };

            return View(model);
        }
        public ViewResult ListAll()
        {
            return View(repository.Books);
        }

    }
}