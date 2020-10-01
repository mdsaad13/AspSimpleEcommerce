using OnlineShopping.DbUtil;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers.Admin
{
    [Helpers.AdminAuthorized]
    [RoutePrefix("admin/products")]
    public class ProductsController : Controller
    {
        readonly ProductsUtil Util = new ProductsUtil();
        readonly string ControllerFor = "Products ";
        readonly string IndexUrl = "/admin/products";
        readonly string Files_Dir = "/Images/Products/";

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
            CategoriesUtil categoriesUtil = new CategoriesUtil();
            ViewBag.Categories = new SelectList(categoriesUtil.List(), "id", "name");
            ViewBag.Title = "Add New "+ ControllerFor;
            return View("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(Products products)
        {
            try
            {
                if (products.ImgFile != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + products.ImgFile.FileName;
                    string path = Server.MapPath(Files_Dir);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    products.ImgFile.SaveAs(path + uniqueFileName);
                    products.ImageUrl = uniqueFileName;
                }
            }
            catch(Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }

            if (Util.Insert(products))
            {
                Session["Flash_Success"] = ControllerFor+"added successfully!";
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
            ViewBag.EditMode = true;
            CategoriesUtil categoriesUtil = new CategoriesUtil();
            ViewBag.Categories = new SelectList(categoriesUtil.List(), "id", "name");

            Products products = Util.GetByID(id, false);

            ViewBag.Title = "Edit product - " + products.Title;
            return View("_Form", products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(int id, Products products)
        {
            try
            {
                if (products.ImgFile != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + products.ImgFile.FileName;
                    string path = Server.MapPath(Files_Dir);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    products.ImgFile.SaveAs(path + uniqueFileName);
                    products.ImageUrl = uniqueFileName;
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }

            if (Util.Update(products))
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
