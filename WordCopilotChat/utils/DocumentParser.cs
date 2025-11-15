using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;
using WordCopilotChat.models;
using WordApp = Microsoft.Office.Interop.Word.Application;
using WordDocument = Microsoft.Office.Interop.Word.Document;

namespace WordCopilotChat.utils
{
    /// <summary>
    /// 文档解析工具类
    /// </summary>
    public static class DocumentParser
    {
        /// <summary>
        /// 解析Word文档（.docx, .doc）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="quickMode">快速模式，只提取标题不提取内容</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <returns>解析结果</returns>
        public static DocumentParseResult ParseWordDocument(string filePath, bool quickMode = false, Action<string> progressCallback = null)
        {
            var result = new DocumentParseResult();
            WordApp wordApp = null;
            WordDocument doc = null;

            try
            {
                Debug.WriteLine($"开始解析Word文档: {filePath}");
                progressCallback?.Invoke($"开始解析Word文档: {Path.GetFileName(filePath)}");
                
                // 创建Word应用程序
                Debug.WriteLine("正在创建Word应用程序实例...");
                progressCallback?.Invoke("正在创建Word应用程序实例...");
                wordApp = new WordApp();
                wordApp.Visible = false;
                wordApp.DisplayAlerts = WdAlertLevel.wdAlertsNone; // 禁用警告对话框
                
                Debug.WriteLine("正在打开文档...");
                progressCallback?.Invoke("正在打开文档...");
                doc = wordApp.Documents.Open(filePath, ReadOnly: true); // 以只读方式打开
                
                Debug.WriteLine($"文档已打开，共有 {doc.Paragraphs.Count} 个段落");
                progressCallback?.Invoke($"文档已打开，共有 {doc.Paragraphs.Count} 个段落");

                var headings = new List<DocumentHeading>();
                var headingStack = new Stack<DocumentHeading>(); // 用于跟踪父级标题
                int orderIndex = 0;
                int processedParagraphs = 0;

                Debug.WriteLine("开始扫描段落寻找标题...");
                progressCallback?.Invoke("开始扫描段落寻找标题...");
                foreach (Paragraph para in doc.Paragraphs)
                {
                    processedParagraphs++;
                    
                    // 每处理100个段落输出一次进度
                    if (processedParagraphs % 100 == 0)
                    {
                        var progressMsg = $"已处理 {processedParagraphs}/{doc.Paragraphs.Count} 个段落，找到 {headings.Count} 个标题";
                        Debug.WriteLine(progressMsg);
                        progressCallback?.Invoke(progressMsg);
                    }

                    try
                    {
                        var style = para.get_Style();
                        string styleName = style.NameLocal;

                        // 检查是否为标题样式
                        if (IsHeadingStyle(styleName, out int level))
                        {
                            var headingText = para.Range.Text.Trim().Replace("\r", "");
                            if (!string.IsNullOrWhiteSpace(headingText))
                            {
                                Debug.WriteLine($"找到标题 (H{level}): {headingText}");
                                progressCallback?.Invoke($"找到标题 (H{level}): {headingText}");
                                
                                // 找到父级标题
                                DocumentHeading parentHeading = null;
                                while (headingStack.Count > 0 && headingStack.Peek().HeadingLevel >= level)
                                {
                                    headingStack.Pop();
                                }
                                if (headingStack.Count > 0)
                                {
                                    parentHeading = headingStack.Peek();
                                }

                                var heading = new DocumentHeading
                                {
                                    HeadingText = headingText,
                                    HeadingLevel = level,
                                    ParentHeadingId = parentHeading?.Id,
                                    Content = "", // 内容稍后收集
                                    OrderIndex = orderIndex++
                                };

                                headings.Add(heading);
                                headingStack.Push(heading);
                            }
                        }
                    }
                    catch (Exception paraEx)
                    {
                        Debug.WriteLine($"处理段落 {processedParagraphs} 时出错: {paraEx.Message}");
                        // 继续处理下一个段落
                    }
                }

                Debug.WriteLine($"段落扫描完成，共找到 {headings.Count} 个标题");
                progressCallback?.Invoke($"段落扫描完成，共找到 {headings.Count} 个标题");

                // 收集每个标题下的内容（除非是快速模式）
                if (headings.Count > 0 && !quickMode)
                {
                    Debug.WriteLine("开始收集标题内容...");
                    progressCallback?.Invoke("开始收集标题内容...");
                    CollectWordHeadingContent(doc, headings, progressCallback);
                    Debug.WriteLine("标题内容收集完成");
                    progressCallback?.Invoke("标题内容收集完成");
                }
                else if (quickMode)
                {
                    Debug.WriteLine("快速模式：跳过内容收集");
                    progressCallback?.Invoke("快速模式：跳过内容收集");
                    // 在快速模式下，为所有标题设置空内容
                    foreach (var heading in headings)
                    {
                        heading.Content = "";
                    }
                }

                result.Headings = headings;
                result.Success = true;
                result.Message = $"Word文档解析成功，找到 {headings.Count} 个标题";

                Debug.WriteLine($"Word文档解析完成，共找到 {headings.Count} 个标题");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Word文档解析失败: {ex.Message}";
                Debug.WriteLine($"Word文档解析错误: {ex}");
                Debug.WriteLine($"错误详情: {ex.StackTrace}");
            }
            finally
            {
                // 清理资源
                try
                {
                    Debug.WriteLine("正在清理Word资源...");
                    if (doc != null)
                    {
                        doc.Close(false);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                    }
                    if (wordApp != null)
                    {
                        wordApp.Quit(false);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                    }
                    Debug.WriteLine("Word资源清理完成");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"清理Word资源时出错: {ex.Message}");
                }
                finally
                {
                    // 强制垃圾回收
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                }
            }

