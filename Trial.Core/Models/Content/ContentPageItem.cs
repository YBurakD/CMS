using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Models.Content
{
    public class ContentPageItem
    {
        public ContentItem content { get; set; }
        public List<Category.CategoryItem> categoryList { get; set; }
        public string ContentHtml { get; set; }
    }
}
