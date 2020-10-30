using System.Collections.Generic;

namespace CodeGenerator
{
    public class ModelBuilderFactory
    {
        private string namespaceStr;

        private List<string> tableNames;
        private List<string> postFixs;

        private string connectionString;

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

        public  void Build()
        {
            foreach (var tableName in tableNames)
            {
                CommentBuilder commentBuilder = new CommentBuilder(tableName, connectionString);
                FieldPropertyBuilder fieldPropertyBuilder = new FieldPropertyBuilder(tableName, connectionString);
                ModelBuilder modelBuilder = new ModelBuilder(tableName, namespaceStr, fieldPropertyBuilder.Build(), commentBuilder.Build());

                foreach (var postFix in postFixs)
                {
                    string modelStr = modelBuilder.BuildModelWithPostFix(postFix);
                    Consitence consitence = new Consitence(postFix, tableName);
                    consitence.FlushToDisk(modelStr);
                }
            }
        }
    }
}