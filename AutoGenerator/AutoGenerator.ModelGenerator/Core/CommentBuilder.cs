using System.Collections.Generic;
using System.Data.SqlClient;

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
}