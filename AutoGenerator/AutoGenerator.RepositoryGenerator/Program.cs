using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGenerator.RepositoryGenerator.Core;

namespace AutoGenerator.RepositoryGenerator
{
    public class RepositoryClassFactory
    {
        private string namespaceStr;

        private List<string> tableNames;

        public RepositoryClassFactory SetNameSpace(string namespaceStr)
        {
            this.namespaceStr = namespaceStr;
            return this;
        }

        public RepositoryClassFactory SetTableNames(List<string> tableNames)
        {
            this.tableNames = tableNames;
            return this;
        }


        public void Build()
        {
            foreach (var tableName in tableNames)
            {
                var repositoryBuilder = new RepositoryBuilder(tableName, namespaceStr);
                var persistence = new Persistence(tableName, PersistenceEnumType.Repository);
                persistence.FlushToDisk(repositoryBuilder.Build());
            }
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            var list = new List<string>
            {
                "DwgPlatformConfig",
                "DwgMenu",
                "DwgInfo",
                "DwgFileFetchScheduleRecord",
                "DwgFileAnalyticScheduleRecord"
            };

          new RepositoryClassFactory()
              .SetNameSpace("JZFZ.DwgInfoPlatform.Repository")
              .SetTableNames(list)
              .Build();

          string dir = AppDomain.CurrentDomain.BaseDirectory;
          System.Diagnostics.Process.Start("explorer.exe", dir);
        }
    }
}
