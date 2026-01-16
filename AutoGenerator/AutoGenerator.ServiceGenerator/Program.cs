using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGenerator.RepositoryGenerator
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

          new ServiceClassFactory()
              .SetNameSpace("JZFZ.DwgInfoPlatform.Business")
              .SetTableNames(list)
              .Build();

          string dir = AppDomain.CurrentDomain.BaseDirectory;
          System.Diagnostics.Process.Start("explorer.exe", dir);
        }
    }
}
