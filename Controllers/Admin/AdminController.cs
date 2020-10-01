using System.Web.Mvc;

namespace OnlineShopping.Controllers.Admin
{
    [Helpers.AdminAuthorized]
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        // GET: Slider
        public ActionResult Index()
        {
            DbUtil.GeneralUtil generalUtil = new DbUtil.GeneralUtil();

            ViewBag.Total = generalUtil.Count("orders");
            ViewBag.TotalPending = generalUtil.CountByArgs("orders", "status = 1");
            ViewBag.TotalOutforDelivery = generalUtil.CountByArgs("orders", "status = 2");
            ViewBag.TotalDelivered = generalUtil.CountByArgs("orders", "status = 3");
            ViewBag.TotalCancelled = generalUtil.CountByArgs("orders", "status = 3");

            return View();
        }

    }
}