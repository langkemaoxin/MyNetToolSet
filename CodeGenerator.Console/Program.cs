using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.console
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var path = @"E:\MyToolSet\MyNetToolSet\CodeGenerator.Simple\bin\Debug\Model\_Entity";

			var list = Directory.GetFiles(path);

			foreach (var file in list)
			{
				var fileName = Path.GetFileName(file).Replace(Path.GetExtension(file), "");

			}
		}
	}
}
