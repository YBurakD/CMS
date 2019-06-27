using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trial.Core.Helpers
{
    public class PageHelper : BaseHelper
    {
        public static Core.Models.Page.PageItem parseRoute(string path, List<Core.Models.Category.CategoryItem> categories)
        {
            Core.Models.Page.PageItem page = null;
            try
            {
                using (var db = new DataModel.Entities())
                {
                    var paths = path.Split('/');
                    var lastElement = paths[paths.Length - 2];
                    if (paths.Length == 2)
                    {
                        var category = GetCategory(lastElement, db);
                        if (category != null)
                        {
                            page = new Models.Page.PageItem()
                            {
                                Category = category,
                                PageType = Enums.Page.PageType.Category
                            };
                        }
                    }
                    else
                    {
                        var category = GetCategory(lastElement, db);
                        if (category != null)
                        {
                            page = new Models.Page.PageItem()
                            {
                                Category = category,
                                Contents = GetContentList(db),
                                PageType = Enums.Page.PageType.Category
                            };
                        }
                        else
                        {
                            var content = GetContent(lastElement, db);
                            if(content != null)
                            {
                                page = new Models.Page.PageItem()
                                {
                                    Content = content,
                                    Category = GetCategory(paths[paths.Length - 3], db)

                                };

                            }
                        }
                    }

                    /*
                    if (paths.Length >= 1)
                        lastElement = paths[paths.Length - 2];
                    else
                        lastElement = "Index";

                    var reqPage = requestPage(paths, categories, db);
                    */


                }
            }
            catch (Exception) { }
            return page;
        }
        private static List<Core.Models.Content.ContentItem> GetContentList(DataModel.Entities db)
        {
            var contents = (from i in db.Contents
                            where i.Deleted == null
                            select new Models.Content.ContentItem()
                            {
                                Id = i.Id,
                                Title = i.Title,
                                Description = i.Description,
                                Body = i.Body,
                                Url = i.Url,
                                ContentDate = i.ContentDate,
                                CategoryId = i.CategoryId,
                                UserId = i.UserId,
                                Type = i.Type,
                                Status = i.Status,
                                Created = i.Created,
                                Updated = i.Updated,

                            }).ToList();
            return contents;
        }
        private static Core.Models.Content.ContentItem GetContent(string url, DataModel.Entities db)
        {
            var content = (from i in db.Contents
                              where i.Deleted == null && i.Url == url
                              select new Models.Content.ContentItem()
                              {
                                  Id = i.Id,
                                  Title = i.Title,
                                  Description = i.Description,
                                  Body = i.Body,
                                  Url = i.Url,
                                  ContentDate = i.ContentDate,
                                  CategoryId = i.CategoryId,
                                  UserId = i.UserId,
                                  Type = i.Type,
                                  Status = i.Status,
                                  Created = i.Created,
                                  Updated = i.Updated,

                              }).SingleOrDefault();
            return content;
        }
        private static Core.Models.Category.CategoryItem GetCategory(string url, DataModel.Entities db)
        {
            var categories = (from i in db.Categories
                              where i.Deleted == null && i.Url == url
                              select new Models.Category.CategoryItem()
                              {
                                  Id = i.Id,
                                  Name = i.Name,
                                  Body = i.Body,
                                  ParentId = i.ParentId,
                                  Url = i.Url,
                                  Row = i.Row,
                                  UserId = i.UserId,
                                  Type = i.Type,
                                  Status = i.Status,
                                  Created = i.Created,
                                  Updated = i.Updated

                              }).SingleOrDefault();
            return categories;
        }
    }
}
