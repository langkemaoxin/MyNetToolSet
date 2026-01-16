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
			var tableNames = new List<string>
		{
						"Project",
				"SystemConfig",
				"SpecialtyMember",
				"SpecialtyManager",
				"Specialty",
				"SpecialtyDeputyManager",
		};
			
			// 模板字符串
			string template = @"Hello {{ name }}! Today is {{ date }}.";

			// 解析模板
			var parsedTemplate = Template.Parse(template);

			// 渲染模板
			var result = parsedTemplate.Render(new
			{
				name = "World",
				date = DateTime.Now.ToString("yyyy-MM-dd")
			});

			Console.WriteLine(result);
			// 输出: Hello World! Today is 2024-01-15.

			CodeGeneratorTools.Generate();
		}
	}
}
