using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class Slider
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.pdf|.docx|.txt)$", ErrorMessage = "Only Document or Image files allowed.")]
        public HttpPostedFileBase ImgFile { get; set; }

        public string Image { get; set; }

        public string Redirect { get; set; }
    }
}