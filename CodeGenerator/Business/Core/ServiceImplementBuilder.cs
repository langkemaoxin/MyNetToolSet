using System.Text;

namespace CodeGenerator
{
    public class ServiceImplementBuilder
    {
        public static string BuildServiceImplement(string name)
        {
            var templateBuilder = new StringBuilder();

            templateBuilder.AppendLine("using JZFZ.Infrastructure.Common.Model.Response;");

            templateBuilder.AppendLine("using System.Collections.Generic;");
            templateBuilder.AppendLine("using System.Threading.Tasks;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Business.Interfaces;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Entity;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Model;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Repository.Interface;");

            templateBuilder.AppendLine("namespace $$PROJECTNAME$$.Business.Implement");
            templateBuilder.AppendLine("{");
            templateBuilder.AppendLine("    public class $$TABLENAME$$Service : I$$TABLENAME$$Service");
            templateBuilder.AppendLine("    {");
            templateBuilder.AppendLine("        private readonly I$$TABLENAME$$Rep _$$PARAMETERNAME$$Rep;");


            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        public $$TABLENAME$$Service(I$$TABLENAME$$Rep $$PARAMETERNAME$$Rep)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            _$$PARAMETERNAME$$Rep = $$PARAMETERNAME$$Rep;");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// 添加单个对象");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<$$TABLENAME$$ResponseModel>> AddAsync($$TABLENAME$$AddModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var entityItem = await _$$PARAMETERNAME$$Rep.AddAsync(model);");
            templateBuilder.AppendLine("            return ResultJsonInfo<$$TABLENAME$$ResponseModel>.GetSucceedObject(entityItem);");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// 修改单个对象");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<$$TABLENAME$$ResponseModel>> ModifyAsync($$TABLENAME$$Model model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var entityItem = await _$$PARAMETERNAME$$Rep.ModifyAsync(model);");
            templateBuilder.AppendLine("            return ResultJsonInfo<$$TABLENAME$$ResponseModel>.GetSucceedObject(entityItem);");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// 获取单个对象");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<$$TABLENAME$$ResponseModel>> InfoAsync($$TABLENAME$$ConditionModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var entityItem = await _$$PARAMETERNAME$$Rep.InfoAsync(model);");
            templateBuilder.AppendLine("            return ResultJsonInfo<$$TABLENAME$$ResponseModel>.GetSucceedObject(entityItem);");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("");

            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// 获取对象列表");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<ResultJsonInfo<List<$$TABLENAME$$ResponseModel>>> InfosAsync($$TABLENAME$$ConditionModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var entityItem = await _$$PARAMETERNAME$$Rep.InfosAsync(model);");
            templateBuilder.AppendLine("            return ResultJsonInfo<List<$$TABLENAME$$ResponseModel>>.GetSucceedObject(entityItem);");
            templateBuilder.AppendLine("        }");

            templateBuilder.AppendLine("    }");
            templateBuilder.AppendLine("}");

            return BusinessBuilderCore.BuildAll(templateBuilder.ToString(), name);
        }
    }
}