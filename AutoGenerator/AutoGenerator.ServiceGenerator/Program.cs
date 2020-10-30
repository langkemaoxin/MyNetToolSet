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
                "DwgPlatformConfig",
                "DwgMenu",
                "DwgInfo",
                "DwgFileFetchScheduleRecord",
                "DwgFileAnalyticScheduleRecord"
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
