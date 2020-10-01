using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Categories
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int TotalProducts { get; set; }
        public int ActiveProducts { get; set; }

        public List<Products> Products { get; set; }
    }
}