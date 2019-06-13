using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Models.User
{
    public class UserItem
    {
        public Guid Id { get; set; }
        [Display(Name = "FirstName", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "FirstNameEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Length50")]
        public string FirstName { get; set; }
        [Display(Name = "LastName", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "LastNameEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Length50")]
        public string LastName { get; set; }
        [Display(Name = "UserName", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "UserNameEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Length50")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessageResourceName = "InvalidMail", ErrorMessageResourceType = typeof(Strings))]
        [Display(Name = "Mail", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "MailEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(500, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Length500")]
        public string Mail { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "PasswordEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Length50")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "PasswordAgain", ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = "PasswordAgainEmpty", ErrorMessageResourceType = typeof(Strings))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Length50")]
        [Compare("Password", ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "InvalidPasswordAgain")]
        public string PasswordAgain { get; set; }
        public Core.Enums.User.UserRole Role { get; set; }
        public byte? Type { get; set; }
        public byte? Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
