using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public class ModelBuilderCore
    {
        public static void BuildModel(string tableName)
        {
            List<DescriptionModel> descriptionModels = CreateDescriptionModel(tableName);

            List<FieldProperty> tableModels = CreateTableModel(tableName);

            var fileTypes = new List<FileType>()
            {
                FileType.Entity,
                FileType.Model,
                FileType.ConditionModel,
                FileType.RequestModel,
                FileType.ResponseModel,
            };

            BuildClient(fileTypes, tableName, tableModels, descriptionModels, GenerateModelStringByFileType);

        }

        public static List<DescriptionModel> CreateDescriptionModel(string tableName)
        {
            string sql = $"{ConstInfo.GetDescriptionSql}  AND b.name='{tableName}'";
            using (SqlConnection conn = new SqlConnection(Program.ConnectionString))//ConnectionString为自己连接字符串
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<DescriptionModel> list = reader.ToDescriptionInfo();
                return list;
            }
        }

        public static List<FieldProperty> CreateTableModel(string tableName)
        {
            string sql = $"{ConstInfo.GetTableInfoSql}  where a.table_name='{tableName}'";
            using (SqlConnection conn = new SqlConnection(Program.ConnectionString))//ConnectionString为自己连接字符串
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<FieldProperty> list = reader.ToTableModel();
                return list;
            }
        }

        private static void BuildClient(List<FileType> fileTypes, string tableName,
            List<FieldProperty> fieldPropertys,
            List<DescriptionModel> descriptionModels,
            Func<FileType, string, List<FieldProperty>, List<DescriptionModel>, string> func)
        {
            foreach (var fileType in fileTypes)
            {
                string stringBuilder = func(fileType,tableName, fieldPropertys, descriptionModels);

                FlushModelToDisk(fileType, $"{tableName}{fileType.ToString()}", stringBuilder);
            }
        }

        private static void FlushModelToDisk(FileType fileType, string tableName, string stringBuilder)
        {
            string directory = string.IsNullOrEmpty(Program.FieldPath) ? AppDomain.CurrentDomain.BaseDirectory + "\\Model\\" : Program.FieldPath;//FieldPath为自己文件路径


            StreamWriter sr;
            //是否存在文件夹,不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var realPath = Path.Combine(directory, fileType.ToString());

            if (!Directory.Exists(realPath))
            {
                Directory.CreateDirectory(realPath);
            }


            string path = realPath + "\\" + tableName + ".cs";

            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch { }

            sr = File.CreateText(path);
            sr.Write(stringBuilder.ToString());
            sr.Flush();
            sr.Close();
        }

        public static string GenerateModelStringByFileType(FileType fileType, string tableName, List<FieldProperty> fieldPropertys, List<DescriptionModel> descriptionModels)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"using System;\r\n");
            stringBuilder.Append($"using JZFZ.Infrastructure.Data.Att;\r\n");
            stringBuilder.Append($"{ConstInfo.ModelNamespace}  \r\n{{\r\n");

            stringBuilder.Append($"public class {tableName}{fileType.ToString()} \r\n{{\r\n");

            foreach (var item in fieldPropertys)
            {
                stringBuilder.Append($"/// <summary>\r\n");
                stringBuilder.Append($"/// {GetDescription(item, descriptionModels)}\r\n");
                stringBuilder.Append($"/// </summary>\r\n");
                stringBuilder.Append($"  public {GetTypeOfColumn(item.TypeName, item.IsnullAble)} {item.ColumnName} {{get;set;}}\r\n  \r\n");
            }

            stringBuilder.Append("} \r\n");


            stringBuilder.Append("} \r\n");
            return stringBuilder.ToString();
        }




        private static string GetDescription(FieldProperty item, List<DescriptionModel> descriptionModels)
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

        private static string GetTypeOfColumn(string type, string nullAble)
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