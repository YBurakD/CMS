using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Trial.Core.Helpers.Users
{
    public class UserHelper : BaseHelper
    {
        static public Core.Models.User.UserItem CurrentUser()
        {
            if (FormsAuthentication.CookiesSupported)
            {
                var req = HttpContext.Current.Request;
                if (req.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    var cookieValue = req.Cookies[FormsAuthentication.FormsCookieName].Value;
                    if (!string.IsNullOrEmpty(cookieValue))
                    {
                        var ticket = FormsAuthentication.Decrypt(cookieValue);

                        if (!ticket.Expired)
                        {
                            var user = Get(ticket.Name);
                            if (user != null && user.Role > Enums.User.UserRole.Pending)
                            {
                                var roles = Core.Helpers.Users.UserHelper.GetRoles(user.Role);
                                var newTicket = new FormsAuthenticationTicket(0, user.Name, DateTime.Now, DateTime.Now.AddHours(2), false, Newtonsoft.Json.JsonConvert.SerializeObject(roles));
                                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(newTicket)));
                                return user;
                            }
                        }
                    }
                }
            }
            return null;   
        }

        static public string[] GetRoles(Core.Enums.User.UserRole role)
        {
            var rs = new List<string>();

            var roles = Enum.GetValues(typeof(Enums.User.UserRole)).Cast<Enums.User.UserRole>();
            foreach (var r in roles)
            {
                if (r <= role)
                {
                    rs.Add(r.ToString());
                }
            }
            return rs.ToArray();

        }

        static public Core.Models.User.UserItem Get(string name)
        {
            using (var db = new DataModel.Entities())
            {
                var result = (from i in db.Users
                              where i.Name == name && i.Deleted == null
                              select new Core.Models.User.UserItem()
                              {
                                  Id = i.Id,
                                  FirstName = i.FirstName,
                                  LastName = i.LastName,
                                  Name = i.Name,                    
                                  Mail = i.Mail,
                                  Role = i.Role
                              }).SingleOrDefault();
                return result;
            }
        }

        static public Core.Models.User.UserItem Get(Guid id)
        {
            using (var db = new DataModel.Entities())
            {
                var result = (from i in db.Users
                              where i.Id == id && i.Deleted == null
                              select new Core.Models.User.UserItem()
                              {
                                  Id = i.Id,
                                  FirstName = i.FirstName,
                                  LastName = i.LastName,
                                  Name = i.Name,
                                  Mail = i.Mail,
                                  Role = i.Role
                              }).SingleOrDefault();
                return result;
            }

        }
        static public Core.Models.User.UserItem Login(Core.Models.User.UserLoginItem user)
        {
            using (var db = new DataModel.Entities())
            {
                var hashedPass = Hash(user.Password);
                var result = (from i in db.Users
                              where (i.Name == user.UserNameMail || i.Mail == user.UserNameMail) &&
                              i.Password == hashedPass && 
                              i.Deleted == null
                              select new Core.Models.User.UserItem()
                              {
                                  Id = i.Id,
                                  FirstName = i.FirstName,
                                  LastName = i.LastName,
                                  Name = i.Name,
                                  Mail = i.Mail,
                                  Role = i.Role
                              }).SingleOrDefault();
                return result;
            }
        }

        static public Core.Models.User.UserItem Save(Core.Models.User.UserItem user)
        {
            using (var db = new DataModel.Entities())
            {
                var result = (from i in db.Users where i.Id == user.Id && i.Deleted == null select i).SingleOrDefault();
                if (result == null)
                {
                    var mailCheck = (from i in db.Users where i.Mail == user.Mail select i).SingleOrDefault();
                    if (mailCheck != null)
                    {
                        throw new Exception(Core.Strings.DiffMail);
                    }

                    var nameCheck = (from i in db.Users where i.Name == user.Name select i).SingleOrDefault();
                    if (nameCheck != null)
                    {
                        throw new Exception(Core.Strings.DiffUserName);
                    }

                    return Create(user, db);
                }
                else
                {
                    
                    var hashPass = Hash(user.Password);
                    if(hashPass != result.Password)
                    {
                        throw new Exception(Core.Strings.WrongPassword);
                    }

                    return Update(user, db, result);
                }

            }
        }

        static private Core.Models.User.UserItem Update(Core.Models.User.UserItem user, DataModel.Entities db, DataModel.User dbUser)
        {
            /// TODO
            dbUser.FirstName = !string.IsNullOrEmpty(user.FirstName) && user.FirstName != dbUser.FirstName ? user.FirstName : dbUser.FirstName;
            dbUser.LastName = !string.IsNullOrEmpty(user.LastName) && user.LastName != dbUser.LastName ? user.LastName : dbUser.LastName;
            db.SaveChanges();
            return user;
        }

        static private Core.Models.User.UserItem Create(Core.Models.User.UserItem user, DataModel.Entities db)
        {
            var pass = Hash(user.PasswordAgain);
            var newUser = new DataModel.User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = pass,
                Name = user.Name,
                Mail = user.Mail,
                Role = user.Role,
                Type = user.Type,
                Status = user.Status,
                Created = DateTime.Now
            };
            db.Users.Add(newUser);
            db.SaveChanges();
            return user;
        }
    }
}