            return result;
        }

        /// <summary>
        /// 解析Markdown文档
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>解析结果</returns>
        public static DocumentParseResult ParseMarkdownDocument(string filePath)
        {
            var result = new DocumentParseResult();

            try
            {
                string content = File.ReadAllText(filePath, Encoding.UTF8);
                var headings = new List<DocumentHeading>();
                var lines = content.Split('\n');
                
                var headingStack = new Stack<DocumentHeading>();
                int orderIndex = 0;
                var currentContent = new StringBuilder();
                DocumentHeading currentHeading = null;

                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    
                    // 检查是否为标题行
                    var headingMatch = Regex.Match(trimmedLine, @"^(#{1,6})\s+(.+)$");
                    if (headingMatch.Success)
                    {
                        // 保存之前标题的内容
                        if (currentHeading != null)
                        {
                            currentHeading.Content = currentContent.ToString().Trim();
                        }

                        int level = headingMatch.Groups[1].Value.Length;
                        string headingText = headingMatch.Groups[2].Value.Trim();

                        // 找到父级标题
                        DocumentHeading parentHeading = null;
                        while (headingStack.Count > 0 && headingStack.Peek().HeadingLevel >= level)
                        {
                            headingStack.Pop();
                        }
                        if (headingStack.Count > 0)
                        {
                            parentHeading = headingStack.Peek();
                        }

                        currentHeading = new DocumentHeading
                        {
                            HeadingText = headingText,
                            HeadingLevel = level,
                            ParentHeadingId = parentHeading?.Id,
                            Content = "",
                            OrderIndex = orderIndex++
                        };

                        headings.Add(currentHeading);
                        headingStack.Push(currentHeading);
                        currentContent.Clear();
                    }
                    else
                    {
                        // 收集内容
                        if (currentHeading != null && !string.IsNullOrWhiteSpace(trimmedLine))
                        {
                            currentContent.AppendLine(line);
                        }
                    }
                }

                // 保存最后一个标题的内容
                if (currentHeading != null)
                {
                    currentHeading.Content = currentContent.ToString().Trim();
                }

                result.Headings = headings;
                result.Success = true;
                result.Message = "Markdown文档解析成功";

                Debug.WriteLine($"Markdown文档解析完成，共找到 {headings.Count} 个标题");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Markdown文档解析失败: {ex.Message}";
                Debug.WriteLine($"Markdown文档解析错误: {ex}");
            }

