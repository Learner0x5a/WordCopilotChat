using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using WordCopilotChat.db;
using WordCopilotChat.services;

namespace WordCopilotChat
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // 先初始化 FreeSql 单例（这一步在项目启动时只会执行一次）
            var freeSqlInstance = FreeSqlDB.Sqlite;

            // 初始化默认模板数据
            var templateService = new RequestTemplateService();
            templateService.InitializeDefaultTemplates();

            // 初始化Model表结构
            var modelService = new ModelService();
            modelService.InitializeModelTable();

            // 初始化Prompt表结构
            var promptService = new PromptService();
            promptService.InitializePromptTable();

            // 初始化文档相关表结构
            var documentService = new DocumentService();
            documentService.InitializeDocumentTables();

            // 初始化应用设置表结构
            var appSettingsService = new AppSettingsService();
            appSettingsService.InitializeAppSettingsTable();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
