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
    [RoutePrefix("admin/slider")]
    public class SliderController : Controller
    {
        readonly SliderUtil Util = new SliderUtil();
        readonly string ControllerFor = "Slider ";
        readonly string IndexUrl = "/admin/slider";
        readonly string Files_Dir = "/Images/Slider/";

        [Route]
        public ActionResult Index()
        {
            return View(Util.List());
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
        public ActionResult Create(Slider slider)
        {
            try
            {
                if (slider.ImgFile != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + slider.ImgFile.FileName;
                    string path = Server.MapPath(Files_Dir);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    slider.ImgFile.SaveAs(path + uniqueFileName);
                    slider.Image = uniqueFileName;
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }

            if (Util.Insert(slider))
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
            ViewBag.EditMode = true;

            Slider obj = Util.GetByID(id);

            ViewBag.Title = "Edit slider - " + obj.Title;
            return View("_Form", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(int id, Slider slider)
        {
            try
            {
                if (slider.ImgFile != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + slider.ImgFile.FileName;
                    string path = Server.MapPath(Files_Dir);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    slider.ImgFile.SaveAs(path + uniqueFileName);
                    slider.Image = uniqueFileName;
                }
            }
            catch (Exception)
            {
                if (!AppEnv.Production)
                {
                    throw;
                }
            }

            if (Util.Update(slider))
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
