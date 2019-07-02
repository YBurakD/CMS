﻿using System;
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


        // lang = en/
        // lang = t4/
        [Route("{*path}")]
        public ActionResult Index(string path)
        {
            try
            {
                var lang = Core.Helpers.PageHelper.GetLanguage(path, out path);
                var categoryList = Core.Helpers.Category.CategoryHelper.GetAllCategoriesByLanguage(lang);
                Trial.Core.Models.Page.PageItem pageItem = null;
                
                var menu = Core.Helpers.Home.HomeHelper.htmlMenuBar(categoryList,lang);
                if (path != null)
                {
                    pageItem = Core.Helpers.PageHelper.parseRoute(path, categoryList);
                }
                else
                {
                    pageItem = new Core.Models.Page.PageItem()
                    {
                        PageType = Core.Enums.Page.PageType.Home
                    };
                }
                if(pageItem != null)
                {
                    pageItem.MenuBar = menu;

                    if (pageItem.Content == null && pageItem.Category != null)
                        return View("Category",pageItem);
                    else if(pageItem.Content != null && pageItem.Category != null)
                        return View("Content", pageItem);
                    return View(pageItem);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Category()
        {
            return View();
        }
    }
}