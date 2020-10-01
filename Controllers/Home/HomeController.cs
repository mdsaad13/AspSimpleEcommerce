using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OnlineShopping.DbUtil;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers.Home
{
    public class HomeController : Controller
    {
        readonly ProductsUtil productsUtil = new ProductsUtil();
        readonly CategoriesUtil categoriesUtil = new CategoriesUtil();

        public HomeController()
        {
            ViewBag.AllCategories = categoriesUtil.List();
        }

        public ActionResult Index()
        {
            SliderUtil sliderUtil = new SliderUtil();
            var Data = new Tuple<
                List<Slider>, 
                List<Products>,
                List<Categories>>
                (
                sliderUtil.List(),
                productsUtil.ListByFeatured(),
                categoriesUtil.ListWithProducts()
                );

            return View(Data);
        }

        [Route("products")]
        public ActionResult Products()
        {
            return View(productsUtil.List());
        }

        [Route("products/{id}")]
        public ActionResult ViewProducts(int ID)
        {
            return View(productsUtil.GetByID(ID));
        }

        [Route("categories")]
        public ActionResult Categories()
        {
            return View(categoriesUtil.List());
        }
        
        [Route("categories/{id}")]
        public ActionResult ViewCategory(int ID)
        {
            return View(productsUtil.ListByCat(ID));
        }

    }
}