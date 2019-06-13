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
    }
}
