using System.Text;

namespace CodeGenerator
{
    public class ServiceInterfaceBuilder
    {
        public static string BuildServiceInterface(string name)
        {
            var templateBuilder = new StringBuilder();

            templateBuilder.AppendLine("using JZFZ.Infrastructure.Common.Model.Response;");

            templateBuilder.AppendLine("using System.Collections.Generic;");
            templateBuilder.AppendLine("using System.Threading.Tasks;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Entity;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Model;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Inject;");
            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("namespace $$PROJECTNAME$$.Business.Interfaces");
            templateBuilder.AppendLine("{");
            templateBuilder.AppendLine("    public interface I$$TABLENAME$$Service : ITransientInject");
            templateBuilder.AppendLine("    {");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<ResultJsonInfo<$$TABLENAME$$Entity>> AddAsync($$TABLENAME$$Model model);");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<ResultJsonInfo<$$TABLENAME$$Entity>> InfoAsync($$TABLENAME$$ConditionModel model);");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<ResultJsonInfo<$$TABLENAME$$Entity>> ModifyAsync($$TABLENAME$$Model model);");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<ResultJsonInfo<List<$$TABLENAME$$Entity>>> InfosAsync($$TABLENAME$$ConditionModel model);");
            templateBuilder.AppendLine("    }");
            templateBuilder.AppendLine("}");

            return BusinessBuilderCore.BuildAll(templateBuilder.ToString(), name);
        }
    }
}