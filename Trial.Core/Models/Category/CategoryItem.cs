using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Trial.Core.Models.Category
{
    public class CategoryItem
    {
        public Guid Id { get; set; }
        [Display(Name = "CategoryName", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "CategoryNameEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(250, ErrorMessageResourceName = "Length250", ErrorMessageResourceType = typeof(Strings))]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        [AllowHtml]
        [Display(Name = "CategoryBody", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "CategoryBodyEmpty", ErrorMessageResourceType = typeof(Strings))]
        [MinLength(3, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Strings))]
        public string Body { get; set; }
        public string Url { get; set; }
        public Guid? ParentId { get; set; }
        public int Row { get; set; }
        public Guid UserId { get; set; }
        public Core.Enums.Category.CategoryType Type { get; set; }
        public Core.Enums.Category.CategoryStatus Status { get; set; }
        public Core.Enums.Category.CategoryLanguage Language { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public List<CategoryItem> Categories { get; set; }
        public int CategoryRow { get; set; }
    }
}
