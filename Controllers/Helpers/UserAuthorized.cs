using System.Web.Mvc;

namespace OnlineShopping.Controllers.Helpers
{
    public class UserAuthorized : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session[AppEnv.UserSessionKey] == null)
            {
                context.Result = new RedirectResult("/account/login");
            }
        }
    }

    public class GuestUserAuthorized : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session[AppEnv.UserSessionKey] != null)
            {
                context.Result = new RedirectResult("/account");
            }
        }
    }
}