using System.Text;

namespace AutoGenerator.RepositoryGenerator.Core
{
    public class RepositoryBuilder
    {
        private readonly string _tableName;
        private readonly string _modelNamespace;

        public RepositoryBuilder(string tableName, string modelNamespace)
        {
            _tableName = tableName;
            _modelNamespace = modelNamespace;
        }

        public string Build()
        {
            var templateBuilder = new StringBuilder();
            templateBuilder.AppendLine("using System;");
            templateBuilder.AppendLine("using System.Collections.Generic;");
            templateBuilder.AppendLine("using System.Linq;");
            templateBuilder.AppendLine("using System.Text;");
            templateBuilder.AppendLine("using System.Threading.Tasks;"); 
            templateBuilder.AppendLine("using JZFZ.Infrastructure.Data.DapperExtensions;");
            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("namespace $NameSpace.Repository");
            templateBuilder.AppendLine("{");
            templateBuilder.AppendLine("    public class $TableNameRep: AbstractRepository<$TableNameEntity>");
            templateBuilder.AppendLine("    {");
            templateBuilder.AppendLine("        public $TableNameRep()");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            SetDbType(DbType.SqlServer);");
            templateBuilder.AppendLine("            ConnectionString = ConnectionConfig.GetConnectionString();");
            templateBuilder.AppendLine("            SlaveConnectionString = ConnectionConfig.GetConnectionString();");
            templateBuilder.AppendLine("        }");
            templateBuilder.AppendLine("    }");
            templateBuilder.AppendLine("}");

            var result = templateBuilder.ToString();
            result = result.Replace("$TableName", _tableName);
            result = result.Replace("$NameSpace", _modelNamespace);

            return result;
        }
    }
}