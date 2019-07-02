using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Core.Enums
{
    static public class Helper
    {
        // ♥
        public class EnumModel<T>
        {
            public T Self { get; set; }
            public int Value => Convert.ToInt32(Self);
            public string Name { get; set; }
            public string Text { get; set; }
            public string Description { get; set; }
            public string Group { get; set; }
            public string ShortName { get; set; }
            public int Order { get; set; }
            public string Category { get; set; }
            public List<string> CategoryList => !string.IsNullOrEmpty(Category) ? Category.Split(',').ToList() : new List<string>();
        }

        static public EnumModel<T> Get<T>(T value)
        {
            var data = typeof(T).GetField(value.ToString());
            var displayAttr = data.GetCustomAttribute<DisplayAttribute>();
            var descAttr = data.GetCustomAttribute<DescriptionAttribute>();
            return new EnumModel<T>()
            {
                Self = (T)Enum.Parse(typeof(T), data.Name, false),
                Name = data.Name,
                Text = displayAttr?.ResourceType != null ? new ResourceManager(typeof(Core.Strings)).GetString(displayAttr.Name) : data.Name,
                Description = descAttr?.Description,
                Group = data.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "GroupName").Any()).Any() ? displayAttr.GroupName : "",
                ShortName = data.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "ShortName").Any()).Any() ? displayAttr.ShortName : "",
                Category = data.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "Prompt").Any()).Any() ? displayAttr.Prompt : "",
                Order = data.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "Order").Any()).Any() ? displayAttr.Order : 0
            };
        }

        static public List<EnumModel<T>> List<T>()
        {
            var type = typeof(T);
            var data = type.GetFields().
                Where(x => x.IsStatic)
                .Select(x => new
                {
                    Data = x,
                    DisplayAttribute = x.GetCustomAttribute<DisplayAttribute>(),
                    DescriptionAttribute = x.GetCustomAttribute<DescriptionAttribute>(),
                    CustomAttributes = x.CustomAttributes,
                })
                .Select(x => new EnumModel<T>()
                {
                    Self = (T)Enum.Parse(typeof(T), x.Data.Name, false),
                    Name = x.Data.Name,
                    Text = x.DisplayAttribute?.ResourceType != null ? new ResourceManager(typeof(Core.Strings)).GetString(x.DisplayAttribute.Name) : x.Data.Name,
                    Description = x.DescriptionAttribute?.Description,
                    Group = x.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "GroupName").Any()).Any() ? x.DisplayAttribute.GroupName : "",
                    ShortName = x.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "ShortName").Any()).Any() ? x.DisplayAttribute.ShortName : "",
                    Category = x.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "Prompt").Any()).Any() ? x.DisplayAttribute.Prompt : "",
                    Order = x.CustomAttributes.Where(c => c.NamedArguments.Where(n => n.MemberName == "Order").Any()).Any() ? x.DisplayAttribute.Order : 0
                })
                .OrderBy(x => x.Order)
                .ToList();
            return data;
        }

        static public List<string> StringList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(x => x.ToString()).ToList();
        }
        static public List<T> All<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
        static public int Count<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Count();
        }
    }
}
