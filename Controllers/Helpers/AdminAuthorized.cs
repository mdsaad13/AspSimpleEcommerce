using System.Web.Mvc;

namespace OnlineShopping.Controllers.Helpers
{
    public class AdminAuthorized :AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (!AppEnv.AdminAuthByPass)
            {
                if (context.HttpContext.Session[AppEnv.AdminSessionKey] == null)
                {
                    context.Result = new RedirectResult("/admin/login");
                }
            }
            
        }
    }

    public class GuestAdminAuthorized : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (!AppEnv.AdminAuthByPass)
            {
                if (context.HttpContext.Session[AppEnv.AdminSessionKey] != null)
                {
                    context.Result = new RedirectResult("/admin");
                }
            }
        }
    }
}