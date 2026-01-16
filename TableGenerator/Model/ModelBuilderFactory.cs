using System.Collections.Generic;

namespace CodeGenerator
{
	public class ModelBuilderFactory
	{
		private string namespaceStr;

		private List<string> tableNames;
		private List<string> postFixs;

		private string connectionString;
	private string outputPath;
	private IOutputWriter outputWriter;
		private ILogger logger = new ConsoleLogger();

		public ModelBuilderFactory SetConnectionString(string connectionString)
		{
			this.connectionString = connectionString;
			return this;
		}

		public ModelBuilderFactory SetNameSpace(string namespaceStr)
		{
			this.namespaceStr = namespaceStr;
			return this;
		}

		public ModelBuilderFactory SetTableNames(List<string> tableNames)
		{
			this.tableNames = tableNames;
			return this;
		}

		public ModelBuilderFactory SetPostFixs(List<string> postFixs)
		{
			this.postFixs = postFixs;
			return this;
		}

		public ModelBuilderFactory SetOutputPath(string outputPath)
		{
			this.outputPath = outputPath;
			return this;
		}

		public ModelBuilderFactory SetOutputWriter(IOutputWriter writer)
		{
			this.outputWriter = writer;
			return this;
		}

		public void Build()
		{
			if (string.IsNullOrWhiteSpace(outputPath))
			{
				throw new System.InvalidOperationException("OutputPath is required");
			}
			if (outputWriter == null)
			{
				outputWriter = new Consitence(outputPath);
			}

			foreach (var tableName in tableNames)
			{
				logger.Info($"Generating models for table: {tableName}");

				FieldPropertyBuilder fieldPropertyBuilder = new FieldPropertyBuilder(tableName, connectionString);

				DescriptionBuilder commentBuilder = new DescriptionBuilder(tableName, connectionString);

				List<FieldProperty> fieldPropertys = SimpleCache.GetOrAdd($"field:{connectionString}:{tableName}", () => fieldPropertyBuilder.Build());

				List<DescriptionModel> descriptionModels = SimpleCache.GetOrAdd($"desc:{connectionString}:{tableName}", () => commentBuilder.Build());

				ModelBuilder modelBuilder = new ModelBuilder(tableName, namespaceStr, fieldPropertys, descriptionModels);

				foreach (var postFix in postFixs)
				{
					string modelStr = modelBuilder.BuildModelWithPostFix(postFix);
					outputWriter.Write(postFix, modelBuilder.TableName, modelStr);
				}
			}
		}
	}
}
