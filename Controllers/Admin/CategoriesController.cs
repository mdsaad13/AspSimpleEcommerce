using OnlineShopping.DbUtil;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers.Admin
{
    [Helpers.AdminAuthorized]
    [RoutePrefix("admin/products/categories")]
    public class CategoriesController : Controller
    {
        readonly CategoriesUtil Util = new CategoriesUtil();
        readonly string ControllerFor = "Category ";
        readonly string IndexUrl = "/admin/products/categories";

        [Route]
        public ActionResult Index()
        {
            return View(Util.List(false));
        }

        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            return View(Util.GetByID(id, false));
        }

        [Route("create")]
        public ActionResult Create()
        {
            ViewBag.Title = "Add New " + ControllerFor;
            return View("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(Categories categories)
        {
            if (Util.Insert(categories))
            {
                Session["Flash_Success"] = ControllerFor + "added successfully!";
            }
            else
            {
                Session["Flash_Error"] = ControllerFor + "add failed!<br>Please try again later";
            }
            return Redirect(IndexUrl);
        }

        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            Categories obj = Util.GetByID(id, false);

            ViewBag.Title = "Edit product - " + obj.Name;
            return View("_Form", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(int id, Categories categories)
        {
            if (Util.Update(categories))
            {
                Session["Flash_Success"] = ControllerFor + "updated successfully!";
            }
            else
            {
                Session["Flash_Error"] = ControllerFor + "update failed!<br>Please try again later";
            }
            return Redirect(IndexUrl);
        }

        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (Util.Delete(id))
            {
                Session["Flash_Success"] = ControllerFor + "deleted successfully!";
            }
            else
            {
                Session["Flash_Error"] = ControllerFor + "deleted failed!<br>Please try again later";
            }
            return Redirect(IndexUrl);
        }

    }
}