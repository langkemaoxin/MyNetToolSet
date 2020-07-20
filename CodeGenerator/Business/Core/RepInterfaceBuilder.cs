using System.Text;

namespace CodeGenerator
{
    public class RepInterfaceBuilder
    {
        public static string BuildRepInterface(string name)
        {
            var templateBuilder = new StringBuilder();

            templateBuilder.AppendLine("using JZFZ.Infrastructure.Data.DapperExtensions;");

            templateBuilder.AppendLine("using System.Collections.Generic;");
            templateBuilder.AppendLine("using System.Linq;");
            templateBuilder.AppendLine("using System.Threading.Tasks;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Config;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Entity;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Model;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Inject;");

            templateBuilder.AppendLine("namespace $$PROJECTNAME$$.Repository.Interface");
            templateBuilder.AppendLine("{");
            templateBuilder.AppendLine("    public interface I$$TABLENAME$$Rep: ITransientInject");
            templateBuilder.AppendLine("    {");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<$$TABLENAME$$Entity> AddAsync($$TABLENAME$$Model model);");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<$$TABLENAME$$Entity> InfoAsync($$TABLENAME$$ConditionModel model);");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<$$TABLENAME$$Entity> ModifyAsync($$TABLENAME$$Model model);");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        Task<List<$$TABLENAME$$Entity>> InfosAsync($$TABLENAME$$ConditionModel model);");
            templateBuilder.AppendLine("    }");
            templateBuilder.AppendLine("}");

            return BusinessBuilderCore.BuildAll(templateBuilder.ToString(), name);
        }
    }
}