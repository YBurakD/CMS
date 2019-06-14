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
                var user = (Core.Models.User.UserItem)(ViewBag.User);
                if (ModelState.IsValid)
                {
                    category.Id = Guid.NewGuid();
                    category.UserId = user.Id;
                    category.Row = 0;
                    Core.Helpers.Category.CategoryHelper.Save(category);
                    TempData["Message"] = Core.Strings.UpdateSuccess;
                }
                else
                {
                    TempData["Error"] = Core.Helpers.BaseHelper.ModelStateErrors(ModelState);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            var categories = Core.Helpers.Category.CategoryHelper.GetAllCategories();
            category.Categories = categories;
            return View(category);
        }
    }
}