namespace CodeGenerator
{
    public class FieldProperty
    {
        public string ColumnName { get; set; }
        public string TypeName { get; set; }
        public string IsnullAble { get; set; }

        public FieldProperty(string columnName, string typeName, string isnullAble)
        {
            ColumnName = columnName;
            TypeName = typeName;
            IsnullAble = isnullAble;
        }
    }
}