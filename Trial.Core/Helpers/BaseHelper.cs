using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace Trial.Core.Helpers
{
    public class BaseHelper
    {
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
