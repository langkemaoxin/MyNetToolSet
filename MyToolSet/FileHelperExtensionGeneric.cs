using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyToolSet
{
    public class FileHelperExtensionGeneric<T>
    {
        public static void RecursioSearch(string path, ref List<T> result, Func<string, T> func)
        {
            if (!Directory.Exists(path)) return;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            List<string> fileList = directoryInfo.GetFiles().Select(o => o.FullName).ToList();

            if (fileList.Any())
            {
                result.AddRange(fileList.Select(o => func(o)).Where(o => o != null).ToList());
            }

            DirectoryInfo[] directories = directoryInfo.GetDirectories();

            foreach (var item in directories)
            {
                RecursioSearch(item.FullName, ref result, func);
            }
        }

        public static List<T> ReadFromFile(string listFilePath, Func<string, T> customFunc)
        {
            var list = new List<T>();

            if (!File.Exists(listFilePath)) return list;

            using (var fileStream = new FileStream(listFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite,
                bufferSize: 4096, useAsync: true))
            {
                using (var streamReader = new StreamReader(fileStream, System.Text.Encoding.Default))
                {
                    var stringReader = new StreamReader(listFilePath, Encoding.GetEncoding("gb2312"));
                    string readLine;

                    do
                    {
                        readLine = stringReader.ReadLine();

                        if (readLine == null) continue;

                        var model = customFunc(readLine);

                        if (model != null)
                        {
                            list.Add(model);
                        }

                    } while (readLine != null);
                }
            }
            return list;
        }
    }
}