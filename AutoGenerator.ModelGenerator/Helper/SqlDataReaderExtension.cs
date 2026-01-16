using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodeGenerator
{
    public static class SqlDataReaderExtension
    {
        public static List<DescriptionModel> ToDescriptionInfo(this SqlDataReader reader)
        {
            List<DescriptionModel> result = new List<DescriptionModel>();

            if (reader == null || reader.IsClosed) return result;

            while (reader.Read())
            {
                var item = new DescriptionModel(reader["columnName"].ToString(), reader["columnDescript"].ToString());
                result.Add(item);
            }
            return result;
        }

        public static List<FieldProperty> ToTableModel(this SqlDataReader reader)
        {
            List<FieldProperty> result = new List<FieldProperty>();

            if (reader == null || reader.IsClosed) return result;

            while (reader.Read())
            {
                var item = new FieldProperty(reader["columnName"].ToString(), reader["typeName"].ToString(), reader["isnullAble"].ToString());
                result.Add(item);
            }
            return result;
        }
    }
}