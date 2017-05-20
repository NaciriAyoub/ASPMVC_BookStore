using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter the first address")]
        public string Line1 { get; set; }


        public string Line2 { get; set; }

        [Required(ErrorMessage = "Please enter the city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the state name")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter the country name")]
        public string Country { get; set; }


        public bool GiftWrap { get; set; }
    }
}
