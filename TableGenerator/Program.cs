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
				"Project",
				"SystemConfig",
				"SpecialtyMember",
				"SpecialtyManager",
				"Specialty",
				"SpecialtyDeputyManager"
			};

			var postFixs = new List<string>
			{
				"_Entity",
			};

			var outputPath = "C:\\GeneratedRepositories";

			var writer = new Consitence(outputPath);

			new ModelBuilderFactory()
				.SetConnectionString(FileHelperExtension.ReadSingleLineFromFile("C:\\jzcadstr.txt"))
				.SetNameSpace("JZFZ.Platform.Dwginfo")
				.SetTableNames(list)
				.SetPostFixs(postFixs)
				.SetOutputPath(outputPath)
				.SetOutputWriter(writer)
				.Build();

			System.Diagnostics.Process.Start("explorer.exe", outputPath);
		}
	}

}
