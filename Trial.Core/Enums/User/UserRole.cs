using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Enums.User
{
    public enum UserRole : byte
    {
        [Display(Name = "Pending", ResourceType = typeof(Strings))]
        Pending,
        [Display(Name = "Default", ResourceType = typeof(Strings))]
        Default,
        [Display(Name = "Manager", ResourceType = typeof(Strings))]
        Manager,
        [Display(Name = "Administrator", ResourceType = typeof(Strings))]
        Administrator
    }
}
