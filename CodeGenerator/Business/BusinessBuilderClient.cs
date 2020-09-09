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
            var tables = new List<string>()
            {
                "PreBindMenu",
            };

            BusinessFactory.Init("JZFZ.Projectmanager");

            foreach (var item in tables)
            {
                BusinessFactory.Build(item);
            }

            string dir = AppDomain.CurrentDomain.BaseDirectory;

            System.Diagnostics.Process.Start("explorer.exe", dir);
        }
    }
}