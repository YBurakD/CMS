using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Trial.Core.Models.Content
{
    public class ContentItem
    {
        public Guid Id { get; set; }
        [Display(Name = "ContentTitle", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "ContentTitleEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(250,ErrorMessageResourceName = "Length250", ErrorMessageResourceType = typeof(Strings))]
        public string Title { get; set; }
        [Display(Name = "ContentDescription", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "ContentDescriptionEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(500, ErrorMessageResourceName = "Length500", ErrorMessageResourceType = typeof(Strings))]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name = "ContentBody", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "ContentBodyEmpty", ErrorMessageResourceType = typeof(Strings))]
        public string Body { get; set; }
        public string Url { get; set; }
        public DateTime ContentDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public byte? Type { get; set; }
        public Core.Enums.Content.ContentStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public List<Category.CategoryItem> Categories { get; set; }
    }
}
