namespace MaterialDedit
{
    partial class NestResultForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nestPartTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.subPartTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.partListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quitBtn = new System.Windows.Forms.Button();
            this.remMatBtn = new System.Windows.Forms.Button();
            this.saveShtBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btQrCode = new System.Windows.Forms.Button();
            this.shtListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.viewShtBtn = new System.Windows.Forms.Button();
            this.utilTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.matListView = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.stopBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.shtPreViewWnd = new System.Windows.Forms.Panel();
            this.partRegionTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.nestProgressBar = new System.Windows.Forms.ProgressBar();
            this.allResultBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.costTimeTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nestPartTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.subPartTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.partListView);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 203);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "排版的零件数据";
            // 
            // nestPartTextBox
            // 
            this.nestPartTextBox.Location = new System.Drawing.Point(300, 18);
            this.nestPartTextBox.Name = "nestPartTextBox";
            this.nestPartTextBox.ReadOnly = true;
            this.nestPartTextBox.Size = new System.Drawing.Size(68, 21);
            this.nestPartTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "排版零件个数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // subPartTextBox
            // 
            this.subPartTextBox.Location = new System.Drawing.Point(98, 18);
            this.subPartTextBox.Name = "subPartTextBox";
            this.subPartTextBox.ReadOnly = true;
            this.subPartTextBox.Size = new System.Drawing.Size(64, 21);
            this.subPartTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "提交零件个数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // partListView
            // 
            this.partListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.partListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.partListView.FullRowSelect = true;
            this.partListView.GridLines = true;
            this.partListView.HideSelection = false;
            this.partListView.Location = new System.Drawing.Point(0, 48);
            this.partListView.Name = "partListView";
            this.partListView.Size = new System.Drawing.Size(376, 155);
            this.partListView.TabIndex = 0;
            this.partListView.UseCompatibleStateImageBehavior = false;
            this.partListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "序号";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "零件名称";
            this.columnHeader6.Width = 112;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "提交个数";
            this.columnHeader7.Width = 93;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "排版个数";
            this.columnHeader8.Width = 101;
            // 
            // quitBtn
            // 
            this.quitBtn.Location = new System.Drawing.Point(978, 638);
            this.quitBtn.Name = "quitBtn";
            this.quitBtn.Size = new System.Drawing.Size(69, 35);
            this.quitBtn.TabIndex = 3;
            this.quitBtn.Text = "退出";
            this.quitBtn.UseVisualStyleBackColor = true;
            this.quitBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // remMatBtn
            // 
            this.remMatBtn.Location = new System.Drawing.Point(0, 157);
            this.remMatBtn.Name = "remMatBtn";
            this.remMatBtn.Size = new System.Drawing.Size(99, 23);
            this.remMatBtn.TabIndex = 5;
            this.remMatBtn.Text = "Generate Remnant Material";
            this.remMatBtn.UseVisualStyleBackColor = true;
            this.remMatBtn.Visible = false;
            this.remMatBtn.Click += new System.EventHandler(this.remMatBtn_Click);
            // 
            // saveShtBtn
            // 
            this.saveShtBtn.Location = new System.Drawing.Point(293, 157);
            this.saveShtBtn.Name = "saveShtBtn";
            this.saveShtBtn.Size = new System.Drawing.Size(75, 23);
            this.saveShtBtn.TabIndex = 4;
            this.saveShtBtn.Text = "保存结果";
            this.saveShtBtn.UseVisualStyleBackColor = true;
            this.saveShtBtn.Click += new System.EventHandler(this.saveShtBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btQrCode);
            this.groupBox1.Controls.Add(this.remMatBtn);
            this.groupBox1.Controls.Add(this.saveShtBtn);
            this.groupBox1.Controls.Add(this.shtListView);
            this.groupBox1.Controls.Add(this.viewShtBtn);
            this.groupBox1.Location = new System.Drawing.Point(9, 447);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 181);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生成的列表";
            // 
            // btQrCode
            // 
            this.btQrCode.Location = new System.Drawing.Point(212, 157);
            this.btQrCode.Name = "btQrCode";
            this.btQrCode.Size = new System.Drawing.Size(75, 23);
            this.btQrCode.TabIndex = 6;
            this.btQrCode.Text = "生成标签";
            this.btQrCode.UseVisualStyleBackColor = true;
            this.btQrCode.Click += new System.EventHandler(this.btQrCode_Click);
            // 
            // shtListView
            // 
            this.shtListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.shtListView.FullRowSelect = true;
            this.shtListView.GridLines = true;
            this.shtListView.HideSelection = false;
            this.shtListView.Location = new System.Drawing.Point(0, 25);
            this.shtListView.Name = "shtListView";
            this.shtListView.Size = new System.Drawing.Size(368, 127);
            this.shtListView.TabIndex = 2;
            this.shtListView.UseCompatibleStateImageBehavior = false;
            this.shtListView.View = System.Windows.Forms.View.Details;
            this.shtListView.SelectedIndexChanged += new System.EventHandler(this.shtListView_SelectedIndexChanged);
            this.shtListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.shtListView_MouseDoubleClick_1);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "表名";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "个数";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "材料名称";
            this.columnHeader4.Width = 100;
            // 
            // viewShtBtn
            // 
            this.viewShtBtn.Location = new System.Drawing.Point(118, 158);
            this.viewShtBtn.Name = "viewShtBtn";
            this.viewShtBtn.Size = new System.Drawing.Size(75, 23);
            this.viewShtBtn.TabIndex = 3;
            this.viewShtBtn.Text = "预览";
            this.viewShtBtn.UseVisualStyleBackColor = true;
            this.viewShtBtn.Click += new System.EventHandler(this.viewShtBtn_Click);
            // 
            // utilTextBox
            // 
            this.utilTextBox.Location = new System.Drawing.Point(277, 13);
            this.utilTextBox.Name = "utilTextBox";
            this.utilTextBox.ReadOnly = true;
            this.utilTextBox.Size = new System.Drawing.Size(91, 21);
            this.utilTextBox.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.groupBox3.Controls.Add(this.matListView);
            this.groupBox3.Controls.Add(this.utilTextBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(9, 235);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(368, 199);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "材料使用情况";
            // 
            // matListView
            // 
            this.matListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.matListView.FullRowSelect = true;
            this.matListView.GridLines = true;
            this.matListView.HideSelection = false;
            this.matListView.Location = new System.Drawing.Point(0, 49);
            this.matListView.Name = "matListView";
            this.matListView.Size = new System.Drawing.Size(368, 150);
            this.matListView.TabIndex = 2;
            this.matListView.UseCompatibleStateImageBehavior = false;
            this.matListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "序号";
            this.columnHeader9.Width = 58;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "材料名称";
            this.columnHeader10.Width = 80;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "提交个数";
            this.columnHeader11.Width = 110;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "消费个数";
            this.columnHeader12.Width = 117;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(169, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "材料总利用率(%):";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(903, 638);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(69, 35);
            this.stopBtn.TabIndex = 3;
            this.stopBtn.Text = "停止排版";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.shtPreViewWnd);
            this.groupBox4.Controls.Add(this.partRegionTextBox);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(392, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(653, 616);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "结果预览";
            // 
            // shtPreViewWnd
            // 
            this.shtPreViewWnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shtPreViewWnd.Location = new System.Drawing.Point(0, 20);
            this.shtPreViewWnd.Name = "shtPreViewWnd";
            this.shtPreViewWnd.Size = new System.Drawing.Size(653, 566);
            this.shtPreViewWnd.TabIndex = 7;
            this.shtPreViewWnd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.shtViewWnd_MouseDown);
            this.shtPreViewWnd.MouseEnter += new System.EventHandler(this.shtViewWnd_MouseEnter);
            this.shtPreViewWnd.MouseMove += new System.Windows.Forms.MouseEventHandler(this.shtViewWnd_MouseMove);
            this.shtPreViewWnd.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.shtViewWnd_MouseWheel);
            // 
            // partRegionTextBox
            // 
            this.partRegionTextBox.Location = new System.Drawing.Point(427, 592);
            this.partRegionTextBox.Name = "partRegionTextBox";
            this.partRegionTextBox.ReadOnly = true;
            this.partRegionTextBox.Size = new System.Drawing.Size(226, 21);
            this.partRegionTextBox.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 595);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "零件矩形区域:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkTimer
            // 
            this.checkTimer.Enabled = true;
            this.checkTimer.Interval = 1000;
            this.checkTimer.Tick += new System.EventHandler(this.checkTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 649);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "状态:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(34, 646);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.Size = new System.Drawing.Size(74, 21);
            this.statusTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 650);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "结果个数:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // countTextBox
            // 
            this.countTextBox.Location = new System.Drawing.Point(170, 646);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.ReadOnly = true;
            this.countTextBox.Size = new System.Drawing.Size(74, 21);
            this.countTextBox.TabIndex = 1;
            // 
            // nestProgressBar
            // 
            this.nestProgressBar.Location = new System.Drawing.Point(436, 646);
            this.nestProgressBar.MarqueeAnimationSpeed = 30;
            this.nestProgressBar.Name = "nestProgressBar";
            this.nestProgressBar.Size = new System.Drawing.Size(365, 23);
            this.nestProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.nestProgressBar.TabIndex = 6;
            // 
            // allResultBtn
            // 
            this.allResultBtn.Enabled = false;
            this.allResultBtn.Location = new System.Drawing.Point(807, 639);
            this.allResultBtn.Name = "allResultBtn";
            this.allResultBtn.Size = new System.Drawing.Size(87, 35);
            this.allResultBtn.TabIndex = 3;
            this.allResultBtn.Text = "All Results";
            this.allResultBtn.UseVisualStyleBackColor = true;
            this.allResultBtn.Visible = false;
            this.allResultBtn.Click += new System.EventHandler(this.allResultBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 650);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "消耗时间:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // costTimeTextBox
            // 
            this.costTimeTextBox.Location = new System.Drawing.Point(304, 646);
            this.costTimeTextBox.Name = "costTimeTextBox";
            this.costTimeTextBox.ReadOnly = true;
            this.costTimeTextBox.Size = new System.Drawing.Size(74, 21);
            this.costTimeTextBox.TabIndex = 1;
            // 
            // NestResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1057, 684);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.costTimeTextBox);
            this.Controls.Add(this.countTextBox);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.nestProgressBar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.allResultBtn);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.quitBtn);
            this.Controls.Add(this.groupBox2);
            this.Name = "NestResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "排版结果";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NestResultForm_FormClosing);
            this.Load += new System.EventHandler(this.NestResultForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NestResultForm_Paint);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button quitBtn;
        private System.Windows.Forms.TextBox subPartTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView partListView;
        private System.Windows.Forms.TextBox nestPartTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button remMatBtn;
        private System.Windows.Forms.Button saveShtBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button viewShtBtn;
        private System.Windows.Forms.ListView shtListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox utilTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView matListView;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel shtPreViewWnd;
        private System.Windows.Forms.Timer checkTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox countTextBox;
        private System.Windows.Forms.ProgressBar nestProgressBar;
        private System.Windows.Forms.Button allResultBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox costTimeTextBox;
        private System.Windows.Forms.TextBox partRegionTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btQrCode;
    }
}