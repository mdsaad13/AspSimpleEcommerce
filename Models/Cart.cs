using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Cart
    {
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int ProdID { get; set; }

        public Products Product { get; set; }
    }
}