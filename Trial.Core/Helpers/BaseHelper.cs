using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;

namespace Trial.Core.Helpers
{
    public class BaseHelper
    {
        /*static public Core.Models.User.UserItem GetUserByCookie(string cookieName)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                var req = HttpContext.Current.Request;
                if (req.Cookies[cookieName] != null)
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
                                var roles = Core.Helpers.User.UserHelper.GetRoles(user.Role);
                                var newTicket = new FormsAuthenticationTicket(0, user.Name, DateTime.Now, DateTime.Now.AddHours(2), false, Newtonsoft.Json.JsonConvert.SerializeObject(roles));
                                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(newTicket)));
                                return user;
                            }
                        }
                    }
                }
            }
            return null;
        }*/
        static public string ModelStateErrors(ModelStateDictionary model)
        {
            var query = (from i in model.Values
                        from j in i.Errors
                        select j.ErrorMessage).ToList();

            var result = string.Join("<br/>", query);
            return result;
        } 

        static public string Hash(string text)
        {
            var b = Hash<MD5CryptoServiceProvider>(text);
            var s = Hash<SHA256CryptoServiceProvider>(b);
            return s;
        }

        static public string Hash<T>(string text) where T: HashAlgorithm, new()
        {
            var bytes = Encoding.Default.GetBytes(text);
            var s = new T().ComputeHash(bytes);
            var str = BitConverter.ToString(s).Replace("-", "");
            return str;
        }
    }
}
