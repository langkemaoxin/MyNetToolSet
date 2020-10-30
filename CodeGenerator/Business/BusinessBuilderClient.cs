using System;
using System.Collections.Generic;
using System.IO;
using MyToolSet;

namespace CodeGenerator
{
    public class BusinessBuilderClient
    {
        public static void Execute()
        {
            var tableNames = new List<string>();
            tableNames.Add("DwgPlatformConfig");
            tableNames.Add("DwgMenu");
            tableNames.Add("DwgInfo");
            tableNames.Add("DwgFileFetchScheduleRecord");
            tableNames.Add("DwgFileAnalyticScheduleRecord");

            BusinessFactory.Init("JZFZ.DwgInfoPlatform");

            foreach (var item in tableNames)
            {
                BusinessFactory.Build(item);
            }

            string dir = AppDomain.CurrentDomain.BaseDirectory;

            System.Diagnostics.Process.Start("explorer.exe", dir);
        }
    }
}