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
        public ActionResult Index(Guid? id)
        {
            try
            {
                string html;
                var content = new Core.Models.Content.ContentPageItem();
                if (id == null)
                {
                    html = Core.Helpers.Content.ContentHelper.ContentHtml();
                }
                else
                {
                    html = Core.Helpers.Content.ContentHelper.ContentHtml((Guid)id);
                    content.categoryName = Core.Helpers.Category.CategoryHelper.Get((Guid)id).Name;
                    content.categoryId = id;
                }
                content.ContentHtml = html;
                content.isCategoryChosen = (id == null);
                return View(content);
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
                if(id == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                var content = new Core.Models.Content.ContentItem
                {
                    Categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList(),
                    CategoryId = (Guid)id
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
            content.Categories = Core.Helpers.Category.CategoryHelper.GetAllCategoriesForList();
            return View(content);
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