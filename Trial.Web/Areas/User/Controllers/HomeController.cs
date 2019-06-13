using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        [Attributes.CustomAuthorize(Roles = "Default")]
        public ActionResult Index()
        {
            return View();
        }
    }
}