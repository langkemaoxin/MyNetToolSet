using System;
using System.IO;

namespace CodeGenerator
{
    public class Consitence
    {
        private readonly string fileType;
        private readonly string tableName;

        public Consitence(string fileType, string tableName)
        {
            this.fileType = fileType;
            this.tableName = tableName;
        }

        public void FlushToDisk(string modelContent)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory + "\\Model\\";

            //是否存在文件夹,不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            } 

            var realPath = Path.Combine(directory, fileType);

            if (!Directory.Exists(realPath))
            {
                Directory.CreateDirectory(realPath);
            }

            var path = $"{realPath}\\{tableName}{fileType}.cs";

            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch { }

            StreamWriter sr = File.CreateText(path);
            sr.Write(modelContent.ToString());
            sr.Flush();
            sr.Close();
        }

    }
}