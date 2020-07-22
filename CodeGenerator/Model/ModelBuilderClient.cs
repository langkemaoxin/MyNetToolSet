using System.Collections.Generic;

namespace CodeGenerator
{
    public class ModelBuilderClient
    {
        public  static void Execute()
        {
            ConstInfo.EntityNamespace = "namespace JZFZ.Projectmanager.Domain.Entity";
            ConstInfo.ModelNamespace = "namespace JZFZ.Projectmanager.Domain.Request";

            var tableNames = new List<string>();
            tableNames.Add("PreBindCompanyAuthorize"); 

            foreach (var tableName in tableNames)
            {
                ModelBuilderCore.BuildModel(tableName.Trim());
            }
        }
    }
}