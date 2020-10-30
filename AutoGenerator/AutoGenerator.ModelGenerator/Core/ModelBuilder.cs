using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public class ModelBuilder
    {
        private readonly string tableName;
        private readonly string modelNamespace;
        private readonly List<FieldProperty> fieldPropertys;
        private readonly List<DescriptionModel> descriptionModels;

        public ModelBuilder(string tableName, string modelNamespace, List<FieldProperty> fieldPropertys, List<DescriptionModel> descriptionModels)
        {
            this.tableName = tableName;
            this.modelNamespace = modelNamespace;
            this.fieldPropertys = fieldPropertys;
            this.descriptionModels = descriptionModels;
        }

        /// <summary>
        /// 根据后缀构建模型
        /// </summary>
        /// <param name="modelPostfix"></param>
        /// <returns></returns>
        public string BuildModelWithPostFix(string modelPostfix)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"using System;\r\n");
            stringBuilder.Append($"namespace {modelNamespace}  \r\n{{\r\n");

            stringBuilder.Append($"public class {tableName}{modelPostfix} \r\n{{\r\n");

            foreach (var item in fieldPropertys)
            {
                stringBuilder.Append($"/// <summary>\r\n");
                stringBuilder.Append($"/// {BuildDescription(item, descriptionModels)}\r\n");
                stringBuilder.Append($"/// </summary>\r\n");
                stringBuilder.Append($"  public {BuildType(item.TypeName, item.IsnullAble)} {item.ColumnName} {{get;set;}}\r\n  \r\n");
            }

            stringBuilder.Append("} \r\n");

            stringBuilder.Append("} \r\n");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取注释
        /// </summary>
        /// <param name="item"></param>
        /// <param name="descriptionModels"></param>
        /// <returns></returns>
        private string BuildDescription(FieldProperty item, List<DescriptionModel> descriptionModels)
        {
            if (descriptionModels == null || !descriptionModels.Any()) return string.Empty;

            try
            {
                var desc = descriptionModels.FirstOrDefault(o => o.ColumnName.Equals(item.ColumnName, StringComparison.InvariantCultureIgnoreCase)).ColumnDescript;
                return desc;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取字段所对应的类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nullAble"></param>
        /// <returns></returns>
        private string BuildType(string type, string nullAble)
        {

            if (type.Equals("int") && nullAble.Equals("NO"))
                return "int";
            else if (type.Equals("int") && nullAble.Equals("YES"))
                return "int?";
            else if (type.Equals("bit") && nullAble.Equals("NO"))
                return "bool";
            else if (type.Equals("bit") && nullAble.Equals("YES"))
                return "bool?";
            else if ((type.Equals("decimal") || type.Equals("numeric") || type.Equals("float") || type.Equals("real")) && nullAble.Equals("NO"))
                return "decimal";
            else if ((type.Equals("decimal") || type.Equals("numeric") || type.Equals("float") || type.Equals("real")) && nullAble.Equals("Yes"))
                return "decimal?";
            else if (type.Equals("datetime") && nullAble.Equals("YES"))
                return "DateTime?";
            else if (type.Equals("datetime") && nullAble.Equals("NO"))
                return "DateTime";
            else if (type.Equals("nchar") || type.Equals("char") || type.Equals("nvarchar") || type.Equals("varchar") || type.Equals("text"))
                return "string";
            else throw new Exception("无此类型");
        }
    }
}