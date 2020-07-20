using System;
using System.Threading.Tasks;


namespace CodeGenerator
{
    class Program
    {
        public static string ConnectionString = "xxxxxx";

        public static string FieldPath = string.Empty;

        //获取所有的数据库名
        //获取所有的表名
        //获取所有的表信息
        //获取表的描述信息

        static void Main(string[] args)
        {
            //BusinessBuilderClient.Execute();

            ModelBuilderClient.Execute();

            string dir = AppDomain.CurrentDomain.BaseDirectory;

            System.Diagnostics.Process.Start("explorer.exe", dir);
        }
    }

}
