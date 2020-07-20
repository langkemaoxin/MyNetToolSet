using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToolSet
{
    public class DirectoryHelper
    {
        public static  void Createdir(string filefullpath)
        {
            bool bexistfile = false;
            if (File.Exists(filefullpath))
            {
                bexistfile = true;
            }
            else //判断路径中的文件夹是否存在
            { 
                string[] pathes = filefullpath.Split('\\');
                if (pathes.Length > 1)
                {
                    string path = pathes[0];
                    for (int i = 1; i < pathes.Length; i++)
                    {
                        path += "\\" + pathes[i];
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                    }
                }
            }
        } 
    }
}
