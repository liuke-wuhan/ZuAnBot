
namespace ZuAnBot_WinForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label_kuangpen = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_qingpen = new System.Windows.Forms.Label();
            this.checkBox_all = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_perWord = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_kuangpen, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_qingpen, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_all, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_perWord, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(317, 192);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "F10";
            // 
            // label_kuangpen
            // 
            this.label_kuangpen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_kuangpen.AutoSize = true;
            this.label_kuangpen.Location = new System.Drawing.Point(219, 88);
            this.label_kuangpen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_kuangpen.Name = "label_kuangpen";
            this.label_kuangpen.Size = new System.Drawing.Size(37, 15);
            this.label_kuangpen.TabIndex = 2;
            this.label_kuangpen.Text = "狂喷";
            this.toolTip1.SetToolTip(this.label_kuangpen, "会被LOL屏蔽、禁言。可在LOL客户端聊天中使用");
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 88);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "F3";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "F2";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(56, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "按键";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(214, 7);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "功能";
            // 
            // label_qingpen
            // 
            this.label_qingpen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_qingpen.AutoSize = true;
            this.label_qingpen.Location = new System.Drawing.Point(219, 50);
            this.label_qingpen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_qingpen.Name = "label_qingpen";
            this.label_qingpen.Size = new System.Drawing.Size(37, 15);
            this.label_qingpen.TabIndex = 0;
            this.label_qingpen.Text = "轻喷";
            this.toolTip1.SetToolTip(this.label_qingpen, "不会被LOL屏蔽、禁言");
            // 
            // checkBox_all
            // 
            this.checkBox_all.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox_all.AutoSize = true;
            this.checkBox_all.Location = new System.Drawing.Point(201, 124);
            this.checkBox_all.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_all.Name = "checkBox_all";
            this.checkBox_all.Size = new System.Drawing.Size(74, 19);
            this.checkBox_all.TabIndex = 4;
            this.checkBox_all.Text = "所有人";
            this.toolTip1.SetToolTip(this.checkBox_all, "是否发送所有人消息");
            this.checkBox_all.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "欢迎来到祖安";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 164);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "F11";
            // 
            // checkBox_perWord
            // 
            this.checkBox_perWord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox_perWord.AutoSize = true;
            this.checkBox_perWord.Checked = true;
            this.checkBox_perWord.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_perWord.Location = new System.Drawing.Point(193, 162);
            this.checkBox_perWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_perWord.Name = "checkBox_perWord";
            this.checkBox_perWord.Size = new System.Drawing.Size(89, 19);
            this.checkBox_perWord.TabIndex = 4;
            this.checkBox_perWord.Text = "逐字发送";
            this.checkBox_perWord.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 193);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "欢迎来到祖安";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_kuangpen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_qingpen;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox_all;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox_perWord;
    }
}

