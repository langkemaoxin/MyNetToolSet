namespace CodeGenerator
{
    public class ConstInfo
    {
        public static string GetAllDataSql = "SELECT NAME FROM MASTER.DBO.SYSDATABASES ORDER BY NAME";
        public static string GetAllTableSql = "SELECT name FROM sys.tables where type ='U'";

        public static string GetTableInfoSql = @"SELECT DISTINCT a.COLUMN_NAME columnName, 
                                          a.DATA_TYPE typeName, a.IS_NULLABLE isnullAble
                                          From INFORMATION_SCHEMA.Columns a LEFT JOIN 
                                          INFORMATION_SCHEMA.KEY_COLUMN_USAGE b ON a.TABLE_NAME=b.TABLE_NAME ";

        public static string GetDescriptionSql = @"SELECT c.name as columnName, a.VALUE as columnDescript
            FROM  SYS.EXTENDED_PROPERTIES a,SYSOBJECTS b,SYS.COLUMNS c
            WHERE a.major_id = b.id  AND c.object_id = b.id AND c.column_id = a.minor_id";

        public static string EntityNamespace = string.Empty;
        public static string ModelNamespace = string.Empty;
    }
}