using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IBookRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IBookRepository bookRep, IOrderProcessor orderProcessorParam)
        {
            repository = bookRep;
            orderProcessor = orderProcessorParam;
        }

        public ViewResult Index(Cart cart, string returnU)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnU = returnU });
        }

        public RedirectToRouteResult AddToCart( Cart cart, int isbn, string returnU)
        {
            Book book = repository.Books.FirstOrDefault(b=>b.ISBN==isbn);
            if (book != null)
            {
                cart.AddItem(book);
            }
            return RedirectToAction("Index", new { returnU }); // index
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int isbn, string returnU)
        {
            Book book = repository.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                cart.RemoveItem(book);
            }
            return RedirectToAction("Index", new { returnU } ); // index
        }        

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult CheckOut()
        {
            return View(new ShippingDetails());
        }



        [HttpPost]
        public ViewResult CheckOut(Cart cart, ShippingDetails shippingD)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty !!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingD);
                cart.ClearAll();
                return View("Completed");
            }
            else
            {
                return View(shippingD);
            }
           
        }

    }
}