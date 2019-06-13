using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Models.User
{
    public class UserLoginItem
    {
        [Display(Name ="UserNameMail", ResourceType =typeof(Strings))]
        [Required(ErrorMessageResourceName = "UserNameMailEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings),ErrorMessageResourceName = "Length50")]
        public string UserNameMail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "PasswordEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName ="Length50")]
        public string Password { get; set; }

    }
}
