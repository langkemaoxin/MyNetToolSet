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
                "Element_Scheme",
                "Element_Scheme_Article",
                "Element_Scheme_Article_Category",
                "Element_Scheme_Category",
                "Element_Scheme_Category_Link"
            };

            var postFixs = new List<string>
            {
                "Entity"
            };

            new ModelBuilderFactory()
                .SetConnectionString(FileHelperExtension.ReadSingleLineFromFile("C:\\jzcadstr.txt"))
                .SetNameSpace("JZFZ.Projectmanager.Element.Domain")
                .SetTableNames(list)
                .SetPostFixs(postFixs)
                .Build();

            string dir = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start("explorer.exe", dir);
        }
    }

}
