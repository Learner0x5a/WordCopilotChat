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
using WordCopilotChat.models;
using WordCopilotChat.services;
using WordCopilotChat.utils;
using Newtonsoft.Json;

namespace WordCopilotChat
{
    public partial class DocumentSettingsForm: Form
    {
        private DocumentService _documentService;
        private List<Document> _documents;
        private DocumentSettings _settings;

        public DocumentSettingsForm()
        {
            InitializeComponent();
            _documentService = new DocumentService();
        }

        private void DocumentSettingsForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            LoadDocuments();
        }

        /// <summary>
        /// 加载文档设置
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                _settings = _documentService.GetDocumentSettings();
                numericUpDownMaxDocs.Value = _settings.MaxDocuments;
                UpdateCurrentCountLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载设置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载文档列表
        /// </summary>
        private void LoadDocuments()
        {
            try
            {
                _documents = _documentService.GetAllDocuments();
                UpdateDocumentList();
                UpdateCurrentCountLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载文档列表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 更新文档列表显示
        /// </summary>
        private void UpdateDocumentList()
        {
            listViewDocuments.Items.Clear();

            foreach (var doc in _documents)
            {
                var item = new ListViewItem(doc.FileName);
                item.SubItems.Add(doc.FileType.ToUpper());
                item.SubItems.Add(FormatFileSize(doc.FileSize));
                item.SubItems.Add(doc.TotalHeadings.ToString());
                item.SubItems.Add(doc.UploadTime.ToString("yyyy-MM-dd HH:mm"));
                item.Tag = doc;

                listViewDocuments.Items.Add(item);
            }

            // 更新按钮状态
            UpdateButtonStates();
        }

        /// <summary>
        /// 更新当前数量标签
        /// </summary>
        private void UpdateCurrentCountLabel()
        {
            int currentCount = _documentService.GetDocumentCount();
            int maxCount = (int)numericUpDownMaxDocs.Value;
            labelCurrentCount.Text = $"当前文档数量: {currentCount}/{maxCount}";

            // 如果接近限制，改变颜色提醒
            if (currentCount >= maxCount * 0.8)
            {
                labelCurrentCount.ForeColor = Color.Orange;
            }
            else if (currentCount >= maxCount)
            {
                labelCurrentCount.ForeColor = Color.Red;
            }
            else
            {
                labelCurrentCount.ForeColor = SystemColors.ControlText;
            }
        }

        /// <summary>
        /// 更新按钮状态
        /// </summary>
        private void UpdateButtonStates()
        {
            bool hasSelection = listViewDocuments.SelectedItems.Count > 0;
            buttonDelete.Enabled = hasSelection;
            buttonViewDetail.Enabled = hasSelection;

            // 检查是否可以上传更多文档
            bool canUpload = !_documentService.IsDocumentLimitReached();
            buttonUpload.Enabled = canUpload;

            if (!canUpload)
            {
                buttonUpload.Text = "已达上限";
            }
            else
            {
                buttonUpload.Text = "上传文档";
            }
        }

        /// <summary>
        /// 格式化文件大小
        /// </summary>
        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes} B";
            else if (bytes < 1024 * 1024)
                return $"{bytes / 1024:F1} KB";
            else
                return $"{bytes / (1024 * 1024):F1} MB";
        }

        #region 事件处理

