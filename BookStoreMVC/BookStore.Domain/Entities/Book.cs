using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        [Key]
        [HiddenInput(DisplayValue= false)]

        public int ISBN { get; set; }
        [Required(ErrorMessage ="Please enter a book title")]
        public string title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a book Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a book Price")]
        [Range(0.01, 999.99,ErrorMessage = "Please enter a Valid Price between $0.01 and $999.99")]
        public decimal Price { get; set; }

        [Required]
        public string Specialization { get; set; }
    }
}
