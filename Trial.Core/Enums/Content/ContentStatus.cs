using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Enums.Content
{
    public enum ContentStatus : byte
    {
        [Display(Name = "Pending", ResourceType = typeof(Strings))]
        Pending,
        [Display(Name = "Approved", ResourceType = typeof(Strings))]
        Approved
    }
}
