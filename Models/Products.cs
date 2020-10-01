using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class Products
    {
        public int ID { get; set; }

        [Required]
        public int CatID { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.pdf|.docx|.txt)$", ErrorMessage = "Only Document or Image files allowed.")]
        public HttpPostedFileBase ImgFile { get; set; }

        public string ImageUrl { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        [Required]
        public bool IsFeatured { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }
        
        public string CategoryName { get; set; }

    }
}