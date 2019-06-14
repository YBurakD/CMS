using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;

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

        public static string ConvertToPrice(double d)
        {
            var s = string.Format("{0:0.00}", d);
            var splitS = s.ToString().Split(new string[] { ".", "," }, StringSplitOptions.RemoveEmptyEntries);
            var data = splitS[0];
            if (data.Length < 4)
                return $"{data},{splitS[1]}";
            var rowCnt = data.Length % 3;
            var txtMoney = data.Substring(0, rowCnt);
            if (rowCnt == 0)
            {
                txtMoney += data.Substring(0, 3);
                rowCnt = 3;
            }
            for (int i = rowCnt; i < data.Length - 1; i += 3)
                txtMoney += "." + data.Substring(i, 3);
            return $"{txtMoney.Replace("-.", "-")},{splitS[1]}";
        }

        public static string ConvertToNumber(long? mny)
        {
            if (mny == null)
                return "0";
            string strMny = mny.ToString();
            if (strMny.Length < 4)
                return strMny;
            int rowCnt = strMny.Length % 3;
            string txtMoney = strMny.Substring(0, rowCnt);
            if (rowCnt == 0)
            {
                txtMoney += strMny.Substring(0, 3);
                rowCnt = 3;
            }
            for (int i = rowCnt; i < strMny.Length - 1; i += 3)
                txtMoney += "." + strMny.Substring(i, 3);
            return txtMoney.Replace("-.", "-");
        }

        static public string ConvertToUrl(string title, string param = null)
        {
            var url = title;
            url = url.Trim();
            url = url.ToLower(new System.Globalization.CultureInfo("tr-Tr"));
            url = url.Replace("ı", "i");
            url = url.Replace("ğ", "g");
            url = url.Replace("ü", "u");
            url = url.Replace("ö", "o");
            url = url.Replace("ş", "s");
            url = url.Replace("ş", "s");
            url = url.Replace("ç", "c");
            url = Regex.Replace(url, @"&\w+;", "");
            url = Regex.Replace(url, @"[^A-Za-z0-9\-\s]", "");
            url = Regex.Replace(url, @"\s+", "-");
            url = Regex.Replace(url, @"\-{2,}", "-");
            if (url.EndsWith("-"))
                url = url.Substring(0, url.Length - 1);
            if (param == null)
                return url;
            else
                return $"{param}-{url}";
        }
    }
}
