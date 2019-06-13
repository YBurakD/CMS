using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Trial.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                if(Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    var cookieValue = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                    if (!string.IsNullOrEmpty(cookieValue))
                    {
                        var ticket = FormsAuthentication.Decrypt(cookieValue);

                        if (!ticket.Expired)
                        {
                            var data = JsonConvert.DeserializeObject<string[]>(ticket.UserData);
                            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(ticket.Name, "Forms"), data);
                        }
                    }
                }
            }
        }
    }
}
