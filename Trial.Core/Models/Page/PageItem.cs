using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Models.Page
{
    public class PageItem
    {
        public Core.Models.User.UserItem User { get; set; }
        public List<Core.Models.Category.CategoryItem> Categories { get; set; }
        public List<Core.Models.Content.ContentItem>  Contents { get; set; }
        public Core.Models.Category.CategoryItem Category { get; set; }
        public Core.Models.Content.ContentItem Content { get; set; }
        public Core.Enums.Page.PageType PageType { get; set; }
        public string Header { get; set; }
        public string MenuBar { get; set; }
        public string Footer { get; set; }
    }
}
