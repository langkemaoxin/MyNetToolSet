﻿using System;
using System.IO;

namespace CodeGenerator
{
    public class Consitence : IOutputWriter
	{
        private readonly string outputRoot;

        public Consitence(string outputRoot)
		{
			this.outputRoot = outputRoot;
		}

        public void Write(string fileType, string tableName, string content)
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
            sr.Write(content);
			sr.Flush();
			sr.Close();
		}
	}
}
