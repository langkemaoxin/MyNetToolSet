using MyToolSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tables = new List<string>() { };
            tables.Add("CMDRecord");
            tables.Add("CMDRecordZY");
            tables.Add("FileEditingTime");
            tables.Add("OpenRecord");
            tables.Add("PrjEvaluate");

            File.Delete("Model.txt");
            File.Delete("Rep.txt");
            File.Delete("Service.txt");
            File.Delete("Controller.txt");

            foreach (var item in tables)
            {

                CommonFileHelper.AppendToFile("Model.txt", BuildModel(item));
                CommonFileHelper.AppendToFile("Rep.txt", BuildRep(item));
                CommonFileHelper.AppendToFile("Service.txt", BuildService(item));
                CommonFileHelper.AppendToFile("Controller.txt", BuildController(item));
            }

            string dir = AppDomain.CurrentDomain.BaseDirectory;

            System.Diagnostics.Process.Start("explorer.exe", dir);
        }

        public static string BuildModel(string name)
        {
            var template = @" 
            [Table(##$$TABLENAME$$##)]
            public class $$TABLENAME$$Entity
            {
                [Key]
                /// <summary>
                /// 自增主键
                /// </summary>
                public int Id { get; set; }
            }
 
            public class $$TABLENAME$$Model
            { 

            }";

            return BuildAll(template, name);
        }

        public static string BuildRep(string name)
        {
            var template = @" 
           
public interface I$$TABLENAME$$Rep
{ 
    $$TABLENAME$$Entity Add($$TABLENAME$$Model model);
             
    $$TABLENAME$$Entity Get($$TABLENAME$$Model model);
             
    $$TABLENAME$$Entity Update($$TABLENAME$$Model model);
}

public class $$TABLENAME$$Rep : AbstractRepository<$$TABLENAME$$Entity>, I$$TABLENAME$$Rep
{
        public $$TABLENAME$$Rep(IConnectionConfig connectionConfig)
        {
            SetDbType(DbType.SqlServer);
            ConnectionString = connectionConfig.GetJZCADConnection();
            SlaveConnectionString = connectionConfig.GetJZCADConnection();
        }
         
        public $$TABLENAME$$Entity Add($$TABLENAME$$Model model)
        {
            var entity = Get(model);
            if (entity != null) return entity;

            var addItem = new $$TABLENAME$$Entity()
            {
          
            };
            Insert(addItem);

            return addItem;
        }

         
        public $$TABLENAME$$Entity Get($$TABLENAME$$Model model)
        {
          return null;
        }


        public $$TABLENAME$$Entity Update($$TABLENAME$$Model model)
        {
            var entity = Get(model);

            if (entity == null) return null;

            Update(entity);

            return entity;
        }
    }";
            return BuildAll(template, name);
        }

        public static string BuildService(string name)
        {
            var template = @" 
            
  public class $$TABLENAME$$Service : I$$TABLENAME$$Service
    {
        private readonly I$$TABLENAME$$Rep $$PARAMETERNAME$$Rep;

        public $$TABLENAME$$Service(I$$TABLENAME$$Rep $$PARAMETERNAME$$Rep)
        {
            this.$$PARAMETERNAME$$Rep = $$PARAMETERNAME$$Rep;
        }

        public ResultJsonInfo<$$TABLENAME$$Entity> Add($$TABLENAME$$Model model)
        {
            var result = $$PARAMETERNAME$$Rep.Add(model);
            return ResultJsonInfo<$$TABLENAME$$Entity>.GetSucceedObject(result);
        }

        public ResultJsonInfo<$$TABLENAME$$Entity> Get($$TABLENAME$$Model model)
        {
            var result = $$PARAMETERNAME$$Rep.Get(model);
            return ResultJsonInfo<$$TABLENAME$$Entity>.GetSucceedObject(result);
        }

        public ResultJsonInfo<$$TABLENAME$$Entity> Update($$TABLENAME$$Model model)
        {
            var result = $$PARAMETERNAME$$Rep.Update(model);
            return ResultJsonInfo<$$TABLENAME$$Entity>.GetSucceedObject(result);
        }
    }


  public interface I$$TABLENAME$$Service
    {
      
        ResultJsonInfo<$$TABLENAME$$Entity> Add($$TABLENAME$$Model model);

  
        ResultJsonInfo<$$TABLENAME$$Entity> Get($$TABLENAME$$Model model);

  
        ResultJsonInfo<$$TABLENAME$$Entity> Update($$TABLENAME$$Model model);
    }


          ";
            return BuildAll(template, name);
        }

        public static string BuildController(string name)
        {
            var template = @"
 
    [ApiController]
    [Route(##[controller]##)]
    public class $$TABLENAME$$Controller
    {
        private readonly I$$TABLENAME$$Service $$PARAMETERNAME$$Service;

        public $$TABLENAME$$Controller(I$$TABLENAME$$Service $$PARAMETERNAME$$Service)
        {
            this.$$PARAMETERNAME$$Service = $$PARAMETERNAME$$Service;
        }
 
 
        [HttpPost]
        [Route(##Add##)]
        public ResultJsonInfo<$$TABLENAME$$Entity> Add($$TABLENAME$$Model model)
        {
            return $$PARAMETERNAME$$Service.Add(model);
        }

 
        [HttpPost]
        [Route(##Get##)]
        public ResultJsonInfo<$$TABLENAME$$Entity> Get($$TABLENAME$$Model model)
        {
            return $$PARAMETERNAME$$Service.Get(model);
        }

 
        [HttpPost]
        [Route(##Update##)]
        public ResultJsonInfo<$$TABLENAME$$Entity> Update($$TABLENAME$$Model model)
        {
            return $$PARAMETERNAME$$Service.Update(model);
        }
    }
";

            return BuildAll(template, name);
        }

        public static string BuildAll(string template, string name)
        {

            template = template.Replace("$$TABLENAME$$", name);

            template = template.Replace("$$PARAMETERNAME$$", name.StrFirstCharToLower());

            return template.ToString();
        }

    }
     
}
