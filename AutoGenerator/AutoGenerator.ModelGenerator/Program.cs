using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyToolSet;


namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<string>
            {
                "DwgPlatformConfig",
                "DwgMenu",
                "DwgInfo",
                "DwgFileFetchScheduleRecord",
                "DwgFileAnalyticScheduleRecord"
            };
            var postFixs = new List<string>
            {
                "Entity",
                "RequestModel",
                "ResponseModel"
            };

            new ModelBuilderFactory()
                .SetConnectionString(FileHelperExtension.ReadSingleLineFromFile("C:\\jzcadstr.txt"))
                .SetNameSpace("JZFZ.Platform.Dwginfo")
                .SetTableNames(list)
                .SetPostFixs(postFixs)
                .Build();

            string dir = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start("explorer.exe", dir);
        }
    }

}
