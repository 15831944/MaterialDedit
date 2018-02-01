namespace MaterialDedit
{
    partial class DemoForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRemoveMaterial = new System.Windows.Forms.Button();
            this.matPreviewWnd = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.saveTaskBtn = new System.Windows.Forms.Button();
            this.loadTaskBtn = new System.Windows.Forms.Button();
            this.configBtn = new System.Windows.Forms.Button();
            this.executeBtn = new System.Windows.Forms.Button();
            this.loadMatBtn = new System.Windows.Forms.Button();
            this.newMatBtn = new System.Windows.Forms.Button();
            this.matListView = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLoadPart = new System.Windows.Forms.Label();
            this.partPreviewWnd = new System.Windows.Forms.Panel();
            this.addPartBtn = new System.Windows.Forms.Button();
            this.editPartBtn = new System.Windows.Forms.Button();
            this.partListView = new System.Windows.Forms.ListView();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PartName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PartCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PartRotationAngle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PartSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Order = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.delPartBtn = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnRemoveMaterial);
            this.groupBox2.Controls.Add(this.matPreviewWnd);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.loadMatBtn);
            this.groupBox2.Controls.Add(this.newMatBtn);
            this.groupBox2.Controls.Add(this.matListView);
            this.groupBox2.Location = new System.Drawing.Point(9, 287);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(899, 338);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "准备用于排版的材料";
            // 
            // btnRemoveMaterial
            // 
            this.btnRemoveMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveMaterial.Location = new System.Drawing.Point(517, 273);
            this.btnRemoveMaterial.Name = "btnRemoveMaterial";
            this.btnRemoveMaterial.Size = new System.Drawing.Size(88, 23);
            this.btnRemoveMaterial.TabIndex = 3;
            this.btnRemoveMaterial.Text = "移除材料";
            this.btnRemoveMaterial.UseVisualStyleBackColor = true;
            this.btnRemoveMaterial.Click += new System.EventHandler(this.btnRemoveMaterial_Click);
            // 
            // matPreviewWnd
            // 
            this.matPreviewWnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.matPreviewWnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.matPreviewWnd.Location = new System.Drawing.Point(614, 23);
            this.matPreviewWnd.Name = "matPreviewWnd";
            this.matPreviewWnd.Size = new System.Drawing.Size(285, 308);
            this.matPreviewWnd.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.saveTaskBtn);
            this.panel3.Controls.Add(this.loadTaskBtn);
            this.panel3.Controls.Add(this.configBtn);
            this.panel3.Controls.Add(this.executeBtn);
            this.panel3.Location = new System.Drawing.Point(3, 298);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(605, 40);
            this.panel3.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(405, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "重排历史作业";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // saveTaskBtn
            // 
            this.saveTaskBtn.Location = new System.Drawing.Point(116, 5);
            this.saveTaskBtn.Name = "saveTaskBtn";
            this.saveTaskBtn.Size = new System.Drawing.Size(75, 33);
            this.saveTaskBtn.TabIndex = 0;
            this.saveTaskBtn.Text = "Save Task";
            this.saveTaskBtn.UseVisualStyleBackColor = true;
            this.saveTaskBtn.Visible = false;
            this.saveTaskBtn.Click += new System.EventHandler(this.saveTaskBtn_Click);
            // 
            // loadTaskBtn
            // 
            this.loadTaskBtn.Location = new System.Drawing.Point(210, 5);
            this.loadTaskBtn.Name = "loadTaskBtn";
            this.loadTaskBtn.Size = new System.Drawing.Size(75, 33);
            this.loadTaskBtn.TabIndex = 0;
            this.loadTaskBtn.Text = "Load Task";
            this.loadTaskBtn.UseVisualStyleBackColor = true;
            this.loadTaskBtn.Visible = false;
            this.loadTaskBtn.Click += new System.EventHandler(this.loadTaskBtn_Click);
            // 
            // configBtn
            // 
            this.configBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.configBtn.Location = new System.Drawing.Point(514, 7);
            this.configBtn.Name = "configBtn";
            this.configBtn.Size = new System.Drawing.Size(88, 33);
            this.configBtn.TabIndex = 0;
            this.configBtn.Text = "参数设置";
            this.configBtn.UseVisualStyleBackColor = true;
            this.configBtn.Click += new System.EventHandler(this.configBtn_Click);
            // 
            // executeBtn
            // 
            this.executeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.executeBtn.Location = new System.Drawing.Point(303, 5);
            this.executeBtn.Name = "executeBtn";
            this.executeBtn.Size = new System.Drawing.Size(88, 33);
            this.executeBtn.TabIndex = 0;
            this.executeBtn.Text = "启动排版";
            this.executeBtn.UseVisualStyleBackColor = true;
            this.executeBtn.Click += new System.EventHandler(this.executeBtn_Click);
            // 
            // loadMatBtn
            // 
            this.loadMatBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadMatBtn.Location = new System.Drawing.Point(158, 278);
            this.loadMatBtn.Name = "loadMatBtn";
            this.loadMatBtn.Size = new System.Drawing.Size(101, 23);
            this.loadMatBtn.TabIndex = 0;
            this.loadMatBtn.Text = "Load From Dxf";
            this.loadMatBtn.UseVisualStyleBackColor = true;
            this.loadMatBtn.Visible = false;
            this.loadMatBtn.Click += new System.EventHandler(this.loadMatBtn_Click);
            // 
            // newMatBtn
            // 
            this.newMatBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.newMatBtn.Location = new System.Drawing.Point(408, 274);
            this.newMatBtn.Name = "newMatBtn";
            this.newMatBtn.Size = new System.Drawing.Size(88, 23);
            this.newMatBtn.TabIndex = 0;
            this.newMatBtn.Text = "添加材料";
            this.newMatBtn.UseVisualStyleBackColor = true;
            this.newMatBtn.Click += new System.EventHandler(this.newMatBtn_Click);
            // 
            // matListView
            // 
            this.matListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.matListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.matListView.FullRowSelect = true;
            this.matListView.GridLines = true;
            this.matListView.HideSelection = false;
            this.matListView.Location = new System.Drawing.Point(6, 23);
            this.matListView.Name = "matListView";
            this.matListView.Size = new System.Drawing.Size(602, 249);
            this.matListView.TabIndex = 0;
            this.matListView.UseCompatibleStateImageBehavior = false;
            this.matListView.View = System.Windows.Forms.View.Details;
            this.matListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.matListView_ItemSelectionChanged);
            this.matListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.matListView_MouseDoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "序号";
            this.columnHeader8.Width = 40;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "材料名称";
            this.columnHeader9.Width = 110;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "材料类型";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "材料高度";
            this.columnHeader11.Width = 120;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "材料宽度";
            this.columnHeader12.Width = 130;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "数量";
            this.columnHeader13.Width = 70;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 269);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLoadPart);
            this.groupBox1.Controls.Add(this.partPreviewWnd);
            this.groupBox1.Controls.Add(this.addPartBtn);
            this.groupBox1.Controls.Add(this.editPartBtn);
            this.groupBox1.Controls.Add(this.partListView);
            this.groupBox1.Controls.Add(this.delPartBtn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(896, 269);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "准备排版的零件";
            // 
            // lblLoadPart
            // 
            this.lblLoadPart.AutoSize = true;
            this.lblLoadPart.Location = new System.Drawing.Point(219, 244);
            this.lblLoadPart.Name = "lblLoadPart";
            this.lblLoadPart.Size = new System.Drawing.Size(0, 12);
            this.lblLoadPart.TabIndex = 4;
            // 
            // partPreviewWnd
            // 
            this.partPreviewWnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.partPreviewWnd.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.partPreviewWnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.partPreviewWnd.Location = new System.Drawing.Point(611, 20);
            this.partPreviewWnd.Name = "partPreviewWnd";
            this.partPreviewWnd.Size = new System.Drawing.Size(285, 242);
            this.partPreviewWnd.TabIndex = 3;
            // 
            // addPartBtn
            // 
            this.addPartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addPartBtn.Location = new System.Drawing.Point(355, 239);
            this.addPartBtn.Name = "addPartBtn";
            this.addPartBtn.Size = new System.Drawing.Size(97, 23);
            this.addPartBtn.TabIndex = 0;
            this.addPartBtn.Text = "选择DXF文件";
            this.addPartBtn.UseVisualStyleBackColor = true;
            this.addPartBtn.Click += new System.EventHandler(this.addPartBtn_Click);
            // 
            // editPartBtn
            // 
            this.editPartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editPartBtn.Location = new System.Drawing.Point(458, 239);
            this.editPartBtn.Name = "editPartBtn";
            this.editPartBtn.Size = new System.Drawing.Size(67, 23);
            this.editPartBtn.TabIndex = 0;
            this.editPartBtn.Text = "零件设置";
            this.editPartBtn.UseVisualStyleBackColor = true;
            this.editPartBtn.Click += new System.EventHandler(this.editPartBtn_Click);
            // 
            // partListView
            // 
            this.partListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.partListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.PartName,
            this.Priority,
            this.PartCount,
            this.PartRotationAngle,
            this.PartSize,
            this.Order});
            this.partListView.FullRowSelect = true;
            this.partListView.GridLines = true;
            this.partListView.HideSelection = false;
            this.partListView.Location = new System.Drawing.Point(3, 20);
            this.partListView.Name = "partListView";
            this.partListView.Size = new System.Drawing.Size(602, 213);
            this.partListView.TabIndex = 0;
            this.partListView.UseCompatibleStateImageBehavior = false;
            this.partListView.View = System.Windows.Forms.View.Details;
            this.partListView.SelectedIndexChanged += new System.EventHandler(this.partListView_SelectedIndexChanged);
            this.partListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.partListView_MouseDoubleClick);
            // 
            // Index
            // 
            this.Index.Tag = "";
            this.Index.Text = "序号";
            this.Index.Width = 46;
            // 
            // PartName
            // 
            this.PartName.Text = "零件名称";
            this.PartName.Width = 130;
            // 
            // Priority
            // 
            this.Priority.Text = "优先级";
            this.Priority.Width = 97;
            // 
            // PartCount
            // 
            this.PartCount.Text = "零件数量";
            this.PartCount.Width = 78;
            // 
            // PartRotationAngle
            // 
            this.PartRotationAngle.Text = "旋转角度";
            this.PartRotationAngle.Width = 77;
            // 
            // PartSize
            // 
            this.PartSize.Text = "零件尺寸";
            this.PartSize.Width = 85;
            // 
            // Order
            // 
            this.Order.Text = "订单号";
            this.Order.Width = 76;
            // 
            // delPartBtn
            // 
            this.delPartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delPartBtn.Location = new System.Drawing.Point(531, 239);
            this.delPartBtn.Name = "delPartBtn";
            this.delPartBtn.Size = new System.Drawing.Size(66, 23);
            this.delPartBtn.TabIndex = 0;
            this.delPartBtn.Text = "移除零件";
            this.delPartBtn.UseVisualStyleBackColor = true;
            this.delPartBtn.Click += new System.EventHandler(this.delPartBtn_Click);
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Name = "DemoForm";
            this.Size = new System.Drawing.Size(911, 628);
            this.Load += new System.EventHandler(this.DemoForm_Load);
            this.SizeChanged += new System.EventHandler(this.DemoForm_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DemoForm_Paint);
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button configBtn;
        private System.Windows.Forms.Button executeBtn;
        private System.Windows.Forms.ListView matListView;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.Button newMatBtn;
        private System.Windows.Forms.Button loadMatBtn;
        private System.Windows.Forms.Button saveTaskBtn;
        private System.Windows.Forms.Button loadTaskBtn;
        private System.Windows.Forms.Panel matPreviewWnd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel partPreviewWnd;
        private System.Windows.Forms.Button addPartBtn;
        private System.Windows.Forms.Button editPartBtn;
        private System.Windows.Forms.ListView partListView;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader PartName;
        private System.Windows.Forms.ColumnHeader Priority;
        private System.Windows.Forms.ColumnHeader PartCount;
        private System.Windows.Forms.ColumnHeader PartRotationAngle;
        private System.Windows.Forms.ColumnHeader PartSize;
        private System.Windows.Forms.Button delPartBtn;
        private System.Windows.Forms.ColumnHeader Order;
        private System.Windows.Forms.Button btnRemoveMaterial;
        private System.Windows.Forms.Label lblLoadPart;
    }
}