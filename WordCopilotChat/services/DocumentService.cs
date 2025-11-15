using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using WordCopilotChat.db;
using WordCopilotChat.models;

namespace WordCopilotChat.services
{
    public class DocumentService
    {
        private readonly IFreeSql _freeSql;

        public DocumentService()
        {
            _freeSql = FreeSqlDB.Sqlite;
        }

        /// <summary>
        /// 初始化文档相关表结构
        /// </summary>
        public void InitializeDocumentTables()
        {
            try
            {
                _freeSql.CodeFirst.SyncStructure<Document>();
                _freeSql.CodeFirst.SyncStructure<DocumentHeading>();
                _freeSql.CodeFirst.SyncStructure<DocumentSettings>();
                
                // 初始化默认设置
                InitializeDefaultSettings();
                
                Debug.WriteLine("文档表初始化成功");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"初始化文档表失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 初始化默认设置
        /// </summary>
        private void InitializeDefaultSettings()
        {
            var existingSettings = _freeSql.Select<DocumentSettings>().First();
            if (existingSettings == null)
            {
                var defaultSettings = new DocumentSettings
                {
                    MaxDocuments = 10,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                };
                _freeSql.Insert(defaultSettings).ExecuteAffrows();
            }
        }

        /// <summary>
        /// 获取文档设置
        /// </summary>
        public DocumentSettings GetDocumentSettings()
        {
            return _freeSql.Select<DocumentSettings>().First() ?? new DocumentSettings { MaxDocuments = 10 };
        }

        /// <summary>
        /// 更新文档设置
        /// </summary>
        public bool UpdateDocumentSettings(DocumentSettings settings)
        {
            try
            {
                settings.UpdatedTime = DateTime.Now;
                return _freeSql.Update<DocumentSettings>()
                    .SetSource(settings)
                    .ExecuteAffrows() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"更新文档设置失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取所有活跃文档
        /// </summary>
        public List<Document> GetAllDocuments()
        {
            return _freeSql.Select<Document>()
                .Where(d => d.IsActive)
                .OrderByDescending(d => d.UploadTime)
                .ToList();
        }

        /// <summary>
        /// 根据ID获取文档
        /// </summary>
        public Document GetDocumentById(int id)
        {
            return _freeSql.Select<Document>()
                .Where(d => d.Id == id && d.IsActive)
                .First();
        }

        /// <summary>
        /// 添加文档
        /// </summary>
        public bool AddDocument(Document document)
        {
            try
            {
                if (document == null || string.IsNullOrWhiteSpace(document.FileName))
                    return false;

                document.UploadTime = DateTime.Now;
                return _freeSql.Insert(document).ExecuteAffrows() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"添加文档失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 删除文档（物理删除，同时删除相关标题数据）
        /// </summary>
        public bool DeleteDocument(int id)
        {
            bool success = false;
            try
            {
                _freeSql.Transaction(() =>
                {
                    // 首先检查文档是否存在
                    var document = _freeSql.Select<Document>()
                        .Where(d => d.Id == id && d.IsActive == true)
                        .First();

                    if (document != null)
                    {
                        // 先删除相关的标题数据
                        var headingsDeleted = _freeSql.Delete<DocumentHeading>()
                            .Where(h => h.DocumentId == id)
                            .ExecuteAffrows();
                        
                        // 然后物理删除文档
                        var documentDeleted = _freeSql.Delete<Document>()
                            .Where(d => d.Id == id)
                            .ExecuteAffrows();
                        
                        if (documentDeleted > 0)
                        {
                            Debug.WriteLine($"成功删除文档 ID: {id}，同时删除了 {headingsDeleted} 个相关标题");
                            success = true;
                        }
                        else
                        {
                            throw new Exception($"删除文档记录失败，文档 ID: {id}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"删除文档失败，文档 ID: {id} 不存在或已被删除");
                        throw new Exception($"文档 ID: {id} 不存在或已被删除");
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"删除文档失败: {ex.Message}");
                success = false;
            }
            
            return success;
        }

        /// <summary>
        /// 获取文档的所有标题
        /// </summary>
        public List<DocumentHeading> GetDocumentHeadings(int documentId)
        {
            return _freeSql.Select<DocumentHeading>()
                .Where(h => h.DocumentId == documentId)
                .OrderBy(h => h.OrderIndex)
                .ToList();
        }

        /// <summary>
        /// 添加文档标题
        /// </summary>
        public bool AddDocumentHeading(DocumentHeading heading)
        {
            try
            {
                if (heading == null || string.IsNullOrWhiteSpace(heading.HeadingText))
                    return false;

                heading.CreatedTime = DateTime.Now;
                return _freeSql.Insert(heading).ExecuteAffrows() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"添加文档标题失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 批量添加文档标题
        /// </summary>
        public bool AddDocumentHeadings(List<DocumentHeading> headings)
        {
            try
            {
                if (headings == null || !headings.Any())
                    return false;

                foreach (var heading in headings)
                {
                    heading.CreatedTime = DateTime.Now;
                }

                return _freeSql.Insert(headings).ExecuteAffrows() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"批量添加文档标题失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查是否达到文档数量限制
        /// </summary>
        public bool IsDocumentLimitReached()
        {
            var settings = GetDocumentSettings();
            var documentCount = _freeSql.Select<Document>()
                .Where(d => d.IsActive)
                .Count();
            
            return documentCount >= settings.MaxDocuments;
        }

        /// <summary>
        /// 获取当前文档数量
        /// </summary>
        public int GetDocumentCount()
        {
            return (int)_freeSql.Select<Document>()
                .Where(d => d.IsActive)
                .Count();
        }

        /// <summary>
        /// 根据文件名检查是否已存在
        /// </summary>
        public bool IsDocumentExists(string fileName)
        {
            return _freeSql.Select<Document>()
                .Where(d => d.FileName == fileName && d.IsActive)
                .Any();
        }

        /// <summary>
        /// 获取指定文档的标题数量（用于验证删除功能）
        /// </summary>
        public int GetDocumentHeadingCount(int documentId)
        {
            return (int)_freeSql.Select<DocumentHeading>()
                .Where(h => h.DocumentId == documentId)
                .Count();
        }

        /// <summary>
        /// 验证文档删除功能是否正确工作
        /// </summary>
        public string ValidateDocumentDeletion(int documentId)
        {
            try
            {
                // 检查文档是否还存在（物理删除后应该不存在）
                var document = _freeSql.Select<Document>().Where(d => d.Id == documentId).First();
                var headingCount = GetDocumentHeadingCount(documentId);
                
                if (document == null)
                {
                    if (headingCount == 0)
                    {
                        return $"✅ 删除成功：文档已完全删除，关联标题数: {headingCount}";
                    }
                    else
                    {
                        return $"⚠️ 部分删除：文档已删除，但仍有 {headingCount} 个关联标题未删除";
                    }
                }
                else
                {
                    return $"❌ 删除失败：文档仍存在 (活跃状态: {document.IsActive})，关联标题数: {headingCount}";
                }
            }
            catch (Exception ex)
            {
                return $"验证失败: {ex.Message}";
            }
        }
    }
} 