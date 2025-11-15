namespace WordCopilotChat
{
    partial class DocumentSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.numericUpDownMaxDocs = new System.Windows.Forms.NumericUpDown();
            this.labelMaxDocs = new System.Windows.Forms.Label();
            this.groupBoxDocuments = new System.Windows.Forms.GroupBox();
            this.listViewDocuments = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderHeadings = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderUploadTime = new System.Windows.Forms.ColumnHeader();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonViewDetail = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelCurrentCount = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDocs)).BeginInit();
            this.groupBoxDocuments.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.labelWarning);
            this.groupBoxSettings.Controls.Add(this.numericUpDownMaxDocs);
            this.groupBoxSettings.Controls.Add(this.labelMaxDocs);
            this.groupBoxSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(760, 120);
            this.groupBoxSettings.TabIndex = 0;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "文档设置";
            // 
            // labelWarning
            // 
            this.labelWarning.ForeColor = System.Drawing.Color.Orange;
            this.labelWarning.Location = new System.Drawing.Point(15, 55);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(730, 55);
            this.labelWarning.TabIndex = 2;
            this.labelWarning.Text = "⚠️ 警告：增加文档数量限制可能会导致：\r\n• 占用更多系统内存和存储空间\r\n• 文档检索和搜索速度变慢\r\n• 影响整体系统性能\r\n建议根据您的电脑配置合理设置，" +
    "一般情况下10-20篇文档为最佳。";
            // 
            // numericUpDownMaxDocs
            // 
            this.numericUpDownMaxDocs.Location = new System.Drawing.Point(120, 25);
            this.numericUpDownMaxDocs.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownMaxDocs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxDocs.Name = "numericUpDownMaxDocs";
            this.numericUpDownMaxDocs.Size = new System.Drawing.Size(80, 21);
            this.numericUpDownMaxDocs.TabIndex = 1;
            this.numericUpDownMaxDocs.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelMaxDocs
            // 
            this.labelMaxDocs.AutoSize = true;
            this.labelMaxDocs.Location = new System.Drawing.Point(15, 27);
            this.labelMaxDocs.Name = "labelMaxDocs";
            this.labelMaxDocs.Size = new System.Drawing.Size(99, 12);
            this.labelMaxDocs.TabIndex = 0;
            this.labelMaxDocs.Text = "最大文档数量限制:";
            // 
            // groupBoxDocuments
            // 
            this.groupBoxDocuments.Controls.Add(this.labelCurrentCount);
            this.groupBoxDocuments.Controls.Add(this.buttonRefresh);
            this.groupBoxDocuments.Controls.Add(this.buttonViewDetail);
            this.groupBoxDocuments.Controls.Add(this.buttonDelete);
            this.groupBoxDocuments.Controls.Add(this.buttonUpload);
            this.groupBoxDocuments.Controls.Add(this.buttonExport);
            this.groupBoxDocuments.Controls.Add(this.buttonImport);
            this.groupBoxDocuments.Controls.Add(this.listViewDocuments);
            this.groupBoxDocuments.Location = new System.Drawing.Point(12, 148);
            this.groupBoxDocuments.Name = "groupBoxDocuments";
            this.groupBoxDocuments.Size = new System.Drawing.Size(760, 350);
            this.groupBoxDocuments.TabIndex = 1;
            this.groupBoxDocuments.TabStop = false;
            this.groupBoxDocuments.Text = "已上传文档";
            // 
            // listViewDocuments
            // 
            this.listViewDocuments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderType,
            this.columnHeaderSize,
            this.columnHeaderHeadings,
            this.columnHeaderUploadTime});
            this.listViewDocuments.ContextMenuStrip = this.contextMenuStrip;
            this.listViewDocuments.FullRowSelect = true;
            this.listViewDocuments.GridLines = true;
            this.listViewDocuments.Location = new System.Drawing.Point(15, 50);
            this.listViewDocuments.MultiSelect = false;
            this.listViewDocuments.Name = "listViewDocuments";
            this.listViewDocuments.Size = new System.Drawing.Size(730, 250);
            this.listViewDocuments.TabIndex = 0;
            this.listViewDocuments.UseCompatibleStateImageBehavior = false;
            this.listViewDocuments.View = System.Windows.Forms.View.Details;
            this.listViewDocuments.SelectedIndexChanged += new System.EventHandler(this.listViewDocuments_SelectedIndexChanged);
            this.listViewDocuments.DoubleClick += new System.EventHandler(this.listViewDocuments_DoubleClick);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "文档名称";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "类型";
            this.columnHeaderType.Width = 60;
            // 
            // columnHeaderSize
            // 
            this.columnHeaderSize.Text = "大小";
            this.columnHeaderSize.Width = 80;
            // 
            // columnHeaderHeadings
            // 
            this.columnHeaderHeadings.Text = "标题数";
            this.columnHeaderHeadings.Width = 70;
            // 
            // columnHeaderUploadTime
            // 
            this.columnHeaderUploadTime.Text = "上传时间";
            this.columnHeaderUploadTime.Width = 140;
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(15, 310);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(80, 30);
            this.buttonUpload.TabIndex = 1;
            this.buttonUpload.Text = "上传文档";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(105, 310);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(80, 30);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "删除文档";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonViewDetail
            // 
            this.buttonViewDetail.Enabled = false;
            this.buttonViewDetail.Location = new System.Drawing.Point(195, 310);
            this.buttonViewDetail.Name = "buttonViewDetail";
            this.buttonViewDetail.Size = new System.Drawing.Size(80, 30);
            this.buttonViewDetail.TabIndex = 3;
            this.buttonViewDetail.Text = "查看详情";
            this.buttonViewDetail.UseVisualStyleBackColor = true;
            this.buttonViewDetail.Click += new System.EventHandler(this.buttonViewDetail_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(285, 310);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(80, 30);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "刷新列表";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(375, 310);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(80, 30);
            this.buttonExport.TabIndex = 5;
            this.buttonExport.Text = "导出文档";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(465, 310);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(80, 30);
            this.buttonImport.TabIndex = 6;
            this.buttonImport.Text = "导入文档";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(612, 514);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 30);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "保存设置";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(698, 514);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 30);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelCurrentCount
            // 
            this.labelCurrentCount.AutoSize = true;
            this.labelCurrentCount.Location = new System.Drawing.Point(15, 25);
            this.labelCurrentCount.Name = "labelCurrentCount";
            this.labelCurrentCount.Size = new System.Drawing.Size(107, 12);
            this.labelCurrentCount.TabIndex = 5;
            this.labelCurrentCount.Text = "当前文档数量: 0/10";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemView,
            this.toolStripMenuItemDelete,
            this.toolStripMenuItemExport,
            this.toolStripSeparator1,
            this.toolStripMenuItemRefresh});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(137, 98);
            // 
            // toolStripMenuItemView
            // 
            this.toolStripMenuItemView.Name = "toolStripMenuItemView";
            this.toolStripMenuItemView.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItemView.Text = "查看详情";
            this.toolStripMenuItemView.Click += new System.EventHandler(this.toolStripMenuItemView_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItemDelete.Text = "删除文档";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // toolStripMenuItemExport
            // 
            this.toolStripMenuItemExport.Name = "toolStripMenuItemExport";
            this.toolStripMenuItemExport.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemExport.Text = "导出所选";
            this.toolStripMenuItemExport.Click += new System.EventHandler(this.toolStripMenuItemExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // toolStripMenuItemRefresh
            // 
            this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
            this.toolStripMenuItemRefresh.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItemRefresh.Text = "刷新列表";
            this.toolStripMenuItemRefresh.Click += new System.EventHandler(this.toolStripMenuItemRefresh_Click);
            // 
            // DocumentSettingsForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxDocuments);
            this.Controls.Add(this.groupBoxSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文档管理设置";
            this.Load += new System.EventHandler(this.DocumentSettingsForm_Load);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDocs)).EndInit();
            this.groupBoxDocuments.ResumeLayout(false);
            this.groupBoxDocuments.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxDocs;
        private System.Windows.Forms.Label labelMaxDocs;
        private System.Windows.Forms.GroupBox groupBoxDocuments;
        private System.Windows.Forms.ListView listViewDocuments;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderHeadings;
        private System.Windows.Forms.ColumnHeader columnHeaderUploadTime;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonViewDetail;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelCurrentCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRefresh;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExport;
        private System.Windows.Forms.Button buttonImport;
    }
}