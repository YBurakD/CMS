using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Enums.Category
{
    public enum CategoryLanguage : byte
    {
        [Display(Name = "Turkish", ResourceType = typeof(Strings))]
        Turkish,
        [Display(Name = "English", ResourceType = typeof(Strings))]
        English
    }
}
