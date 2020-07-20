using System;
using System.IO;
using MyToolSet;

namespace CodeGenerator
{
    public class BusinessFactory
    {
        public static void Init(string projectName)
        {
            BusinessBuilderCore.SetProjectName(projectName);
        }

        public static void Build(string tableName)
        {
            CreateFile("Rep\\Interface", RepInterface(tableName), RepInterfaceBuilder.BuildRepInterface(tableName));

            CreateFile("Rep\\Implement", RepImplement(tableName), RepImplementBuilder.BuildRepImplement(tableName));

            CreateFile("Service\\Interface", ServiceInterface(tableName), ServiceInterfaceBuilder.BuildServiceInterface(tableName));

            CreateFile("Service\\Implement", ServiceImplement(tableName), ServiceImplementBuilder.BuildServiceImplement(tableName));

            CreateFile("Controller", Controller(tableName), ControllerBuilder.BuildController(tableName));
        }

 


        private static string RepInterface(string tableName) => $"I{tableName}Rep.cs";
        private static string RepImplement(string tableName) => $"{tableName}Rep.cs";
        private static string ServiceInterface(string tableName) => $"I{tableName}Service.cs";
        private static string ServiceImplement(string tableName) => $"{tableName}Service.cs";
        private static string Controller(string tableName) => $"{tableName}Controller.cs";
        private static void CreateFile(string filePath, string fileName, string content)
        {
            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,filePath);
            DirectoryHelper.Createdir(basePath);

            var fileFullPath = Path.Combine(basePath, fileName);

            if (File.Exists(fileFullPath)) File.Delete(fileFullPath);

            FileHelperExtension.AppendToFile(fileFullPath, content);
        }
    }
}