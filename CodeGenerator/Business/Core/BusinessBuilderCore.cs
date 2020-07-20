using System;
using MyToolSet;

namespace CodeGenerator
{
    public class BusinessBuilderCore
    {
        private static string _projectName;
        public static void SetProjectName(string projectName)
        {
            _projectName = projectName;
        }

        public static string BuildAll(string template, string name)
        {
            //设置项目名称
            template = template.Replace("$$PROJECTNAME$$", _projectName);

            //设置表名
            template = template.Replace("$$TABLENAME$$", name);


            template = template.Replace("$$PARAMETERNAME$$", name.StrFirstCharToLower());

            template = template.Replace("$$", "\"");

            return template.ToString();
        }
    }
}