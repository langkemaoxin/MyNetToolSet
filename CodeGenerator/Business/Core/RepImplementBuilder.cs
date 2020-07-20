using System.Text;

namespace CodeGenerator
{
    public class RepImplementBuilder
    {
        public static string BuildRepImplement(string name)
        {
            var templateBuilder = new StringBuilder();
            templateBuilder.AppendLine("using JZFZ.Infrastructure.Data.DapperExtensions;");

            templateBuilder.AppendLine("using System.Collections.Generic;");
            templateBuilder.AppendLine("using System.Linq;");
            templateBuilder.AppendLine("using System.Threading.Tasks;");
            templateBuilder.AppendLine("using AutoMapper;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Config;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Entity;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Domain.Model;");
            templateBuilder.AppendLine("using $$PROJECTNAME$$.Inject;");
            templateBuilder.AppendLine("");
            templateBuilder.AppendLine("namespace $$PROJECTNAME$$.Repository.Interface");
            templateBuilder.AppendLine("{");
            templateBuilder.AppendLine("public class $$TABLENAME$$Rep : AbstractRepository<$$TABLENAME$$Entity>, I$$TABLENAME$$Rep");
            templateBuilder.AppendLine("    {");
            templateBuilder.AppendLine("        private readonly IMapper _mapper;");
            templateBuilder.AppendLine("        public $$TABLENAME$$Rep(IConnectionConfig connectionConfig, IMapper mapper)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            SetDbType(DbType.SqlServer);");
            templateBuilder.AppendLine("            ConnectionString = connectionConfig.GetJZCADConnection();");
            templateBuilder.AppendLine("            SlaveConnectionString = connectionConfig.GetJZCADConnection();");
            templateBuilder.AppendLine("            this._mapper = mapper;");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<$$TABLENAME$$Entity> InfoAsync($$TABLENAME$$ConditionModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var items = await GetListAsync(\"\");");
            templateBuilder.AppendLine("            return items.FirstOrDefault();");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<$$TABLENAME$$Entity> ModifyAsync($$TABLENAME$$Model model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var dbItem = await GetAsync(model);");
            templateBuilder.AppendLine("            await UpdateAsync(dbItem);");
            templateBuilder.AppendLine("            return dbItem;");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<List<$$TABLENAME$$Entity>> InfosAsync($$TABLENAME$$ConditionModel model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var items = await GetListAsync(\"\");");
            templateBuilder.AppendLine("            return items.ToList();");
            templateBuilder.AppendLine("        }");


            templateBuilder.AppendLine("        /// <summary>");
            templateBuilder.AppendLine("        /// ");
            templateBuilder.AppendLine("        /// </summary>");
            templateBuilder.AppendLine("        /// <param name=\"model\"></param>");
            templateBuilder.AppendLine("        /// <returns></returns>");
            templateBuilder.AppendLine("        public async Task<$$TABLENAME$$Entity> AddAsync($$TABLENAME$$Model model)");
            templateBuilder.AppendLine("        {");
            templateBuilder.AppendLine("            var addItem = _mapper.Map<$$TABLENAME$$Entity>(model); ");
            templateBuilder.AppendLine("            await InsertAsync(addItem);");
            templateBuilder.AppendLine("            return addItem;");
            templateBuilder.AppendLine("        }");
            templateBuilder.AppendLine("    }");
            templateBuilder.AppendLine("  }");
            return BusinessBuilderCore.BuildAll(templateBuilder.ToString(), name);
        }
    }
}