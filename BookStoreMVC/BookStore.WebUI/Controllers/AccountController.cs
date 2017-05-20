using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.WebUI.Infrastructure.Abstract;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider AuthProvider;
       
        public AccountController(IAuthProvider providerParam)
        {
            AuthProvider = providerParam;
        }
       
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(AuthProvider.Anthenticate(model.Username, model.Password))
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                else
                {
                    ModelState.AddModelError("", "Incorrect Username/Password");
                    return View();
                }
            }
            else
            {

            }

            return View();
        }

    }
}