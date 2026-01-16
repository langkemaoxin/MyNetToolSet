using System;
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace MyToolSet
{
    public class FileHelperExtension
    {
        public static string GetLength(double lengthOfDocument)
        {
            if (lengthOfDocument < 1024)
                return Math.Round(lengthOfDocument, 2).ToString(CultureInfo.InvariantCulture) + 'B';

            if (lengthOfDocument > 1024 && lengthOfDocument <= Math.Pow(1024, 2))
                return Math.Round((lengthOfDocument / 1024.0), 2).ToString(CultureInfo.InvariantCulture) + "KB";

            if (lengthOfDocument > Math.Pow(1024, 2) && lengthOfDocument <= Math.Pow(1024, 3))
                return Math.Round((lengthOfDocument / 1024.0 / 1024.0), 2).ToString(CultureInfo.InvariantCulture) + "M";

            return Math.Round((lengthOfDocument / 1024.0 / 1024.0 / 1024.0), 2).ToString(CultureInfo.InvariantCulture) + "GB";
        }

        public static string GetFileBelongs(FileInfo fileInfo)
        {
            try
            {
                FileSecurity fileSecurity = fileInfo.GetAccessControl();
                IdentityReference identityReference = fileSecurity.GetOwner(typeof(NTAccount));
                return identityReference.Value;
            }
            catch (Exception ex)
            {
                return "<UnKnow>";
            }
        }

        public static string GetFileBelongs(string fileInfo)
        {
            if (string.IsNullOrWhiteSpace(fileInfo))
            {
                return "<UnKnow>";
            }

            return GetFileBelongs(new FileInfo(fileInfo));
        }

        public static void CreateFile(string filePath)
        {
            AppendToFile(filePath, "");
        }

        /// <summary>
        ///     往文件添加一行内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public static void AppendToFile(string filePath, string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return;

            if (!File.Exists(filePath))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite,
                    4096, true))
                {
                    using (var streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("gb2312")))
                    {
                        streamWriter.WriteLine(content);
                    }
                }

                return;
            }

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite,
                    4096, true))
                {
                    using (var streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("gb2312")))
                    {
                        streamWriter.WriteLine(content);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        /// <summary>
        ///     读取文件的第一行
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadSingleLineFromFile(string filePath)
        {
            if (!File.Exists(filePath)) throw new Exception($"文件路径不存在:{filePath}");

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite,
                    4096, true))
                {
                    using (var stringReader = new StreamReader(fileStream, Encoding.GetEncoding("gb2312")))
                    {
                        var readLine = stringReader.ReadLine();
                        return readLine;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}