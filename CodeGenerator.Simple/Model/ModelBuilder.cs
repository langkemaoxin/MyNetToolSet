using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator
{


	public class ModelBuilder
	{
		public string TableName { get; set; }
		private readonly string modelNamespace;
		private readonly List<FieldProperty> fieldPropertys;
		private readonly List<DescriptionModel> descriptionModels;

		public ModelBuilder(string tableName, string modelNamespace, List<FieldProperty> fieldPropertys, List<DescriptionModel> descriptionModels)
		{
			this.TableName = tableName;
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

			stringBuilder.Append($"public class {TableName}{modelPostfix} \r\n{{\r\n");

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
				var desc = descriptionModels.FirstOrDefault(o => o.ColumnName.Equals(item.ColumnName, StringComparison.InvariantCultureIgnoreCase))?.ColumnDescript;
				return desc;
			}
			catch
			{
				return string.Empty;
			}
		}
		private string BuildType(string type, string nullAble)
		{
			// 统一转换为小写便于比较
			string typeLower = type.ToLower();
			string nullAbleLower = nullAble.ToLower();

			if (typeLower.Contains("int"))
			{
				return nullAbleLower == "yes" ? "int?" : "int";
			}
			else if (typeLower == "bit")
			{
				return nullAbleLower == "yes" ? "bool?" : "bool";
			}
			else if (typeLower == "tinyint")
			{
				return nullAbleLower == "yes" ? "byte?" : "byte";
			}
			else if (typeLower == "smallint")
			{
				return nullAbleLower == "yes" ? "short?" : "short";
			}
			else if (typeLower == "bigint")
			{
				return nullAbleLower == "yes" ? "long?" : "long";
			}
			else if (typeLower == "date")  // 添加date类型
			{
				return nullAbleLower == "yes" ? "DateTime?" : "DateTime";
			}
			else if (typeLower == "datetime" || typeLower == "datetime2" || typeLower == "smalldatetime")
			{
				return nullAbleLower == "yes" ? "DateTime?" : "DateTime";
			}
			else if (typeLower == "datetimeoffset")
			{
				return nullAbleLower == "yes" ? "DateTimeOffset?" : "DateTimeOffset";
			}
			else if (typeLower == "time")
			{
				return nullAbleLower == "yes" ? "TimeSpan?" : "TimeSpan";
			}
			else if (typeLower == "decimal" || typeLower == "numeric")
			{
				return nullAbleLower == "yes" ? "decimal?" : "decimal";
			}
			else if (typeLower == "float" || typeLower == "real")
			{
				return nullAbleLower == "yes" ? "double?" : "double";  // SQL float对应C# double
			}
			else if (typeLower == "money" || typeLower == "smallmoney")
			{
				return nullAbleLower == "yes" ? "decimal?" : "decimal";
			}
			else if (typeLower.Contains("char") || typeLower.Contains("text") ||
					 typeLower.Contains("nchar") || typeLower.Contains("nvarchar") ||
					 typeLower.Contains("varchar") || typeLower == "xml")
			{
				return "string";  // 字符串类型在C#中本身就是可空的
			}
			else if (typeLower == "uniqueidentifier")
			{
				return nullAbleLower == "yes" ? "Guid?" : "Guid";
			}
			else if (typeLower == "binary" || typeLower == "varbinary" || typeLower == "image")
			{
				return "byte[]";
			}
			else
			{
				throw new ArgumentException($"不支持的数据类型: {type}");
			}
		}
	}
}