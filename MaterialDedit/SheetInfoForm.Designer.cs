namespace MaterialDedit
{
    partial class SheetInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheetInfoForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.partPmtTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.viewTypeComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.shtViewWnd = new System.Windows.Forms.Panel();
            this.posTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.partPmtTreeView);
            this.groupBox1.Controls.Add(this.viewTypeComboBox);
            this.groupBox1.Location = new System.Drawing.Point(9, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 665);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "零件信息";
            // 
            // partPmtTreeView
            // 
            this.partPmtTreeView.HideSelection = false;
            this.partPmtTreeView.ImageIndex = 0;
            this.partPmtTreeView.ImageList = this.imageList1;
            this.partPmtTreeView.Location = new System.Drawing.Point(15, 57);
            this.partPmtTreeView.Name = "partPmtTreeView";
            this.partPmtTreeView.SelectedImageIndex = 0;
            this.partPmtTreeView.Size = new System.Drawing.Size(259, 595);
            this.partPmtTreeView.TabIndex = 2;
            this.partPmtTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.partPmtTreeView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Silver;
            this.imageList1.Images.SetKeyName(0, "part.bmp");
            this.imageList1.Images.SetKeyName(1, "partpmt.bmp");
            // 
            // viewTypeComboBox
            // 
            this.viewTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewTypeComboBox.FormattingEnabled = true;
            this.viewTypeComboBox.Location = new System.Drawing.Point(15, 25);
            this.viewTypeComboBox.Name = "viewTypeComboBox";
            this.viewTypeComboBox.Size = new System.Drawing.Size(259, 20);
            this.viewTypeComboBox.TabIndex = 1;
            this.viewTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.viewTypeComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.shtViewWnd);
            this.groupBox2.Controls.Add(this.posTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(316, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 661);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表预览";
            // 
            // shtViewWnd
            // 
            this.shtViewWnd.Location = new System.Drawing.Point(11, 53);
            this.shtViewWnd.Name = "shtViewWnd";
            this.shtViewWnd.Size = new System.Drawing.Size(557, 595);
            this.shtViewWnd.TabIndex = 7;
            this.shtViewWnd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.shtViewWnd_MouseDown);
            this.shtViewWnd.MouseEnter += new System.EventHandler(this.shtViewWnd_MouseEnter);
            this.shtViewWnd.MouseMove += new System.Windows.Forms.MouseEventHandler(this.shtViewWnd_MouseMove);
            this.shtViewWnd.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.shtViewWnd_MouseWheel);
            // 
            // posTextBox
            // 
            this.posTextBox.Location = new System.Drawing.Point(345, 21);
            this.posTextBox.Name = "posTextBox";
            this.posTextBox.ReadOnly = true;
            this.posTextBox.Size = new System.Drawing.Size(223, 21);
            this.posTextBox.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "当前坐标：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(824, 679);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.Text = "关闭";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // SheetInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(911, 707);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SheetInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sheet Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SheetInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.SheetInfoForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SheetInfoForm_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox viewTypeComboBox;
        private System.Windows.Forms.TreeView partPmtTreeView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox posTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel shtViewWnd;
        private System.Windows.Forms.ImageList imageList1;
    }
}