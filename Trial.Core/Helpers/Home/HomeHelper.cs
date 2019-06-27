using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Helpers.Home
{
    public class HomeHelper : BaseHelper
    {
        public static string htmlHeader()
        {
            string html = "";

            return html;
        }
        public static string htmlMenuBar(List<Core.Models.Category.CategoryItem> categoryList)
        {
            string html =  "<div class=\"wrapper row2\">"+
                           " <nav id = \"mainav\" class=\"hoc clear\">"+
                           " <ul class=\"clear\">";
            html += htmlMenuBar(categoryList, false, "");
            html += "</ul>"+"<form action=\"#\"><select>";
            html += htmlMenuBar(categoryList, true,"");
            html += "</select></form>";
            html += "</nav></div>";        
            return html;
        }
        public static string htmlMenuBar(List<Core.Models.Category.CategoryItem> categoryList,bool form,string url)
        {
            string html = "";
            if (form)
            {
                foreach (var category in categoryList)
                {
                    html += $"<option selected=\"selected\" value=\"\">{category.Name}</option>";
                    if (category.Categories?.Count > 0)
                    {
                        html += htmlMenuBar(category.Categories, form,"");
                    }
                }
                
            }
            else
            {
                foreach (var category in categoryList)
                {
                    html += "<li>";
                    string newUrl = url + "/" +category.Url;
                    if (category.Categories?.Count > 0)
                    {
                        html += $"<a class=\"drop\" href=\"{newUrl}/\">{category.Name}</a>";
                        html += "<ul>";
                        html += htmlMenuBar(category.Categories,form, newUrl);
                        html += "</ul>";
                    }
                    else
                    {
                        html += $"<a href=\"{newUrl}/\">{category.Name}</a>";
                    }
                   
                    html += "</li>";
                }
            }
            return html;
        }
    }
}
