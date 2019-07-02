using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Enums.Page
{
    public enum PageType
    {
        Home,
        Category,
        Content
    }
    public enum Language : byte
    {
        [Display(Name = "Turkish", ResourceType = typeof(Strings), Order = 0, ShortName = "tr")]
        Turkish,
        [Display(Name = "English", ResourceType = typeof(Strings), Order = 10, ShortName = "en")]
        English
    }
}
