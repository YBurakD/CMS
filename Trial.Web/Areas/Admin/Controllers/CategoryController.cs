using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Areas.Admin.Controllers
{
    [Attributes.CustomAuthorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                var user = Core.Helpers.User.UserHelper.CurrentUser();
                if(user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                var categories = Core.Helpers.Category.CategoryHelper.GetAllCategories();
                var category = new Core.Models.Category.CategoryItem()
                {
                    Categories = categories
                };
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Core.Models.Category.CategoryItem category)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }
    }
}