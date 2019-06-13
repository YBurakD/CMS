using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        [Attributes.CustomAuthorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}