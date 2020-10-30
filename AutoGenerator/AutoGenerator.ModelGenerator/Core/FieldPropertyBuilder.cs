using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodeGenerator
{
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
}