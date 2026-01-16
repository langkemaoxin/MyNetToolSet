// 使用Scriban模板引擎
using Scriban;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CodeGenerator
{
    /// <summary>
    /// 代码生成器构建器，支持链式配置和批量代码生成
    /// </summary>
    public class CodeGeneratorTools : IDisposable
    {
        private readonly object _lock = new object();
        private string _template;
        private readonly List<TableItem> _tables;
        private string _outputPath;
        private string _fileNamePattern;
        private Func<object, string> _fileNamer;
        private bool _disposed;

        /// <summary>
        /// 数据表信息
        /// </summary>
        public class TableInfo
        {
            /// <summary>
            /// 表名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 表描述
            /// </summary>
            public string Description { get; set; }
        }

        /// <summary>
        /// 代码生成结果
        /// </summary>
        public class GenerationResult
        {
            /// <summary>
            /// 生成的文件数量
            /// </summary>
            public int FileCount { get; set; }

            /// <summary>
            /// 生成的文件路径列表
            /// </summary>
            public List<string> GeneratedFiles { get; set; } = new List<string>();

            /// <summary>
            /// 是否成功
            /// </summary>
            public bool Success { get; set; }

            /// <summary>
            /// 错误信息
            /// </summary>
            public string ErrorMessage { get; set; }
        }

        /// <summary>
        /// 私有构造函数，防止直接实例化
        /// </summary>
        private CodeGeneratorTools()
        {
            _tables = new List<TableItem>();
        }

        /// <summary>
        /// 创建代码生成器构建器实例
        /// </summary>
        /// <returns>代码生成器构建器实例</returns>
        public static CodeGeneratorTools Create()
        {
            return new CodeGeneratorTools();
        }

        /// <summary>
        /// 设置代码生成模板
        /// </summary>
        /// <param name="template">模板内容</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentException">当模板为空或null时抛出</exception>
        public CodeGeneratorTools WithTemplateOrFilePath(string template)
        {
            if (string.IsNullOrWhiteSpace(template))
            {
                throw new ArgumentException("模板不能为空或null", nameof(template));
            }

            lock (_lock)
            {
                _template = File.Exists(template) ? File.ReadAllText(template) : template;
            }

            return this;
        }

        /// <summary>
        /// 设置模板（别名）：支持传入模板字符串或模板文件路径。
        /// </summary>
        /// <param name="template">模板内容或文件路径</param>
        /// <returns>当前构建器实例</returns>
        public CodeGeneratorTools WithTemplate(string template) => WithTemplateOrFilePath(template);
         

        /// <summary>
        /// 添加单个数据表配置并指定生成文件名（强制覆盖命名策略）。
        /// </summary>
        /// <param name="table">数据表信息</param>
        /// <param name="fileName">生成文件名（可带扩展名，不可包含路径）</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentNullException">当表信息为null时抛出</exception>
        /// <exception cref="ArgumentException">当文件名为空时抛出</exception>
        public CodeGeneratorTools WithTable(TableInfo table, string fileName)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("文件名不能为空", nameof(fileName));

            lock (_lock)
            {
                _tables.Add(new TableItem { Data = table, FileName = fileName });
            }

            return this;
        }
         

        /// <summary>
        /// 添加单个数据表配置（动态对象）并指定生成文件名（强制覆盖命名策略）。
        /// </summary>
        /// <param name="table">任意具有公共属性的对象（匿名对象、字典、ExpandoObject 等）。</param>
        /// <param name="fileName">生成文件名（可带扩展名，不可包含路径）</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentNullException">当表信息为null时抛出</exception>
        /// <exception cref="ArgumentException">当文件名为空时抛出</exception>
        public CodeGeneratorTools WithTable(object table, string fileName)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("文件名不能为空", nameof(fileName));

            lock (_lock)
            {
                _tables.Add(new TableItem { Data = table, FileName = fileName });
            }

            return this;
        }

        /// <summary>
        /// 批量添加多个数据表配置（动态对象版本）。
        /// </summary>
        /// <param name="tables">任意对象集合。</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentNullException">当表集合为null时抛出</exception>
        public CodeGeneratorTools WithTables(IEnumerable<object> tables)
        {
            if (tables == null)
            {
                throw new ArgumentNullException(nameof(tables));
            }

            lock (_lock)
            {
                foreach (var t in tables)
                {
                    _tables.Add(new TableItem { Data = t });
                }
            }

            return this;
        }

        /// <summary>
        /// 设置输出文件路径
        /// </summary>
        /// <param name="outputPath">输出路径</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentException">当路径为空或null时抛出</exception>
        public CodeGeneratorTools WithOutputPath(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                throw new ArgumentException("输出路径不能为空或null", nameof(outputPath));
            }

            lock (_lock)
            {
                _outputPath = outputPath;
            }

            return this;
        }
 
        /// <summary>
        /// 执行代码生成
        /// </summary>
        /// <returns>代码生成结果</returns>
        /// <exception cref="InvalidOperationException">当配置不完整时抛出</exception>
        public GenerationResult Build()
        {
            lock (_lock)
            {
                // 参数验证
                if (string.IsNullOrWhiteSpace(_template))
                {
                    throw new InvalidOperationException("模板未设置，请先调用WithTemplate方法设置模板");
                }

                if (_tables.Count == 0)
                {
                    throw new InvalidOperationException("数据表配置为空，请先调用WithTable或WithTables方法添加数据表");
                }

                if (string.IsNullOrWhiteSpace(_outputPath))
                {
                    throw new InvalidOperationException("输出路径未设置，请先调用WithOutputPath方法设置输出路径");
                }

                var result = new GenerationResult();

                try
                {
                    // 确保输出目录存在
                    Directory.CreateDirectory(_outputPath);

                    // 解析模板
                    var parsedTemplate = Template.Parse(_template);
                    Template fileNameTemplate = null; 

                    if (parsedTemplate.HasErrors)
                    {
                        result.Success = false;
                        result.ErrorMessage = "模板解析失败: " + string.Join(", ", parsedTemplate.Messages.Select(m => m.Message));
                        return result;
                    }

                    // 生成代码文件 
                    foreach (var item in _tables)
                    {
                        var context = new TemplateContext();

                        var script = new ScriptObject();

                        ImportDynamic(item.Data, script);

                        context.PushGlobal(script);

                        var generatedCode = parsedTemplate.Render(context);

                        // 计算文件名：优先使用文件名模板，其次命名委托，最后回退策略
                        string fileName = item.FileName;

						fileName = string.IsNullOrWhiteSpace(Path.GetExtension(fileName)) ? fileName + ".cs" : fileName;
                        fileName = Path.GetFileName(fileName);
                        var filePath = Path.Combine(_outputPath, fileName);

                        File.WriteAllText(filePath, generatedCode, Encoding.UTF8);

                        result.GeneratedFiles.Add(filePath);

                        context.PopGlobal();
                    }

                    result.FileCount = result.GeneratedFiles.Count;
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.ErrorMessage = $"代码生成失败: {ex.Message}";
                }

                return result;
            }
        }

        /// <summary>
        /// 获取表对象中的名称属性值，支持 name/Name 以及字典、ExpandoObject。
        /// </summary>
        /// <param name="table">表对象</param>
        /// <returns>名称字符串，不可为空则回退为"Unknown"</returns>
        private static string GetNameValue(object table)
        {
            if (table == null) return "Unknown";

            var type = table.GetType();
            var prop = type.GetProperty("name") ?? type.GetProperty("Name");
            var value = prop?.GetValue(table)?.ToString();

            if (string.IsNullOrWhiteSpace(value) && table is System.Collections.Generic.IDictionary<string, object> dict)
            {
                if (dict.TryGetValue("name", out var v) || dict.TryGetValue("Name", out v))
                {
                    value = v?.ToString();
                }
            }

            return string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        private static void ImportDynamic(object source, ScriptObject script)
        {
            if (source is System.Collections.Generic.IDictionary<string, object> dict)
            {
                foreach (var kv in dict)
                {
                    var key = kv.Key ?? string.Empty;
                    script.SetValue(key, kv.Value, true);
                    script.SetValue(key.ToLowerInvariant(), kv.Value, true);
                }
                return;
            }

            var props = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in props)
            {
                var name = p.Name;
                var val = p.GetValue(source);
                script.SetValue(name, val, true);
                script.SetValue(name.ToLowerInvariant(), val, true);
            }
        }

        private class TableItem
        {
            public object Data { get; set; }
            public string FileName { get; set; }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // 清理托管资源
                    _tables?.Clear();
                }

                _disposed = true;
            }
        }
    }
}
