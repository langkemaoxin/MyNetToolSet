using System;
using System.Security.Cryptography;
using System.Text;

namespace MyToolSet
{
    public static class StringExtension
    {
        public static string ToMD5(this string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = string.Empty;
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        public static string ToPinYinString(this string str)
        {
            return str.ToPinYin();
        }

        public static string ToPinYinAbbrString(this string str)
        {
            return str.ToPinYinAbbr();
        }
    }
}