using System.Text;

namespace AutoGenerator.RepositoryGenerator.Core
{
    public class ServiceBuilder
    {
        private readonly string _tableName;
        private readonly string _modelNamespace;

        public ServiceBuilder(string tableName, string modelNamespace)
        {
            _tableName = tableName;
            _modelNamespace = modelNamespace;
        }

        public string Build()
        {
            var templateBuilder = new StringBuilder();
            templateBuilder.AppendLine("namespace $NameSpace");
            templateBuilder.AppendLine("{");
            templateBuilder.AppendLine("    public interface I$TableNameService");
            templateBuilder.AppendLine("    {");

            templateBuilder.AppendLine("    }");

            templateBuilder.AppendLine("    public class $TableNameService : I$TableNameService");
            templateBuilder.AppendLine("    {");
            templateBuilder.AppendLine("        private $TableNameRep $TableNameRep = new $TableNameRep();");
            templateBuilder.AppendLine("    }");
            templateBuilder.AppendLine("}");

            var result = templateBuilder.ToString();
            result = result.Replace("$TableName", _tableName);
            result = result.Replace("$NameSpace", _modelNamespace);

            return result;
        }
    }
}