﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Helpers.Category
{
    public class CategoryHelper : BaseHelper
    {
        /*
        * <ol class='dd-list'>
            <li class='dd-item'><div class='dd-handle'>Item 1</div></li>
            <li class='dd-item'><div class='dd-handle'>Item 2</div></li>
            <li class='dd-item'><div class='dd-handle'>Item 3</div>
                <ol class='dd-list'>
                    <li class='dd-item' data-id='4'><div class='dd-handle'>Item 4</div></li>
                </ol>
            </li>
        </ol>
        */

        static public string NestedListHTML = "<ol class='dd-list'>\n";

        static public List<Core.Models.Category.CategoryItem> GetAllCategories()
        {
            using (var db = new DataModel.Entities())
            {
                var lst = new List<Core.Models.Category.CategoryItem>();
                var all = All();
                var parents = all.Where(x => x.ParentId == null).ToList();
                foreach (var parent in parents)
                {
                    parent.DisplayName = parent.Name;
                    NestedListHTML += "<li class='dd-item'><div class='dd-handle'>" + parent.DisplayName + "</div>\n";
                    parent.Categories = GetAllSubCategories(parent, all, parent.CategoryRow);
                    NestedListHTML += "</li>\n";
                    lst.Add(parent);
                    lst.AddRange(parent.Categories);
                }
                NestedListHTML += "</ol>\n";
                return lst;
            }
        }
        

        static private List<Core.Models.Category.CategoryItem> GetAllSubCategories(Core.Models.Category.CategoryItem parent, List<Core.Models.Category.CategoryItem> categories, int row)
        {
            
            var lst = new List<Core.Models.Category.CategoryItem>();
            row++;
            var result = categories.Where(x => x.ParentId == parent.Id).ToList();
            NestedListHTML += result != null ? "<ol class='dd-list'>\n" : "";
            foreach (var r in result)
            {
                var display = "";
                for (int i = 0; i < row; i++)
                {
                    display += "-";
                }
                r.DisplayName = $"{display} {r.Name}";

                NestedListHTML += "<li class='dd-item'><div class='dd-handle'>" + r.DisplayName + "</div></li>\n";
                r.CategoryRow = row;
                r.Categories = GetAllSubCategories(r, categories, row);
                
                lst.Add(r);
                lst.AddRange(r.Categories);
            }
            NestedListHTML += result != null ? "</ol>\n" : "";
            return lst;
        }


        static public List<Core.Models.Category.CategoryItem> All()
        {
            using (var db = new DataModel.Entities())
            {
                var list = (from i in db.Categories
                            where i.Deleted == null
                            orderby i.Row
                            select new Core.Models.Category.CategoryItem()
                            {
                                Id = i.Id,
                                Name = i.Name,
                                Body = i.Body,
                                ParentId = i.ParentId,
                                Row = i.Row,
                                UserId = i.UserId,
                                Type = i.Type,
                                Status = i.Status,
                                Created = i.Created,
                                Updated = i.Updated
                            }).ToList();
                return list;
            }
        }
        static public Core.Models.Category.CategoryItem Save(Core.Models.Category.CategoryItem category)
        {
            using (var db = new DataModel.Entities())
            {
                var result = (from i in db.Categories where i.Id == category.Id && i.Deleted == null select i).SingleOrDefault();
                if (result == null)
                {
                    var url = Core.Helpers.BaseHelper.ConvertToUrl(category.Name);
                    category.Url = url;
                    var row = 0;
                    while ((from i in db.Categories where i.Url == category.Url select i).SingleOrDefault() != null)
                    {
                        row++;
                        category.Url = $"{url}-{row}";
                    }

                    return Create(category, db);
                }
                return Update(category, db, result);
            }
        }

        static private Core.Models.Category.CategoryItem Update(Core.Models.Category.CategoryItem category, DataModel.Entities db, DataModel.Category dbCategory)
        {
            dbCategory.Name = !string.IsNullOrEmpty(category.Name) && category.Name != dbCategory.Name ? category.Name : dbCategory.Name;
            dbCategory.Body = !string.IsNullOrEmpty(category.Body) && category.Body != dbCategory.Body ? category.Body : dbCategory.Body;
            dbCategory.ParentId = category.ParentId != null && category.ParentId != dbCategory.ParentId ? category.ParentId : dbCategory.ParentId;
            db.SaveChanges();
            return category;
        }

        static private Core.Models.Category.CategoryItem Create(Core.Models.Category.CategoryItem category, DataModel.Entities db)
        {
            var newCategory = new DataModel.Category()
            {
                Id = category.Id,
                Name = category.Name,
                Url = category.Url,
                Body = category.Body,
                ParentId = category.ParentId,
                Row = category.Row,
                UserId = category.UserId,
                Type = category.Type,
                Status = category.Status,
                Created = DateTime.Now
            };
            db.Categories.Add(newCategory);
            db.SaveChanges();
            return category;
        }
    }
}
