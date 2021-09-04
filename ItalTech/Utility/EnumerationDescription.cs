using System;
using System.ComponentModel;
using System.Linq;

namespace ItalTech.Utility
{
    public static class EnumerationDescription
    {
        public static string GetDescription<T>(this T val)
        {
            var enumType = val.GetType();
            var name = Enum.GetName(enumType, val);
            var descriptionAttr = (DescriptionAttribute)enumType.GetField(name).GetCustomAttributes(false).Where(a => a.GetType() == typeof(DescriptionAttribute)).SingleOrDefault();
            return descriptionAttr?.Description;
        }
    }
}
