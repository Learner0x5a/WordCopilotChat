using System;
using System.Collections.Generic;
using System.Linq;
using WordCopilotChat.db;
using WordCopilotChat.models;

namespace WordCopilotChat.services
{
    public class RequestTemplateService
    {
        private readonly IFreeSql _freeSql;

        public RequestTemplateService()
        {
            _freeSql = FreeSqlDB.Sqlite;
        }

        /// <summary>
        /// 初始化默认模板数据
        /// </summary>
        public void InitializeDefaultTemplates()
        {
            try
            {
                // 先确保表存在
                _freeSql.CodeFirst.SyncStructure<RequestTemplate>();

                // 检查是否已有数据
                var existingCount = _freeSql.Select<RequestTemplate>().Count();
                if (existingCount > 0)
                {
                    return; // 已有数据，不需要初始化
                }

                // 插入默认数据
                var defaultTemplates = new List<RequestTemplate>
                {
                    new RequestTemplate { TemplateName = "OpenAI" },
                };

                _freeSql.Insert(defaultTemplates).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"初始化默认模板失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取所有模板
        /// </summary>
        public List<RequestTemplate> GetAllTemplates()
        {
            return _freeSql.Select<RequestTemplate>().ToList();
        }

        /// <summary>
        /// 添加新模板
        /// </summary>
        public bool AddTemplate(string templateName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(templateName))
                    return false;

                var template = new RequestTemplate
                {
                    TemplateName = templateName.Trim()
                };

                return _freeSql.Insert(template).ExecuteAffrows() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"添加模板失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        public bool DeleteTemplate(int id)
        {
            try
            {
                return _freeSql.Delete<RequestTemplate>().Where(x => x.Id == id).ExecuteAffrows() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"删除模板失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 更新模板
        /// </summary>
        public bool UpdateTemplate(int id, string templateName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(templateName))
                    return false;

                return _freeSql.Update<RequestTemplate>()
                    .Set(x => x.TemplateName, templateName.Trim())
                    .Where(x => x.Id == id)
                    .ExecuteAffrows() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更新模板失败: {ex.Message}");
                return false;
            }
        }
    }
} 