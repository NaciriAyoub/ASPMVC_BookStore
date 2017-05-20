using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Book", action = "List", specilization = (string)null, pageN = 1 }
            );

            routes.MapRoute(
                name: null,
                url: "BookListPage{pageN}",
                defaults: new { controller = "Book", action = "List", specilization = (string)null}
            );

            routes.MapRoute(
                name: null,
                url: "{specilization}",
                defaults: new { controller = "Book", action = "List", pageN = 1 }
            );

            routes.MapRoute(
               name: null,
               url: "{specilization}/Page{pageN}",
               defaults: new { controller = "Book", action = "List"}, 
               constraints: new {pageN= @"\d+"}
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
