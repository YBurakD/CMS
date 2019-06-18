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
        [HttpPost]
        public JsonResult Sort(Core.Models.Category.CategorySortItem sort)
        {
            try
            {
                Core.Helpers.Category.CategoryHelper.Sort(sort);
                return Json(new { message = Core.Strings.UpdateSuccess, action = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, action = "error" });
            }
        }


        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var categories = Core.Helpers.Category.CategoryHelper.GetAllCategories();
                var html = Core.Helpers.Category.CategoryHelper.CategoryList(categories);
                return View(new Core.Models.Category.CategoryPageItem()
                {
                    CategoryHtml = html
                });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                var categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
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
            var categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
            category.Categories = categories;
            return View(category);
        }
        [HttpGet]
        public ActionResult Update(Guid? id)
        {
            try
            {
                if(id == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                var categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
                var category = Core.Helpers.Category.CategoryHelper.Get((Guid)id);
                category.Categories = categories;
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Update(Core.Models.Category.CategoryItem category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cat = Core.Helpers.Category.CategoryHelper.Save(category);
                    TempData["Message"] = Core.Strings.UpdateSuccess;
                }
                else
                {
                    TempData["Message"] = Core.Helpers.BaseHelper.ModelStateErrors(ModelState);
                }
            }
            catch (Exception ex )
            {
                TempData["Error"] = ex.Message;
            }

            var categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
            category.Categories = categories;
            return View(category);
        }
    }
}