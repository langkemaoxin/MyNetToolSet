using System.Collections.Generic;
using AutoGenerator.RepositoryGenerator.Core;

namespace AutoGenerator.RepositoryGenerator
{
    public class ServiceClassFactory
    {
        private string namespaceStr;

        private List<string> tableNames;

        public ServiceClassFactory SetNameSpace(string namespaceStr)
        {
            this.namespaceStr = namespaceStr;
            return this;
        }

        public ServiceClassFactory SetTableNames(List<string> tableNames)
        {
            this.tableNames = tableNames;
            return this;
        }


        public void Build()
        {
            foreach (var tableName in tableNames)
            {
                var repositoryBuilder = new ServiceBuilder(tableName, namespaceStr);
                var persistence = new Persistence(tableName, PersistenceEnumType.Service);
                persistence.FlushToDisk(repositoryBuilder.Build());
            }
        }
    }
}