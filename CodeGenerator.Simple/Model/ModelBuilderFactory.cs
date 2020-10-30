using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public class CommentBuilder
    {
        private readonly string tableName;
        private readonly string connectionString;

        public CommentBuilder(string tableName, string connectionString)
        {
            this.tableName = tableName;
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 获取字段注释
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<DescriptionModel> Build()
        {
            string sql = $"{ConstInfo.GetDescriptionSql}  AND b.name='{tableName}'";
            using (SqlConnection conn = new SqlConnection(connectionString))//ConnectionString为自己连接字符串
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<DescriptionModel> list = reader.ToDescriptionInfo();
                return list;
            }
        }
    }

    public class FieldPropertyBuilder
    {
        private readonly string tableName;
        private readonly string connectionString;

        public FieldPropertyBuilder(string tableName, string connectionString)
        {
            this.tableName = tableName;
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<FieldProperty> Build()
        {
            string sql = $"{ConstInfo.GetTableInfoSql}  where a.table_name='{tableName}'";
            using (SqlConnection conn = new SqlConnection(connectionString))//ConnectionString为自己连接字符串
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<FieldProperty> list = reader.ToTableModel();
                return list;
            }
        }
    }

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

    public class Consitence
    {
        private readonly string fileType;
        private readonly string tableName;

        public Consitence(string fileType, string tableName)
        {
            this.fileType = fileType;
            this.tableName = tableName;
        }

        public void FlushToDisk(string modelContent)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory + "\\Model\\";

            //是否存在文件夹,不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var realPath = Path.Combine(directory, fileType);

            if (!Directory.Exists(realPath))
            {
                Directory.CreateDirectory(realPath);
            }

            var path = $"{realPath}\\{tableName}{fileType}.cs";

            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch { }

            StreamWriter sr = File.CreateText(path);
            sr.Write(modelContent.ToString());
            sr.Flush();
            sr.Close();
        }

    }

    public class ModelBuilderFactory
    {
        private string namespaceStr;

        private List<string> tableNames;
        private List<string> postFixs;

        private string connectionString;

        public ModelBuilderFactory SetConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
            return this;
        }

        public ModelBuilderFactory SetNameSpace(string namespaceStr)
        {
            this.namespaceStr = namespaceStr;
            return this;
        }

        public ModelBuilderFactory SetTableNames(List<string> tableNames)
        {
            this.tableNames = tableNames;
            return this;
        }
      
        public ModelBuilderFactory SetPostFixs(List<string> postFixs)
        {
            this.postFixs = postFixs;
            return this;
        }

        public  void Build()
        {
            foreach (var tableName in tableNames)
            {
                CommentBuilder commentBuilder = new CommentBuilder(tableName, connectionString);
                FieldPropertyBuilder fieldPropertyBuilder = new FieldPropertyBuilder(tableName, connectionString);
                ModelBuilder modelBuilder = new ModelBuilder(tableName, namespaceStr, fieldPropertyBuilder.Build(), commentBuilder.Build());

                foreach (var postFix in postFixs)
                {
                    string modelStr = modelBuilder.BuildModelWithPostFix(postFix);
                    Consitence consitence = new Consitence(postFix, tableName);
                    consitence.FlushToDisk(modelStr);
                }
            }
        }
    }
}