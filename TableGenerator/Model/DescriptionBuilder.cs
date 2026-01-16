using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CodeGenerator
{
	public class DescriptionBuilder
	{
		private readonly string _tableName = string.Empty;
		private readonly string _tableCatalog = string.Empty;
		private readonly string _tableScheme = string.Empty;

		private readonly string connectionString;

		public DescriptionBuilder(string tableName, string connectionString)
		{
			if (tableName.Contains(".dbo."))
			{
				var infoArray = tableName.Split('.').ToList();
				if (infoArray.Count < 3) throw new ArgumentException($"传入的表名不正确:{tableName}");

				this._tableCatalog = infoArray[0];
				this._tableScheme = infoArray[1];
				this._tableName = infoArray[2];
			}
			else
			{
				this._tableName = tableName;
			}

			this.connectionString = connectionString.Replace("##DataBase##", _tableCatalog);
		}

		/// <summary>
		/// 获取字段注释
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public List<DescriptionModel> Build()
		{
			string sql = $"{ConstInfo.GetDescriptionSql}  AND b.name='{_tableName}'";

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