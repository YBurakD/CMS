using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("{*path?}")]
        public ActionResult Index(string path)
        {
            return View();
        }


        /*[Route("blog/{*path?}")]
        public ActionResult Blog(string path)
        {
            return View();
        }*/
    }
}