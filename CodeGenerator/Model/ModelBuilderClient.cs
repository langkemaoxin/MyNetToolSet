using System.Collections.Generic;

namespace CodeGenerator
{
    public class ModelBuilderClient
    {
        public  static void Execute()
        {
            ConstInfo.EntityNamespace = "namespace JZFZ.CADProjectManager.Domain.Entity";
            ConstInfo.ModelNamespace = "namespace JZFZ.CADProjectManager.Domain.Model";

            var tableNames = new List<string>();
            tableNames.Add("CMDRecord ");
            tableNames.Add("CMDRecordZY ");
            tableNames.Add("FileEditingTime ");
            tableNames.Add("OpenRecord ");
            tableNames.Add("PrjEvaluate ");
            tableNames.Add("ErrorInfo ");
            tableNames.Add("UseRecord ");

            foreach (var tableName in tableNames)
            {
                ModelBuilderCore.BuildModel(tableName.Trim());
            }
        }
    }
}