namespace CodeGenerator
{
    public interface IOutputWriter
    {
        void Write(string fileType, string tableName, string content);
    }
}
