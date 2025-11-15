using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordCopilotChat
{
    /// <summary>
    /// 密码输入对话框
    /// </summary>
    public partial class PasswordDialog : Form
    {
        public string Password { get; private set; }
        public bool IsExportMode { get; private set; }

        // 控件定义迁移到 Designer 文件，便于使用可视化设计器

        public PasswordDialog() : this(true)
        {
        }

        public PasswordDialog(bool isExportMode = true)
        {
            IsExportMode = isExportMode;
            InitializeComponent();
            SetupUI();
        }

        // InitializeComponent 迁移到 Designer 文件

        private void SetupUI()
        {
            // 根据模式应用界面状态
            ApplyMode();

            // 初始状态
            btnOK.Enabled = false;
            txtPassword.Focus();
        }

        private void ApplyMode()
        {
            if (IsExportMode)
            {
                // 导出模式
                this.Text = "设置密码";
                //this.Size = new Size(350, 220);
                this.lblTitle.Text = "请为导出文件设置密码：";

                this.lblConfirmPassword.Visible = true;
                this.txtConfirmPassword.Visible = true;

                //this.chkShowPassword.Location = new Point(75, 105);
                //this.btnOK.Location = new Point(120, 135);
                //this.btnCancel.Location = new Point(200, 135);
            }
            else
            {
                // 导入模式
                this.Text = "输入密码";
                //this.Size = new Size(350, 180);
                this.lblTitle.Text = "请输入文件密码：";

                this.lblConfirmPassword.Visible = false;
                this.txtConfirmPassword.Visible = false;

                // 上移复选框和按钮
                //this.chkShowPassword.Location = new Point(75, 75);
                //this.btnOK.Location = new Point(120, 105);
                //this.btnCancel.Location = new Point(200, 105);
            }
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void ValidateInput()
        {
            if (IsExportMode)
            {
                // 导出模式：密码不能为空，且两次输入必须一致
                string hint = string.Empty;
                bool isValid = true;
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    isValid = false;
                    hint = "请输入密码";
                }
                else if (txtPassword.Text.Length < 6)
                {
                    isValid = false;
                    hint = "密码长度不能少于6位";
                }
                else if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    isValid = false;
                    hint = "两次输入不一致";
                }
                lblHint.Text = hint;
                btnOK.Enabled = isValid;
            }
            else
            {
                // 导入模式：密码不能为空
                string hint = string.Empty;
                bool isValid = !string.IsNullOrEmpty(txtPassword.Text);
                if (!isValid)
                {
                    hint = "请输入密码";
                }
                lblHint.Text = hint;
                btnOK.Enabled = isValid;
            }
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            if (IsExportMode)
            {
                txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (IsExportMode)
            {
                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("密码长度不能少于6位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("两次输入的密码不一致", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    txtConfirmPassword.SelectAll();
                    return;
                }
            }

            Password = txtPassword.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 显示导出密码对话框
        /// </summary>
        /// <param name="parent">父窗体</param>
        /// <returns>用户输入的密码，如果取消则返回null</returns>
        public static string ShowExportDialog(IWin32Window parent = null)
        {
            using (var dialog = new PasswordDialog(true))
            {
                if (dialog.ShowDialog(parent) == DialogResult.OK)
                {
                    return dialog.Password;
                }
                return null;
            }
        }

        /// <summary>
        /// 显示导入密码对话框
        /// </summary>
        /// <param name="parent">父窗体</param>
        /// <returns>用户输入的密码，如果取消则返回null</returns>
        public static string ShowImportDialog(IWin32Window parent = null)
        {
            using (var dialog = new PasswordDialog(false))
            {
                if (dialog.ShowDialog(parent) == DialogResult.OK)
                {
                    return dialog.Password;
                }
                return null;
            }
        }
    }
} 