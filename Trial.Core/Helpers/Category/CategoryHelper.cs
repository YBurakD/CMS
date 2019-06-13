using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Helpers.Category
{
    class CategoryHelper : BaseHelper
    {
        public List<Core.Models.Category.CategoryItem> GetAllCategories()
        {
            using (var db = new DataModel.Entities())
            {
                var list = (from i in db.Categories
                            select new Core.Models.Category.CategoryItem()
                            {
                                Id = i.Id,
                                Name = i.Name,
                                Body = i.Body,
                                ParentId = i.ParentId,
                                Row = i.Row,
                                UserId = i.UserId
                                
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
