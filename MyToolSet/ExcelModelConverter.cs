using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MyToolSet
{
    /// <summary>
    /// Excel模型转换器
    ///
    /// <remarks>
    ///
    ///     //传入文件路径，
    ///     var models = ExcelModelConverter.ReadFromExcelFile<ModelName>(path);
    /// 
    /// </remarks>
    /// </summary>
    public class ExcelModelConverter
    {
        /// <summary>
        /// 泛型：从Excel文件中转变成模型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="isSkip"></param>
        /// <returns></returns>
        public static List<T> ReadFromExcelFile<T>(string filePath, bool isSkip = true) where T : new()
        {
            var list = new List<T>();

            try
            {
                var wk = CreateWorkbook<T>(filePath);

                IsValidate<T>(wk);

                list = ModelListGenerator<T>(isSkip, wk);
            }
            catch (Exception e)
            {
                //只在Debug模式下才输出
                Console.WriteLine(e.Message);
            }
            return list;
        }

        /// <summary>
        /// 模型列表构建器 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isSkip"></param>
        /// <param name="wk"></param>
        /// <returns></returns>
        private static List<T> ModelListGenerator<T>(bool isSkip, IWorkbook wk) where T : new()
        {
            var sheet = wk.GetSheetAt(0);

            List<T> list = new List<T>();
            for (var rowNum = 0; rowNum <= sheet.LastRowNum; rowNum++)
            {
                var row = sheet.GetRow(rowNum);
                if (row == null) continue;

                if (rowNum == 0 && isSkip)
                {
                    continue;
                }

                var item = new T();

                var propertyInfos = typeof(T).GetProperties();

                //LastCellNum 是当前行的总列数
                for (var cellNum = 0; cellNum < row.LastCellNum; cellNum++)
                {


                    try
                    {
                        var value = row.GetCell(cellNum).ToString();
                        propertyInfos[cellNum].SetValue(item, value);
                    }
                    catch (Exception exception)
                    { }
                }

                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 根据文件创建工作簿对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static  IWorkbook CreateWorkbook<T>(string filePath) where T : new()
        {
            IWorkbook wk;
            var allowExtensions = new List<string>() { ".xlsx", ".xls" };
            var extension = Path.GetExtension(filePath);

            if (!allowExtensions.Contains(extension, StringComparer.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("只支持xlsx,xls 类型文件");
            }

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(filePath);
                if (extension != null && extension.Equals(".xls"))
                {
                    wk = new HSSFWorkbook(fs);
                }
                else
                {
                    wk = new XSSFWorkbook(fs);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                fs?.Close();
            }

            return wk;
        }

        /// <summary>
        /// 验证工作簿和模型数量是否一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wk"></param>
        private static  void IsValidate<T>(IWorkbook wk) where T : new()
        {
            //读取当前表数据
            var sheet = wk.GetSheetAt(0);

            if (sheet == null) { throw new ArgumentException("当前表单读取失败"); }

            var firstRow = sheet.GetRow(0);
            if (firstRow == null)
            {
                throw new ArgumentException("当前表单没有数据");
            }

            //表单的列数
            var cellCount = firstRow.LastCellNum;

            //模型的属性数
            var propertiesCount = typeof(T).GetProperties().Length;
            if (cellCount != propertiesCount)
            {
                throw new ArgumentException($"表单列数为{cellCount},模型属性数为：{propertiesCount}");
            }
        }
    }
}