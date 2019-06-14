using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = Core.Helpers.User.UserHelper.CurrentUser();
            if (this.AuthorizeCore(filterContext.HttpContext) && user != null)
            {
                var vb = filterContext.Controller.ViewBag;
                vb.User = user;
                base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary()
                {
                    {"controller", "Home" },
                    {"action", "Index" }
                });
            }
        }
    }
}