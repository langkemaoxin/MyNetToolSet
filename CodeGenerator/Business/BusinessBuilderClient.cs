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
			var tableNames = new List<string>
			{
				 "T_CP_TaskNoticeMajor",
				 "T_CP_TaskNoticeRole",
				 "T_CP_TaskNotice",
				 "S_W_RBS_ToXT",
				 "ProjectUser",
				 "V_I_ProjectInfo",
				 "S_A_User",
				 "S_A_Org"
			};

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