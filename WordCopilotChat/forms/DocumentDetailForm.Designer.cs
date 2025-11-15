namespace WordCopilotChat
{
    partial class DocumentDetailForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxTree = new System.Windows.Forms.GroupBox();
            this.labelSearchResult = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.treeViewHeadings = new System.Windows.Forms.TreeView();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.labelTotalHeadings = new System.Windows.Forms.Label();
            this.labelUploadTime = new System.Windows.Forms.Label();
            this.labelFileSize = new System.Windows.Forms.Label();
            this.labelFileType = new System.Windows.Forms.Label();
            this.labelFileName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxContent = new System.Windows.Forms.GroupBox();
            this.labelContentLength = new System.Windows.Forms.Label();
            this.buttonCopyContent = new System.Windows.Forms.Button();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.labelSelectedHeading = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxTree.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxTree);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxContent);
            this.splitContainer1.Size = new System.Drawing.Size(984, 711);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBoxTree
            // 
            this.groupBoxTree.Controls.Add(this.labelSearchResult);
            this.groupBoxTree.Controls.Add(this.buttonSearch);
            this.groupBoxTree.Controls.Add(this.textBoxSearch);
            this.groupBoxTree.Controls.Add(this.treeViewHeadings);
            this.groupBoxTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTree.Location = new System.Drawing.Point(0, 150);
            this.groupBoxTree.Name = "groupBoxTree";
            this.groupBoxTree.Size = new System.Drawing.Size(400, 561);
            this.groupBoxTree.TabIndex = 1;
            this.groupBoxTree.TabStop = false;
            this.groupBoxTree.Text = "文档结构";
            // 
            // labelSearchResult
            // 
            this.labelSearchResult.AutoSize = true;
            this.labelSearchResult.Location = new System.Drawing.Point(15, 485);
            this.labelSearchResult.Name = "labelSearchResult";
            this.labelSearchResult.Size = new System.Drawing.Size(0, 12);
            this.labelSearchResult.TabIndex = 5;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(310, 450);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(60, 25);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "搜索";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(15, 452);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(285, 21);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // treeViewHeadings
            // 
            this.treeViewHeadings.Location = new System.Drawing.Point(15, 31);
            this.treeViewHeadings.Name = "treeViewHeadings";
            this.treeViewHeadings.Size = new System.Drawing.Size(370, 409);
            this.treeViewHeadings.TabIndex = 0;
            this.treeViewHeadings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewHeadings_AfterSelect);
            this.treeViewHeadings.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewHeadings_NodeMouseDoubleClick);
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.labelTotalHeadings);
            this.groupBoxInfo.Controls.Add(this.labelUploadTime);
            this.groupBoxInfo.Controls.Add(this.labelFileSize);
            this.groupBoxInfo.Controls.Add(this.labelFileType);
            this.groupBoxInfo.Controls.Add(this.labelFileName);
            this.groupBoxInfo.Controls.Add(this.label5);
            this.groupBoxInfo.Controls.Add(this.label4);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(400, 150);
            this.groupBoxInfo.TabIndex = 0;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "文档信息";
            // 
            // labelTotalHeadings
            // 
            this.labelTotalHeadings.AutoSize = true;
            this.labelTotalHeadings.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTotalHeadings.Location = new System.Drawing.Point(80, 120);
            this.labelTotalHeadings.Name = "labelTotalHeadings";
            this.labelTotalHeadings.Size = new System.Drawing.Size(12, 12);
            this.labelTotalHeadings.TabIndex = 9;
            this.labelTotalHeadings.Text = "0";
            // 
            // labelUploadTime
            // 
            this.labelUploadTime.AutoSize = true;
            this.labelUploadTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUploadTime.Location = new System.Drawing.Point(80, 95);
            this.labelUploadTime.Name = "labelUploadTime";
            this.labelUploadTime.Size = new System.Drawing.Size(12, 12);
            this.labelUploadTime.TabIndex = 8;
            this.labelUploadTime.Text = "-";
            // 
            // labelFileSize
            // 
            this.labelFileSize.AutoSize = true;
            this.labelFileSize.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFileSize.Location = new System.Drawing.Point(80, 70);
            this.labelFileSize.Name = "labelFileSize";
            this.labelFileSize.Size = new System.Drawing.Size(12, 12);
            this.labelFileSize.TabIndex = 7;
            this.labelFileSize.Text = "-";
            // 
            // labelFileType
            // 
            this.labelFileType.AutoSize = true;
            this.labelFileType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFileType.Location = new System.Drawing.Point(80, 45);
            this.labelFileType.Name = "labelFileType";
            this.labelFileType.Size = new System.Drawing.Size(12, 12);
            this.labelFileType.TabIndex = 6;
            this.labelFileType.Text = "-";
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFileName.Location = new System.Drawing.Point(80, 20);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(12, 12);
            this.labelFileName.TabIndex = 5;
            this.labelFileName.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "标题数量:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "上传时间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "文件大小:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "文件类型:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件名称:";
            // 
            // groupBoxContent
            // 
            this.groupBoxContent.Controls.Add(this.labelContentLength);
            this.groupBoxContent.Controls.Add(this.buttonCopyContent);
            this.groupBoxContent.Controls.Add(this.textBoxContent);
            this.groupBoxContent.Controls.Add(this.labelSelectedHeading);
            this.groupBoxContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxContent.Location = new System.Drawing.Point(0, 0);
            this.groupBoxContent.Name = "groupBoxContent";
            this.groupBoxContent.Size = new System.Drawing.Size(580, 711);
            this.groupBoxContent.TabIndex = 0;
            this.groupBoxContent.TabStop = false;
            this.groupBoxContent.Text = "标题内容";
            // 
            // labelContentLength
            // 
            this.labelContentLength.AutoSize = true;
            this.labelContentLength.ForeColor = System.Drawing.Color.Gray;
            this.labelContentLength.Location = new System.Drawing.Point(15, 635);
            this.labelContentLength.Name = "labelContentLength";
            this.labelContentLength.Size = new System.Drawing.Size(101, 12);
            this.labelContentLength.TabIndex = 3;
            this.labelContentLength.Text = "内容长度: 0 字符";
            // 
            // buttonCopyContent
            // 
            this.buttonCopyContent.Enabled = false;
            this.buttonCopyContent.Location = new System.Drawing.Point(480, 630);
            this.buttonCopyContent.Name = "buttonCopyContent";
            this.buttonCopyContent.Size = new System.Drawing.Size(80, 25);
            this.buttonCopyContent.TabIndex = 2;
            this.buttonCopyContent.Text = "复制内容";
            this.buttonCopyContent.UseVisualStyleBackColor = true;
            this.buttonCopyContent.Click += new System.EventHandler(this.buttonCopyContent_Click);
            // 
            // textBoxContent
            // 
            this.textBoxContent.Location = new System.Drawing.Point(15, 50);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.ReadOnly = true;
            this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContent.Size = new System.Drawing.Size(545, 570);
            this.textBoxContent.TabIndex = 1;
            // 
            // labelSelectedHeading
            // 
            this.labelSelectedHeading.AutoSize = true;
            this.labelSelectedHeading.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelectedHeading.Location = new System.Drawing.Point(15, 25);
            this.labelSelectedHeading.Name = "labelSelectedHeading";
            this.labelSelectedHeading.Size = new System.Drawing.Size(136, 14);
            this.labelSelectedHeading.TabIndex = 0;
            this.labelSelectedHeading.Text = "请选择一个标题...";
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(909, 675);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 30);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // DocumentDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(984, 711);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.splitContainer1);
            this.MinimizeBox = false;
            this.Name = "DocumentDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文档详情";
            this.Load += new System.EventHandler(this.DocumentDetailForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxTree.ResumeLayout(false);
            this.groupBoxTree.PerformLayout();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxContent.ResumeLayout(false);
            this.groupBoxContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxTree;
        private System.Windows.Forms.GroupBox groupBoxContent;
        private System.Windows.Forms.Label labelTotalHeadings;
        private System.Windows.Forms.Label labelUploadTime;
        private System.Windows.Forms.Label labelFileSize;
        private System.Windows.Forms.Label labelFileType;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeViewHeadings;
        private System.Windows.Forms.TextBox textBoxContent;
        private System.Windows.Forms.Label labelSelectedHeading;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCopyContent;
        private System.Windows.Forms.Label labelContentLength;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearchResult;
    }
}