        private void listViewDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void listViewDocuments_DoubleClick(object sender, EventArgs e)
        {
            if (listViewDocuments.SelectedItems.Count > 0)
            {
                ViewDocumentDetail();
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            try
            {
                // 检查是否达到限制
                if (_documentService.IsDocumentLimitReached())
                {
                    MessageBox.Show($"已达到文档数量限制（{_settings.MaxDocuments}篇），请先删除一些文档或增加限制。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var uploadForm = new DocumentUploadForm();
                if (uploadForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDocuments(); // 重新加载文档列表
                    MessageBox.Show("文档上传成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"上传文档失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedDocument();
        }

        private void buttonViewDetail_Click(object sender, EventArgs e)
        {
            ViewDocumentDetail();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadDocuments();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            ExportSelectedDocument();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            ImportDocumentFromPackage();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _settings.MaxDocuments = (int)numericUpDownMaxDocs.Value;
                
                if (_documentService.UpdateDocumentSettings(_settings))
                {
                    MessageBox.Show("设置保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("设置保存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存设置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 右键菜单事件
        private void toolStripMenuItemView_Click(object sender, EventArgs e)
        {
            ViewDocumentDetail();
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedDocument();
        }

        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            LoadDocuments();
        }

        private void toolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            ExportSelectedDocument();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 删除选中的文档
        /// </summary>
        private void DeleteSelectedDocument()
        {
            if (listViewDocuments.SelectedItems.Count == 0) return;

            var selectedItem = listViewDocuments.SelectedItems[0];
            var document = (Document)selectedItem.Tag;

            var result = MessageBox.Show($"确定要删除文档 \"{document.FileName}\" 吗？\n\n此操作将删除文档及其所有标题内容，且无法恢复。",
                "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_documentService.DeleteDocument(document.Id))
                    {
                        LoadDocuments(); // 重新加载列表
                        MessageBox.Show("文档删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("文档删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除文档时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 导出所选文档（加密）
        /// </summary>
        private void ExportSelectedDocument()
        {
            if (listViewDocuments.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要导出的文档", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var document = (Document)listViewDocuments.SelectedItems[0].Tag;

            // 获取导出密码
            string password = PasswordDialog.ShowExportDialog(this);
            if (string.IsNullOrEmpty(password)) return;

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "WordCopilot文档|*.wcdoc";
                saveFileDialog.Title = "导出所选文档";
                string baseName = Path.GetFileNameWithoutExtension(document.FileName);
                saveFileDialog.FileName = $"{baseName}.wcdoc";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 构建导出数据（包含文档元数据和所有标题内容）
                        var exportData = new DocumentExportData
                        {
                            ExportTime = DateTime.Now,
                            Version = "1.0",
                            Document = document,
                            Headings = _documentService.GetDocumentHeadings(document.Id)
                        };

                        string json = JsonConvert.SerializeObject(exportData, Formatting.Indented);
                        string encrypted = EncryptionUtils.Encrypt(json, password);
                        File.WriteAllText(saveFileDialog.FileName, encrypted, Encoding.UTF8);

                        MessageBox.Show("导出成功！已保存为加密的 .wcdoc 文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 从加密包导入文档（.wcdoc）
        /// </summary>
        private void ImportDocumentFromPackage()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "WordCopilot文档|*.wcdoc|所有文件|*.*";
                openFileDialog.Title = "导入文档";
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    string password = PasswordDialog.ShowImportDialog(this);
                    if (string.IsNullOrEmpty(password)) return;

                    string encrypted = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                    string json;
                    try
                    {
                        json = EncryptionUtils.Decrypt(encrypted, password);
                    }
                    catch (System.Security.Cryptography.CryptographicException)
                    {
                        MessageBox.Show("密码错误，无法解密文件", "解密失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var data = JsonConvert.DeserializeObject<DocumentExportData>(json);
                    if (data == null || data.Document == null)
                    {
                        MessageBox.Show("文件内容无效", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 写入数据库：新建文档并保存标题
                    var doc = data.Document;
                    doc.Id = 0; // 让数据库分配新ID
                    doc.UploadTime = DateTime.Now;
                    doc.IsActive = true;

                    if (!_documentService.AddDocument(doc))
                    {
                        MessageBox.Show("保存文档失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 获取新ID
                    var newDoc = _documentService.GetAllDocuments()
                        .FirstOrDefault(d => d.FileName == doc.FileName && d.UploadTime == doc.UploadTime);
                    int newId = newDoc?.Id ?? 0;

                    if (newId == 0)
                    {
                        MessageBox.Show("无法获取新文档ID", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (data.Headings != null)
                    {
                        foreach (var h in data.Headings)
                        {
                            h.Id = 0;
                            h.DocumentId = newId;
                            h.CreatedTime = DateTime.Now;
                        }
                        _documentService.AddDocumentHeadings(data.Headings);
                    }

                    LoadDocuments();
                    MessageBox.Show("导入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导入失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 查看文档详情
        /// </summary>
        private void ViewDocumentDetail()
        {
            if (listViewDocuments.SelectedItems.Count == 0) return;

            var selectedItem = listViewDocuments.SelectedItems[0];
            var document = (Document)selectedItem.Tag;

            try
            {
                var detailForm = new DocumentDetailForm(document.Id);
                detailForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开文档详情失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}

// 导出数据结构
public class DocumentExportData
{
    public DateTime ExportTime { get; set; }
    public string Version { get; set; }
    public Document Document { get; set; }
    public List<DocumentHeading> Headings { get; set; }
}
