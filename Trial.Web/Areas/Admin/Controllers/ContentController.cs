using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trial.Web.Areas.Admin.Controllers
{
    [Attributes.CustomAuthorize(Roles = "Administrator")]
    public class ContentController : Controller
    {
        // GET: Admin/Content
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var html = Core.Helpers.Content.ContentHelper.ContentHtml();
                return View(new Core.Models.Content.ContentPageItem()
                {
                    ContentHtml = html
                });
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create(Guid? id)
        {
            try
            {
                if (id != null && Core.Helpers.Category.CategoryHelper.Get((Guid)id) != null)
                { ViewBag.selectedCategory = id; }
                    ViewBag.categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
                    var content = new Core.Models.Content.ContentItem
                    {
                    };
                    return View(content);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Core.Models.Content.ContentItem content)
        {
            try
            {
                ViewBag.categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
                var user = (Core.Models.User.UserItem)(ViewBag.User);
                if (ModelState.IsValid)
                {
                    content.Id = Guid.NewGuid();
                    content.UserId = user.Id;
                    Core.Helpers.Content.ContentHelper.Save(content);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View();
        }
        [HttpGet]
        public ActionResult Update(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    RedirectToAction(nameof(Index));
                }
                var content = Core.Helpers.Content.ContentHelper.Get((Guid)id);
                ViewBag.categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
                return View(content);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }
    }
}