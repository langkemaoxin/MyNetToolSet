using System;
using System.ComponentModel;
using System.Reflection;

namespace MyToolSet
{
    public static class EnumExtension
    {
        public static string GetDescription(this System.Enum obj)
        {
            if (obj == null) { return string.Empty; }
            string objName = obj.ToString();
            Type t = obj.GetType();
            FieldInfo fi = t.GetField(objName);
            DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return arrDesc?[0]?.Description;
        }
    }
}