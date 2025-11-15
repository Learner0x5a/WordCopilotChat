using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordCopilotChat.models;
using WordCopilotChat.services;

namespace WordCopilotChat
{
    public partial class DocumentDetailForm : Form
    {
        private DocumentService _documentService;
        private Document _document;
        private List<DocumentHeading> _headings;
        private int _documentId;

        public DocumentDetailForm(int documentId)
        {
            InitializeComponent();
            _documentId = documentId;
            _documentService = new DocumentService();
        }

        private void DocumentDetailForm_Load(object sender, EventArgs e)
        {
            LoadDocumentData();
        }

        /// <summary>
        /// 加载文档数据
        /// </summary>
        private void LoadDocumentData()
        {
            try
            {
                // 加载文档信息
                _document = _documentService.GetDocumentById(_documentId);
                if (_document == null)
                {
                    MessageBox.Show("文档不存在或已被删除！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // 加载标题列表
                _headings = _documentService.GetDocumentHeadings(_documentId);

                // 更新界面
                UpdateDocumentInfo();
                BuildHeadingTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载文档数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 更新文档信息显示
        /// </summary>
        private void UpdateDocumentInfo()
        {
            this.Text = $"文档详情 - {_document.FileName}";
            labelFileName.Text = _document.FileName;
            labelFileType.Text = _document.FileType.ToUpper();
            labelFileSize.Text = FormatFileSize(_document.FileSize);
            labelUploadTime.Text = _document.UploadTime.ToString("yyyy-MM-dd HH:mm:ss");
            labelTotalHeadings.Text = _document.TotalHeadings.ToString();
        }

        /// <summary>
        /// 构建标题树
        /// </summary>
        private void BuildHeadingTree()
        {
            treeViewHeadings.Nodes.Clear();

            if (_headings == null || !_headings.Any()) return;

            // 创建节点字典，用于快速查找父节点
            var nodeDict = new Dictionary<int, TreeNode>();

            foreach (var heading in _headings.OrderBy(h => h.OrderIndex))
            {
                var node = new TreeNode(heading.HeadingText);
                node.Tag = heading;
                
                // 根据标题级别设置图标和样式
                SetNodeAppearance(node, heading.HeadingLevel);

                nodeDict[heading.Id] = node;

                // 查找父节点
                if (heading.ParentHeadingId.HasValue && nodeDict.ContainsKey(heading.ParentHeadingId.Value))
                {
                    // 添加到父节点
                    nodeDict[heading.ParentHeadingId.Value].Nodes.Add(node);
                }
                else
                {
                    // 添加到根节点
                    treeViewHeadings.Nodes.Add(node);
                }
            }

            // 展开第一级节点
            foreach (TreeNode node in treeViewHeadings.Nodes)
            {
                node.Expand();
            }
        }

        /// <summary>
        /// 设置节点外观
        /// </summary>
        private void SetNodeAppearance(TreeNode node, int level)
        {
            // 根据级别设置不同的颜色和字体
            switch (level)
            {
                case 1:
                    node.ForeColor = Color.DarkBlue;
                    node.NodeFont = new Font(treeViewHeadings.Font, FontStyle.Bold);
                    break;
                case 2:
                    node.ForeColor = Color.DarkGreen;
                    node.NodeFont = new Font(treeViewHeadings.Font, FontStyle.Bold);
                    break;
                case 3:
                    node.ForeColor = Color.DarkOrange;
                    break;
                case 4:
                    node.ForeColor = Color.DarkRed;
                    break;
                default:
                    node.ForeColor = Color.Black;
                    break;
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

        private void treeViewHeadings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is DocumentHeading heading)
            {
                DisplayHeadingContent(heading);
            }
        }

        private void treeViewHeadings_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 双击展开/折叠节点
            if (e.Node.IsExpanded)
            {
                e.Node.Collapse();
            }
            else
            {
                e.Node.Expand();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonExpandAll_Click(object sender, EventArgs e)
        {
            treeViewHeadings.ExpandAll();
        }

        private void buttonCollapseAll_Click(object sender, EventArgs e)
        {
            treeViewHeadings.CollapseAll();
        }

        private void buttonCopyContent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxContent.Text))
            {
                try
                {
                    Clipboard.SetText(textBoxContent.Text);
                    MessageBox.Show("内容已复制到剪贴板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"复制失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) return;

            SearchInTree(searchText);
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch_Click(sender, e);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示标题内容
        /// </summary>
        private void DisplayHeadingContent(DocumentHeading heading)
        {
            labelSelectedHeading.Text = $"H{heading.HeadingLevel}: {heading.HeadingText}";
            textBoxContent.Text = heading.Content ?? "";
            
            // 更新内容统计
            labelContentLength.Text = $"内容长度: {textBoxContent.Text.Length} 字符";
            
            // 启用复制按钮
            buttonCopyContent.Enabled = !string.IsNullOrEmpty(textBoxContent.Text);
        }

        /// <summary>
        /// 在树中搜索
        /// </summary>
        private void SearchInTree(string searchText)
        {
            var foundNodes = new List<TreeNode>();
            
            // 搜索所有节点
            SearchNodesRecursive(treeViewHeadings.Nodes, searchText.ToLower(), foundNodes);

            if (foundNodes.Any())
            {
                // 清除之前的选择
                treeViewHeadings.SelectedNode = null;
                
                // 折叠所有节点
                treeViewHeadings.CollapseAll();
                
                // 展开并高亮找到的节点
                foreach (var node in foundNodes)
                {
                    // 展开到根节点的路径
                    var current = node;
                    while (current != null)
                    {
                        current.Expand();
                        current = current.Parent;
                    }
                    
                    // 设置背景色
                    node.BackColor = Color.Yellow;
                }
                
                // 选中第一个找到的节点
                treeViewHeadings.SelectedNode = foundNodes[0];
                foundNodes[0].EnsureVisible();
                
                labelSearchResult.Text = $"找到 {foundNodes.Count} 个匹配项";
                labelSearchResult.ForeColor = Color.Green;
            }
            else
            {
                labelSearchResult.Text = "未找到匹配项";
                labelSearchResult.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 递归搜索节点
        /// </summary>
        private void SearchNodesRecursive(TreeNodeCollection nodes, string searchText, List<TreeNode> foundNodes)
        {
            foreach (TreeNode node in nodes)
            {
                // 清除之前的高亮
                node.BackColor = Color.White;
                
                // 检查节点文本是否包含搜索文本
                if (node.Text.ToLower().Contains(searchText))
                {
                    foundNodes.Add(node);
                }
                
                // 检查节点内容是否包含搜索文本
                if (node.Tag is DocumentHeading heading && 
                    !string.IsNullOrEmpty(heading.Content) && 
                    heading.Content.ToLower().Contains(searchText))
                {
                    if (!foundNodes.Contains(node))
                    {
                        foundNodes.Add(node);
                    }
                }
                
                // 递归搜索子节点
                if (node.Nodes.Count > 0)
                {
                    SearchNodesRecursive(node.Nodes, searchText, foundNodes);
                }
            }
        }

        #endregion
    }
}