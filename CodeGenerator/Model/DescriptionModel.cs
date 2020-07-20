namespace CodeGenerator
{
    public class DescriptionModel
    {
        public string ColumnName { get; set; }
        public string ColumnDescript { get; set; }

        public DescriptionModel(string columnName, string columnDescript)
        {
            ColumnName = columnName;
            ColumnDescript = columnDescript;
        }
    }
}