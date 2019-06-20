using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Helpers.Category
{
    public class CategoryHelper : BaseHelper
    {
        static public void Sort(Core.Models.Category.CategorySortItem sort)
        {
            using (var db = new Core.DataModel.Entities())
            {
                var categories = (from i in db.Categories where sort.SortList.Contains(i.Id) select i).ToList();
                foreach (var category in categories)
                {
                    if (category.Id == sort.SourceId)
                    {
                        category.ParentId = sort.ParentId;
                    }
                    category.Row = sort.SortList.IndexOf(category.Id);
                }
                db.SaveChanges();
            }
        }



        static public string CategoryList(List<Core.Models.Category.CategoryItem> categories)
        {
            var html = "<ol class='dd-list'>";
            foreach (var category in categories)
            {
                html += $"<li class='dd-item' data-id='{category.Id}'><div class='dd-handle'><span class='dd-name'>{category.Name}</span><a class='btn btn-primary ink-reaction btn-raised pull-right jsUpdateBtn' data-id='{category.Id}'><i class='fa fa-edit'></i></a></div>";
                if (category.Categories?.Count > 0)
                {
                    html += CategoryList(category.Categories);
                }
                html += "</li>";
            }
            html += "</ol>";
            return html;
        }


        static public string HTMLifier(List<Models.Category.CategoryItem> ContentList)
        {
            string HTMLContext = "<ol class='dd-list'>";
            int count = 0;

            //If no content added yet, return a warning text.
            if (ContentList == null)
            {
                HTMLContext += Core.Strings.WarningAddContent + "</ol>";
            }
            foreach (var item in ContentList)
            {
                int numberofDashes = item.DisplayName.Count(f => f == '-');
                if (count > numberofDashes || item == ContentList.First())
                {
                    string olClosers = new StringBuilder().Insert(0, "</li>\n</ol>\n", count - numberofDashes).ToString() + "</li>";
                    //  numberofDashes!=0 ? "</li>\n" + olClosers + " < li class='dd-item'>\n<div class='dd-handle'>\n" + item.Name + "</div>\n" : 
                    HTMLContext += olClosers + "<li class='dd-item'>\n<div class='dd-handle'>\n" + item.Name + "</div>\n";
                    count = numberofDashes;
                }
                else if (count < numberofDashes)
                {
                    HTMLContext += "<ol class='dd-list'>" + "<li class='dd-item'><div class='dd-handle'>\n" + item.Name + "\n</div>\n";
                    count = numberofDashes;
                }
                else if (count == numberofDashes)
                {
                    HTMLContext += "</li>\n<li class='dd-item'><div class='dd-handle'>\n" + item.Name + "</div>\n";
                }
                if (item == ContentList.Last())
                {
                    HTMLContext += new StringBuilder().Insert(0, "</li>\n</ol>\n", numberofDashes + 1).ToString();
                }
            }
            return HTMLContext;
        }

        static public List<Core.Models.Category.CategoryItem> GetAllCategories()
        {
            using (var db = new DataModel.Entities())
            {
                var all = All();
                var parents = all.Where(x => x.ParentId == null).ToList();
                foreach (var parent in parents)
                {
                    parent.DisplayName = parent.Name;
                    parent.Categories = GetAllSubCategories(parent, all, parent.CategoryRow);
                }
                return parents;
            }
        }
        static private List<Core.Models.Category.CategoryItem> GetAllSubCategories(Core.Models.Category.CategoryItem parent, List<Core.Models.Category.CategoryItem> categories, int row)
        {
            row++;
            var lst = new List<Core.Models.Category.CategoryItem>();
            var result = categories.Where(x => x.ParentId == parent.Id).ToList();
            foreach (var r in result)
            {
                r.Categories = GetAllSubCategories(r, categories, row);
                lst.Add(r);
            }
            return lst;
        }
        static public List<Core.Models.Category.CategoryItem> GetAllCategoryItemsSorted()
        {
            using (var db = new DataModel.Entities())
            {
                List<Core.Models.Category.CategoryItem> list = new List<Core.Models.Category.CategoryItem>();
                var query = (from i in db.Categories
                        where i.Deleted == null
                        orderby i.Name ascending
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
                        });
                foreach (var item in query)
                {
                    list.Add(item);
                }
                return list;
            }
        }
        static public List<Core.Models.Category.CategoryItem> GetAllCategoriesForList()
        {
            using (var db = new DataModel.Entities())
            {
                var lst = new List<Core.Models.Category.CategoryItem>();
                var all = All();
                var parents = all.Where(x => x.ParentId == null).ToList();
                foreach (var parent in parents)
                {
                    parent.DisplayName = parent.Name;
                    lst.Add(parent);
                    lst.AddRange(GetAllSubCategoriesList(parent, all, parent.CategoryRow));
                }
                return lst;
            }
        }

        static private List<Core.Models.Category.CategoryItem> GetAllSubCategoriesList(Core.Models.Category.CategoryItem parent, List<Core.Models.Category.CategoryItem> categories, int row)
        {
            var lst = new List<Core.Models.Category.CategoryItem>();
            row++;
            var result = categories.Where(x => x.ParentId == parent.Id).ToList();
            foreach (var r in result)
            {
                var display = "";
                for (int i = 0; i < row; i++)
                {
                    display += "-";
                }
                r.DisplayName = $"{display} {r.Name}";
                r.CategoryRow = row;
                lst.Add(r);
                lst.AddRange(GetAllSubCategoriesList(r, categories, row));
            }
            return lst;
        }


        static public Core.Models.Category.CategoryItem Get(Guid id)
        {
            using (var db = new DataModel.Entities())
            {
                var list = (from i in db.Categories
                            where i.Deleted == null && i.Id == id
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
                            }).SingleOrDefault();
                return list;
            }
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
            dbCategory.Type = category.Type != dbCategory.Type ? category.Type : dbCategory.Type;
            dbCategory.Status = category.Status != dbCategory.Status ? category.Status : dbCategory.Status;
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
