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

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrFirstCharToLower(this string str)
        {
            if (str == null)
                return "";
            int iLen = str.Length;
            if (iLen == 0)
                return "";
            if (iLen == 1)
                return str.ToLower();
            return str[0].ToString().ToLower() + str.Substring(1);
        }
    }
}