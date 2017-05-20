using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItem { set; get; }
        public int ItemsPerPage { set; get; }
        public int CurrentPage { set; get; }


        public decimal TotalPages
        {
            get {
                   decimal x = (decimal)TotalItem / ItemsPerPage;
                   return Math.Ceiling(x);  
                }

        }

    }
}