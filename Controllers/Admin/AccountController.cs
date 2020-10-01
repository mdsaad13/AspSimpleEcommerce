using System.Web.Mvc;
using OnlineShopping.Controllers.Helpers;
using OnlineShopping.DbUtil;

namespace OnlineShopping.Controllers.Admin
{
    public class AccountController : Controller
    {
        readonly AccountUtil Util = new AccountUtil();

        [GuestAdminAuthorized]
        [Route("admin/login")]
        public ActionResult Login()
        {
            return View();
        }

        [GuestAdminAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/login")]
        public ActionResult Login(Models.Admin admin)
        {
            if (Util.AdminLogin(admin.Email, admin.Password))
            {
                Session["Flash_Success"] = "Login Success";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                Session["Flash_Error"] = "Incorrect email or password";
                return View();
            }
        }

        [AdminAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/logout")]
        public ActionResult Logout()
        {
            Session.Remove(AppEnv.AdminSessionKey);
            return View();
        }
    }
}
