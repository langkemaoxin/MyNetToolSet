// 使用Scriban模板引擎
using Scriban;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    /// <summary>
    /// 代码生成器构建器，支持链式配置和批量代码生成
    /// </summary>
    public class CodeGeneratorTools : IDisposable
    {
        private readonly object _lock = new object();
        private string _template;
        private readonly List<object> _tables;
        private string _outputPath;
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
            _tables = new List<object>();
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
        public CodeGeneratorTools WithTemplate(string template)
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
        /// 添加单个数据表配置
        /// </summary>
        /// <param name="table">数据表信息</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentNullException">当表信息为null时抛出</exception>
        public CodeGeneratorTools WithTable(TableInfo table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            lock (_lock)
            {
                _tables.Add(table);
            }

            return this;
        }

        /// <summary>
        /// 添加单个数据表配置（动态对象版本）。
        /// 支持使用 <c>new { Name = "Users", Description = "用户表" }</c> 等匿名对象。
        /// </summary>
        /// <param name="table">任意具有公共属性的对象（匿名对象、字典、ExpandoObject 等）。</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentNullException">当表信息为null时抛出</exception>
        public CodeGeneratorTools WithTable(object table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            lock (_lock)
            {
                _tables.Add(table);
            }

            return this;
        }

        /// <summary>
        /// 批量添加多个数据表配置
        /// </summary>
        /// <param name="tables">数据表信息集合</param>
        /// <returns>当前构建器实例</returns>
        /// <exception cref="ArgumentNullException">当表集合为null时抛出</exception>
        public CodeGeneratorTools WithTables(IEnumerable<TableInfo> tables)
        {
            if (tables == null)
            {
                throw new ArgumentNullException(nameof(tables));
            }

            lock (_lock)
            {
                _tables.AddRange(tables);
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
                _tables.AddRange(tables);
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

                    if (parsedTemplate.HasErrors)
                    {
                        result.Success = false;
                        result.ErrorMessage = "模板解析失败: " + string.Join(", ", parsedTemplate.Messages.Select(m => m.Message));
                        return result;
                    }

                    // 生成代码文件
                    foreach (var table in _tables)
                    {
                        var context = new TemplateContext
                        {
                            MemberRenamer = member => member.Name.ToLowerInvariant()
                        };

                        var script = new ScriptObject();
                        script.Import(table, renamer: member => member.Name.ToLowerInvariant());
                        context.PushGlobal(script);

                        var generatedCode = parsedTemplate.Render(context);

                        // 计算文件名：优先取 name/Name 属性
                        string nameValue = GetNameValue(table);
                        var fileName = $"{nameValue}Rep.cs";
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
