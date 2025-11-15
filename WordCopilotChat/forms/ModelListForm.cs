using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using WordCopilotChat.models;
using WordCopilotChat.services;
using WordCopilotChat.utils;

namespace WordCopilotChat
{
    public partial class ModelListForm : Form
    {
        private ModelService _modelService;
        private AppSettingsService _appSettingsService;

        public ModelListForm()
        {
            InitializeComponent();
            _modelService = new ModelService();
            _appSettingsService = new AppSettingsService();
            LoadModels();
        }

        private void LoadModels()
        {
            try
            {
                _listView.Items.Clear();
                var models = _modelService.GetAllModels();

                foreach (var model in models)
                {
                    var item = new ListViewItem(model.Id.ToString());
                    item.SubItems.Add(model.NickName ?? "未命名");
                    item.SubItems.Add(model.Template?.TemplateName ?? "未选择");
                    item.SubItems.Add(model.modelType == 1 ? "对话模型" : "词嵌入模型");
                    item.SubItems.Add(model.BaseUrl ?? "未设置");
                    item.Tag = model;
                    _listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载模型列表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            ShowAddEditDialog();
        }

        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            if (_listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要编辑的模型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedModel = _listView.SelectedItems[0].Tag as Model;
            ShowAddEditDialog(selectedModel);
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (_listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要删除的模型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedModel = _listView.SelectedItems[0].Tag as Model;
            var result = MessageBox.Show($"确定要删除模型 '{selectedModel.NickName}' 吗？", 
                "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (_modelService.DeleteModel(selectedModel.Id))
                {
                    MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadModels();
                }
                else
                {
                    MessageBox.Show("删除失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            LoadModels();
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            if (_listView.SelectedItems.Count > 0)
            {
                var selectedModel = _listView.SelectedItems[0].Tag as Model;
                ShowAddEditDialog(selectedModel);
            }
        }

        private void ShowAddEditDialog(Model model = null)
        {
            try
            {
                var addModelForm = new AddModelForm(model);
                if (addModelForm.ShowDialog() == DialogResult.OK)
                {
                    LoadModels(); // 刷新列表
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开编辑窗口失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 导出所选模型
        private void ExportSelectedMenuItem_Click(object sender, EventArgs e)
        {
            if (_listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要导出的模型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedModels = new List<Model>();
            foreach (ListViewItem item in _listView.SelectedItems)
            {
                selectedModels.Add(item.Tag as Model);
            }

            ExportModels(selectedModels, $"导出{selectedModels.Count}个模型");
        }

        // 导出全部模型
        private void BtnExportAll_Click(object sender, EventArgs e)
        {
            try
            {
                var allModels = _modelService.GetAllModels();
                if (allModels.Count == 0)
                {
                    MessageBox.Show("没有模型可以导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ExportModels(allModels, "导出全部模型");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取模型列表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 导入模型
        private void BtnImport_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "WordCopilot配置文件|*.wc|所有文件|*.*";
                openFileDialog.Title = "选择要导入的模型配置文件";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 获取密码
                        string password = PasswordDialog.ShowImportDialog(this);
                        if (string.IsNullOrEmpty(password))
                        {
                            return; // 用户取消
                        }

                        // 读取加密文件
                        string encryptedContent = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                        
                        // 解密内容
                        string jsonContent;
                        try
                        {
                            jsonContent = EncryptionUtils.Decrypt(encryptedContent, password);
                        }
                        catch (System.Security.Cryptography.CryptographicException)
                        {
                            MessageBox.Show("密码错误，无法解密文件", "解密失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 解析JSON
                        var importData = JsonConvert.DeserializeObject<ModelExportData>(jsonContent);

                        if (importData?.Models == null || importData.Models.Count == 0)
                        {
                            MessageBox.Show("文件中没有找到有效的模型数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var result = MessageBox.Show($"确定要导入 {importData.Models.Count} 个模型吗？\n\n注意：如果存在同名模型，将会自动重命名。", 
                            "确认导入", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            int successCount = 0;
                            int errorCount = 0;

                            // 获取现有模型名称，用于检查重复
                            var existingModels = _modelService.GetAllModels();
                            var existingNames = new HashSet<string>(
                                existingModels.Select(m => m.NickName?.ToLower() ?? ""), 
                                StringComparer.OrdinalIgnoreCase
                            );

                            foreach (var model in importData.Models)
                            {
                                try
                                {
                                    // 重置ID，让数据库自动分配
                                    model.Id = 0;
                                    
                                    // 处理重复名称
                                    model.NickName = GetUniqueModelName(model.NickName, existingNames);
                                    existingNames.Add(model.NickName.ToLower());
                                    
                                    if (_modelService.AddModel(model))
                                    {
                                        successCount++;
                                    }
                                    else
                                    {
                                        errorCount++;
                                    }
                                }
                                catch
                                {
                                    errorCount++;
                                }
                            }

                            MessageBox.Show($"导入完成！\n成功：{successCount} 个\n失败：{errorCount} 个", 
                                "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadModels(); // 刷新列表
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导入失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 通用导出方法
        private void ExportModels(List<Model> models, string title)
        {
            // 获取导出密码
            string password = PasswordDialog.ShowExportDialog(this);
            if (string.IsNullOrEmpty(password))
            {
                return; // 用户取消
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "WordCopilot配置文件|*.wc";
                saveFileDialog.Title = title;
                saveFileDialog.FileName = $"WordCopilot_Models_{DateTime.Now:yyyyMMdd_HHmmss}.wc";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exportData = new ModelExportData
                        {
                            ExportTime = DateTime.Now,
                            Version = "1.0",
                            Models = models
                        };

                        // 序列化为JSON
                        string jsonContent = JsonConvert.SerializeObject(exportData, Formatting.Indented);
                        
                        // 加密内容
                        string encryptedContent = EncryptionUtils.Encrypt(jsonContent, password);
                        
                        // 写入加密文件
                        File.WriteAllText(saveFileDialog.FileName, encryptedContent, Encoding.UTF8);

                        MessageBox.Show($"导出成功！\n文件保存至：{saveFileDialog.FileName}\n\n请妥善保管您的密码，丢失密码将无法导入配置。", 
                            "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 获取唯一的模型名称（处理重复）
        /// </summary>
        /// <param name="originalName">原始名称</param>
        /// <param name="existingNames">已存在的名称集合</param>
        /// <returns>唯一的名称</returns>
        private string GetUniqueModelName(string originalName, HashSet<string> existingNames)
        {
            if (string.IsNullOrEmpty(originalName))
            {
                originalName = "未命名模型";
            }

            string baseName = originalName;
            string uniqueName = baseName;
            int counter = 1;

            // 如果名称已存在，添加序号
            while (existingNames.Contains(uniqueName.ToLower()))
            {
                uniqueName = $"{baseName}-{counter}";
                counter++;
            }

            return uniqueName;
        }

        // 默认参数设置按钮点击事件
        private void BtnDefaultSettings_Click(object sender, EventArgs e)
        {
            ShowDefaultParametersDialog();
        }

        /// <summary>
        /// 显示默认参数设置对话框
        /// </summary>
        private void ShowDefaultParametersDialog()
        {
            try
            {
                var settingsForm = new DefaultParametersForm(_appSettingsService);
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("默认参数设置已保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开设置窗口失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // 导出数据结构
    public class ModelExportData
    {
        public DateTime ExportTime { get; set; }
        public string Version { get; set; }
        public List<Model> Models { get; set; }
    }
} 