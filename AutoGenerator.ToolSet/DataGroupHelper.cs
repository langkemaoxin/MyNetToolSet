using System;
using System.Collections.Generic;
using System.Linq;

namespace MyToolSet
{
    /// <summary>
    /// 数据分组扩展方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataGroupHelper<T>
    {
        /// <summary>
        /// 拆分成 相同大小的文件
        /// </summary>
        ///
        /// <remarks>
        ///
        ///      //直接
        ///      List<List<string>> groupTaskIds = DataGroupExtension<string>.SplitToSameGroup(Ids, 1000);
        ///
        /// </remarks>
        /// <param name="list"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<T>> SplitToSameGroup(List<T> list, int size)
        {
            if (list == null) throw new ArgumentNullException("list parameter can not be null");

            if (size < 0) throw new ArgumentException("size can not be below zero");

            List<List<T>> arrayList = list.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
            return arrayList;
        }
    }
}