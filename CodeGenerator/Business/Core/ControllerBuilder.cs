using System.Text;

namespace CodeGenerator
{
    public class ControllerBuilder
    {
        public static string BuildController(string name)
        {
            var templateBuilder = new StringBuilder();

            templateBuilder.AppendLine("using System.Collections.Generic;");
            templateBuilder.AppendLine("using System.Threading.Tasks;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Business.Interfaces;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Entity;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Request;");
            templateBuilder.AppendLine("using JZFZ.Infrastructure.Common.Model.Response;");
            templateBuilder.AppendLine("using Microsoft.AspNetCore.Mvc;");

            templateBuilder.AppendLine("namespace $$PROJECTNAME$$.WebAPI2.Controllers");
            templateBuilder.AppendLine("{");

            templateBuilder.AppendLine("    /// <summary>");
            templateBuilder.AppendLine("    /// ");
            templateBuilder.AppendLine("    /// </summary>");
            templateBuilder.AppendLine("    [ApiController]");
            templateBuilder.AppendLine("    [Route(\"[controller]\")]");
            templateBuilder.AppendLine("    public class $$TABLENAME$$Controller");
            templateBuilder.AppendLine("    {");
            templateBuilder.AppendLine("        private readonly I$$TABLENAME$$Service _$$PARAMETERNAME$$Service;");

            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>"); 
            templateBuilder.AppendLine("        public $$TABLENAME$$Controller(I$$TABLENAME$$Service $$PARAMETERNAME$$Service)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            this._$$PARAMETERNAME$$Service = $$PARAMETERNAME$$Service;");
            templateBuilder.AppendLine("        }");

            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        [HttpPost]");
            templateBuilder.AppendLine("        [Route(\"Add\")]");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<$$TABLENAME$$Entity>> AddAsync($$TABLENAME$$AddModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            return await _$$PARAMETERNAME$$Service.AddAsync(model);");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        [HttpPost]");
            templateBuilder.AppendLine("        [Route(\"Modify\")]");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<$$TABLENAME$$Entity>> ModifyAsync($$TABLENAME$$Model model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            return await _$$PARAMETERNAME$$Service.ModifyAsync(model);");
            templateBuilder.AppendLine("        }");

            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        [HttpPost]");
            templateBuilder.AppendLine("        [Route(\"Info\")]");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<$$TABLENAME$$Entity>> InfoAsync($$TABLENAME$$ConditionModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            return await _$$PARAMETERNAME$$Service.InfoAsync(model);");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        [HttpPost]");
            templateBuilder.AppendLine("        [Route(\"Infos\")]");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<List<$$TABLENAME$$Entity>>> InfosAsync($$TABLENAME$$ConditionModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            return await _$$PARAMETERNAME$$Service.InfosAsync(model);");
            templateBuilder.AppendLine("        }");
             

            templateBuilder.AppendLine("    }");
            templateBuilder.AppendLine("}");

            return BusinessBuilderCore.BuildAll(templateBuilder.ToString(), name);
        }
    }
}