            return result;
        }

        /// <summary>
        /// 检查样式是否为标题样式
        /// </summary>
        private static bool IsHeadingStyle(string styleName, out int level)
        {
            level = 0;
            if (string.IsNullOrEmpty(styleName)) return false;

            // 中文标题样式
            if (styleName.Contains("标题"))
            {
                var match = Regex.Match(styleName, @"标题\s*(\d+)");
                if (match.Success && int.TryParse(match.Groups[1].Value, out level))
                {
                    return level >= 1 && level <= 6;
                }
            }

            // 英文标题样式
            if (styleName.StartsWith("Heading", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(styleName, @"Heading\s*(\d+)", RegexOptions.IgnoreCase);
                if (match.Success && int.TryParse(match.Groups[1].Value, out level))
                {
                    return level >= 1 && level <= 6;
                }
            }

            return false;
        }

        /// <summary>
        /// 收集Word文档中每个标题下的内容
        /// </summary>
        private static void CollectWordHeadingContent(WordDocument doc, List<DocumentHeading> headings, Action<string> progressCallback = null)
        {
            try
            {
                Debug.WriteLine($"开始为 {headings.Count} 个标题收集内容...");
                
                for (int i = 0; i < headings.Count; i++)
                {
                    var currentHeading = headings[i];
                    var progressMsg = $"收集标题内容 ({i + 1}/{headings.Count}): {currentHeading.HeadingText}";
                    Debug.WriteLine(progressMsg);
                    progressCallback?.Invoke(progressMsg);
                    
                    var content = new StringBuilder();
                    bool collectingContent = false;
                    int contentParagraphs = 0;
                    
                    foreach (Paragraph para in doc.Paragraphs)
                    {
                        try
                        {
                            string paraText = para.Range.Text.Trim().Replace("\r", "");
                            
                            // 找到当前标题
                            if (paraText == currentHeading.HeadingText)
                            {
                                collectingContent = true;
                                continue;
                            }
                            
                            if (collectingContent)
                            {
                                // 检查是否遇到其他标题
                                var style = para.get_Style();
                                if (IsHeadingStyle(style.NameLocal, out int level))
                                {
                                    // 如果遇到同级或更高级别的标题，停止收集
                                    if (level <= currentHeading.HeadingLevel)
                                    {
                                        // 检查是否为当前标题的子标题
                                        bool isSubHeading = headings.Any(h => h.HeadingText == paraText && 
                                                                            h.HeadingLevel > currentHeading.HeadingLevel);
                                        if (!isSubHeading)
                                        {
                                            break;
                                        }
                                    }
                                }
                                
                                // 收集内容
                                if (!string.IsNullOrWhiteSpace(paraText))
                                {
                                    content.AppendLine(paraText);
                                    contentParagraphs++;
                                }
                            }
                        }
                        catch (Exception paraEx)
                        {
                            Debug.WriteLine($"处理内容段落时出错: {paraEx.Message}");
                            // 继续处理下一个段落
                        }
                    }
                    
                    currentHeading.Content = content.ToString().Trim();
                    Debug.WriteLine($"标题 '{currentHeading.HeadingText}' 收集到 {contentParagraphs} 个内容段落，总长度: {currentHeading.Content.Length} 字符");
                }
                
                Debug.WriteLine("所有标题内容收集完成");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"收集Word标题内容时出错: {ex.Message}");
                Debug.WriteLine($"错误详情: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// 验证文件格式
        /// </summary>
        public static bool IsSupportedFileType(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".docx" || extension == ".doc" || extension == ".md";
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        public static string GetFileType(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return "";
            
            return Path.GetExtension(filePath).ToLower().TrimStart('.');
        }
    }

    /// <summary>
    /// 文档解析结果
    /// </summary>
    public class DocumentParseResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DocumentHeading> Headings { get; set; } = new List<DocumentHeading>();
    }
} 