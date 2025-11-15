namespace WordCopilotChat
{
    partial class ModelListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView _listView;
        private System.Windows.Forms.ContextMenuStrip _contextMenu;
        private System.Windows.Forms.ToolStripMenuItem addMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem refreshMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedMenuItem;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDefaultSettings;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderTemplate;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderUrl;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._listView = new System.Windows.Forms.ListView();
            this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportSelectedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTemplate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.btnDefaultSettings = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this._contextMenu.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _listView
            // 
            this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderName,
            this.columnHeaderTemplate,
            this.columnHeaderType,
            this.columnHeaderUrl});
            this._listView.ContextMenuStrip = this._contextMenu;
            this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listView.FullRowSelect = true;
            this._listView.GridLines = true;
            this._listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._listView.HideSelection = false;
            this._listView.Location = new System.Drawing.Point(0, 0);
            this._listView.Name = "_listView";
            this._listView.Size = new System.Drawing.Size(584, 311);
            this._listView.TabIndex = 0;
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.View = System.Windows.Forms.View.Details;
            this._listView.DoubleClick += new System.EventHandler(this.ListView_DoubleClick);
            // 
            // _contextMenu
            // 
            this._contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMenuItem,
            this.editMenuItem,
            this.deleteMenuItem,
            this.toolStripSeparator1,
            this.exportSelectedMenuItem,
            this.toolStripSeparator2,
            this.refreshMenuItem});
            this._contextMenu.Name = "_contextMenu";
            this._contextMenu.Size = new System.Drawing.Size(149, 126);
            // 
            // addMenuItem
            // 
            this.addMenuItem.Name = "addMenuItem";
            this.addMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addMenuItem.Text = "新增模型";
            this.addMenuItem.Click += new System.EventHandler(this.AddMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(148, 22);
            this.editMenuItem.Text = "编辑模型";
            this.editMenuItem.Click += new System.EventHandler(this.EditMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deleteMenuItem.Text = "删除模型";
            this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // exportSelectedMenuItem
            // 
            this.exportSelectedMenuItem.Name = "exportSelectedMenuItem";
            this.exportSelectedMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exportSelectedMenuItem.Text = "导出所选模型";
            this.exportSelectedMenuItem.Click += new System.EventHandler(this.ExportSelectedMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.Name = "refreshMenuItem";
            this.refreshMenuItem.Size = new System.Drawing.Size(148, 22);
            this.refreshMenuItem.Text = "刷新列表";
            this.refreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "ID";
            this.columnHeaderId.Width = 0;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "模型名称";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderTemplate
            // 
            this.columnHeaderTemplate.Text = "请求模板";
            this.columnHeaderTemplate.Width = 120;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "模型类型";
            this.columnHeaderType.Width = 100;
            // 
            // columnHeaderUrl
            // 
            this.columnHeaderUrl.Text = "API地址";
            this.columnHeaderUrl.Width = 150;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.btnDefaultSettings);
            this.bottomPanel.Controls.Add(this.btnImport);
            this.bottomPanel.Controls.Add(this.btnExportAll);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 311);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(10);
            this.bottomPanel.Size = new System.Drawing.Size(584, 50);
            this.bottomPanel.TabIndex = 1;
            // 
            // btnDefaultSettings
            // 
            this.btnDefaultSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefaultSettings.Location = new System.Drawing.Point(450, 13);
            this.btnDefaultSettings.Name = "btnDefaultSettings";
            this.btnDefaultSettings.Size = new System.Drawing.Size(120, 24);
            this.btnDefaultSettings.TabIndex = 2;
            this.btnDefaultSettings.Text = "默认参数设置";
            this.btnDefaultSettings.UseVisualStyleBackColor = true;
            this.btnDefaultSettings.Click += new System.EventHandler(this.BtnDefaultSettings_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(119, 13);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 24);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "导入模型";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportAll.Location = new System.Drawing.Point(13, 13);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(100, 24);
            this.btnExportAll.TabIndex = 0;
            this.btnExportAll.Text = "导出全部模型";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.BtnExportAll_Click);
            // 
            // ModelListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this._listView);
            this.Controls.Add(this.bottomPanel);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "ModelListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模型列表";
            this._contextMenu.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
} 