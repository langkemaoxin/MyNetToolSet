using System;
using System.IO;

namespace CodeGenerator
{
	public class Consitence
	{
		private readonly string fileType;
		private readonly string tableName;
		private readonly string outputRoot;

		public Consitence(string fileType, string tableName, string outputRoot)
		{
			this.fileType = fileType;
			this.tableName = tableName;
			this.outputRoot = outputRoot;
		}

		public void FlushToDisk(string modelContent)
		{
			string directory = string.IsNullOrWhiteSpace(outputRoot) ? AppDomain.CurrentDomain.BaseDirectory + "\\Model\\" : outputRoot;

			//是否存在文件夹,不存在则创建
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			var realPath = Path.Combine(directory, fileType);

			if (!Directory.Exists(realPath))
			{
				Directory.CreateDirectory(realPath);
			}

			var path = $"{realPath}\\{tableName}{fileType}.cs";

			try
			{
				if (File.Exists(path)) File.Delete(path);
			}
			catch { }

			StreamWriter sr = File.CreateText(path);
			sr.Write(modelContent.ToString());
			sr.Flush();
			sr.Close();
		}

	}
}
