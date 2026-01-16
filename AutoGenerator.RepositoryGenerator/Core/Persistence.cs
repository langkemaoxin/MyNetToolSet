using System;
using System.IO;

namespace AutoGenerator.RepositoryGenerator.Core
{
    public class Persistence
    {
        private readonly string _tableName;
        private readonly PersistenceEnumType _persistenceEnumType;

        public Persistence(string tableName, PersistenceEnumType persistenceEnumType)
        {
            _tableName = tableName;
            _persistenceEnumType = persistenceEnumType;
        }

        public void FlushToDisk(string content)
        {
            string directory = $"{AppDomain.CurrentDomain.BaseDirectory}\\{_persistenceEnumType.ToString()}";

            //是否存在文件夹,不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = $"{directory}\\{_tableName}{_persistenceEnumType.ToString()}.cs";

            if (File.Exists(path)) File.Delete(path);

            StreamWriter sr = File.CreateText(path);
            sr.Write(content.ToString());
            sr.Flush();
            sr.Close();
        }
    }
}