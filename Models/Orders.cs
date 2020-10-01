using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class Orders
    {
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int ProdID { get; set; }

        public int Status { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }

        public Products Product { get; set; }

        public Users User { get; set; }
    }
}