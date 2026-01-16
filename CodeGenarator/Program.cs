using Scriban;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scriban;


namespace CodeGenerator.console
{
	class Program
	{
		static void Main(string[] args)
		{
			string template = @"
using JZFZ.Infrastructure.Apollo;
using JZFZ.Infrastructure.Data.DapperExtensions;
using XT.Model.DbModels;

namespace XT.Service.Rep
{
    public class {{ name }}Rep : AbstractRepository<{{ name }}_Entity>
    {
        public {{ name }}Rep()
        {
            SetDbType(DbType.SqlServer);
            ConnectionString = ""xx"".GetApolloConfig();
        }
    }
}";
			
			var result = CodeGeneratorTools.Create()
				.WithTemplateOrFilePath(template)
				.WithTable(new { Name = "Users", Description = "用户表" },"UsersRep.cs")
				.WithTable(new { Name = "Orders", Description = "订单表" }, "Orders.cs")
				.WithOutputPath("C:\\GeneratedRepositories")
				.Build();

			if (result.Success)
			{
				Console.WriteLine($"成功生成 {result.FileCount} 个文件");
				foreach (var file in result.GeneratedFiles)
				{
					Console.WriteLine($"生成文件: {file}");
				}
			}
			else
			{
				Console.WriteLine($"生成失败: {result.ErrorMessage}");
			}
		}
	}
}
