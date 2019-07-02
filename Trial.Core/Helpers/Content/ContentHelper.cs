using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Helpers.Content
{
    public class ContentHelper : BaseHelper
    {   //List<Core.Models.Category.CategoryItem> categories
        static public string ContentHtml(List<Core.Models.Category.CategoryItem> categories)
        {
            var html = "<ol class='dd-list'>";
            foreach (var category in categories)
            {
                var display = "inherit";
                if (category.Language == Enums.Category.CategoryLanguage.English)
                {
                    display = "none";
                }
                html += $"<li class='dd-item' data-id='{category.Id}'data-language={category.LanguageData.Name} style='display:{display}'><div class='dd-handle'><span class='dd-name'>{category.Name}</span><a class='btn btn-primary ink-reaction btn-raised pull-right jsContentSelectBtn' data-id='{category.Id}'><i class='fa fa-chevron-right'></i></a></div>";
                if (category.Categories?.Count > 0)
                {
                    html += ContentHtml(category.Categories);
                }
                html += "</li>";
            }
            html += "</ol>";
            return html;
        }
        static public string ContentHtml(Guid id)
        {
            var html = "<ol class='dd-list'>";
            var category = Helpers.Category.CategoryHelper.Get(id);
            var contents = GetAllContents();
            foreach (var content in contents)
            {
                if (content.CategoryId == category.Id)
                    html += $"<li class='dd-item' data-id='{content.Id}'><div class='dd-handle'><span class='dd-name'>{content.Title}</span><a class='btn btn-primary ink-reaction btn-raised pull-right jsContentUpdateBtn' data-id='{content.Id}'><i class='fa fa-edit'></i></a></div></li>";
            }
            html += "</ol>";
            return html;
        }
        static public List<Core.Models.Content.ContentItem> GetAllContents()
        {
            List<Core.Models.Content.ContentItem> list = new List<Models.Content.ContentItem>();
            using (var db = new DataModel.Entities())
            {
                var result = (from i in db.Contents
                              where i.Deleted == null
                              select new Core.Models.Content.ContentItem
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
                                  Deleted = i.Deleted
                              });
                foreach (var item in result)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        static public Core.Models.Content.ContentItem Get(Guid id)
        {
            using (var db = new DataModel.Entities())
            {
                var result = (from i in db.Contents
                              where i.Id == id && i.Deleted == null
                              select new Core.Models.Content.ContentItem
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
                                  Deleted = i.Deleted

                              }).SingleOrDefault();
                return result;
            }
        }
        static public Core.Models.Content.ContentItem Save(Core.Models.Content.ContentItem content)
        {
            using (var db = new DataModel.Entities())
            {
                var result = (from i in db.Contents where i.Id == content.Id && i.Deleted == null select i).SingleOrDefault();
                if (result == null)
                {
                    var url = Core.Helpers.BaseHelper.ConvertToUrl(content.Title);
                    content.Url = url;
                    var row = 0;
                    while ((from i in db.Contents where i.Url == content.Url select i).SingleOrDefault() != null)
                    {
                        row++;
                        content.Url = $"{url}-{row}";
                    }
                    return Create(content, db);
                }
                return Update(content, db, result);
            }
        }

        static private Core.Models.Content.ContentItem Update(Core.Models.Content.ContentItem content, DataModel.Entities db, DataModel.Content dbContent)
        {
            dbContent.Title = !string.IsNullOrEmpty(content.Title) && content.Title != dbContent.Title ? content.Title : dbContent.Title;
            dbContent.Body = !string.IsNullOrEmpty(content.Body) && content.Body != dbContent.Body ? content.Body : dbContent.Body;
            dbContent.CategoryId = content.CategoryId != null && content.CategoryId != dbContent.CategoryId ? content.CategoryId : dbContent.CategoryId;
            dbContent.UserId = content.UserId != null && content.UserId != dbContent.UserId ? content.UserId : dbContent.UserId;
            dbContent.Type = content.Type != dbContent.Type ? content.Type : dbContent.Type;
            dbContent.Status = content.Status != dbContent.Status ? content.Status : dbContent.Status;
            db.SaveChanges();
            return content;
        }


        static private Core.Models.Content.ContentItem Create(Core.Models.Content.ContentItem content, DataModel.Entities db)
        {
            var newContent = new DataModel.Content()
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                Url = content.Url,
                Body = content.Body,
                ContentDate = content.ContentDate,
                CategoryId = content.CategoryId,
                UserId = content.UserId,
                Type = content.Type,
                Status = content.Status,
                Created = DateTime.Now
            };
            db.Contents.Add(newContent);
            db.SaveChanges();
            return content;
        }

    }
}
