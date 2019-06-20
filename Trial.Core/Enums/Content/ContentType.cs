using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Enums.Content
{
   public enum ContentType : byte
    {
        [Display(Name = "Hide", ResourceType = typeof(Strings))]
        Hide,
        [Display(Name = "Navigation", ResourceType = typeof(Strings))]
        Navigation
    }
}
