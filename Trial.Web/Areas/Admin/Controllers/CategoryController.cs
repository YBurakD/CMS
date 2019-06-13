using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        [Attributes.CustomAuthorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Core.Models.Category.CategoryItem category)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}