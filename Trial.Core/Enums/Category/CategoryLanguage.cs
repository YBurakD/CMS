using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Enums.Category
{
    public enum CategoryLanguage : byte
    {
        [Display(Name = "Turkish", ResourceType = typeof(Strings), Order = 0, ShortName = "")]
        Turkish,
        [Display(Name = "English", ResourceType = typeof(Strings), Order = 10, ShortName = "en")]
        English
    }
}
