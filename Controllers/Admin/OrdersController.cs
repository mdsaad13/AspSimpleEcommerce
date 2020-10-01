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
    [RoutePrefix("admin/orders")]
    public class OrdersController : Controller
    {
        readonly OrdersUtil Util = new OrdersUtil();
        readonly string ControllerFor = "Order ";
        readonly string IndexUrl = "/admin/orders";

        [Route]
        public ActionResult Index()
        {
            return View(Util.List(true));
        }
        
        [Route("update")]
        public ActionResult Update(int ID, int Status)
        {
            if (Util.UpdateStatus(ID, Status))
            {
                Session["Flash_Success"] = ControllerFor + "updated successfully!";
            }
            else
            {
                Session["Flash_Error"] = ControllerFor + "update failed!<br>Please try again later";
            }
            return RedirectToAction("Index");
        }

    }
}