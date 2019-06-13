using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Controllers
{    
    public class HomeController : Controller
    {

        /*var s = Test(1, 2, 3, 4, 5, 6, 452, 352, 352, 352, 352, 35235);
        public string Test(params int[] test)
        {
            return string.Join(",", test[0]);
        }*/

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