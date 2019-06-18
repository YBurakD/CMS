using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Models.Category
{
    public class CategorySortItem
    {
        public Guid SourceId { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> SortList { get; set; }
    }
}
