namespace MaterialDedit
{
    partial class RemnantMatInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemnantMatInfoForm));
            this.matViewWnd = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.posTextBox = new System.Windows.Forms.TextBox();
            this.polyTreeView = new System.Windows.Forms.TreeView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mergeDisTextBox = new System.Windows.Forms.TextBox();
            this.reGenBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.saveDxfBtn = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // matViewWnd
            // 
            this.matViewWnd.Location = new System.Drawing.Point(7, 40);
            this.matViewWnd.Name = "matViewWnd";
            this.matViewWnd.Size = new System.Drawing.Size(556, 395);
            this.matViewWnd.TabIndex = 7;
            this.matViewWnd.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.matViewWnd_MouseWheel);
            this.matViewWnd.MouseMove += new System.Windows.Forms.MouseEventHandler(this.matViewWnd_MouseMove);
            this.matViewWnd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.matViewWnd_MouseDown);
            this.matViewWnd.MouseEnter += new System.EventHandler(this.matViewWnd_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current Cursor Coordinate:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.matViewWnd);
            this.groupBox2.Controls.Add(this.posTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(204, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 443);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Remnant Material Preview";
            // 
            // posTextBox
            // 
            this.posTextBox.Location = new System.Drawing.Point(241, 15);
            this.posTextBox.Name = "posTextBox";
            this.posTextBox.ReadOnly = true;
            this.posTextBox.Size = new System.Drawing.Size(191, 21);
            this.posTextBox.TabIndex = 16;
            // 
            // polyTreeView
            // 
            this.polyTreeView.HideSelection = false;
            this.polyTreeView.ImageIndex = 0;
            this.polyTreeView.ImageList = this.imageList2;
            this.polyTreeView.Location = new System.Drawing.Point(11, 22);
            this.polyTreeView.Name = "polyTreeView";
            this.polyTreeView.SelectedImageIndex = 0;
            this.polyTreeView.Size = new System.Drawing.Size(168, 468);
            this.polyTreeView.TabIndex = 2;
            this.polyTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.polyTreeView_AfterSelect);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Silver;
            this.imageList2.Images.SetKeyName(0, "polygon.bmp");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.polyTreeView);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 498);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Polygons In Remnant Material";
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(697, 508);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 17;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mergeDisTextBox);
            this.groupBox3.Controls.Add(this.reGenBtn);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(204, 453);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(567, 49);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Re-generate Remnant Material";
            // 
            // mergeDisTextBox
            // 
            this.mergeDisTextBox.Location = new System.Drawing.Point(189, 20);
            this.mergeDisTextBox.Name = "mergeDisTextBox";
            this.mergeDisTextBox.Size = new System.Drawing.Size(96, 21);
            this.mergeDisTextBox.TabIndex = 16;
            // 
            // reGenBtn
            // 
            this.reGenBtn.Location = new System.Drawing.Point(301, 18);
            this.reGenBtn.Name = "reGenBtn";
            this.reGenBtn.Size = new System.Drawing.Size(101, 23);
            this.reGenBtn.TabIndex = 17;
            this.reGenBtn.Text = "Re-generate";
            this.reGenBtn.UseVisualStyleBackColor = true;
            this.reGenBtn.Click += new System.EventHandler(this.reGenBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Merge Distance:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveDxfBtn
            // 
            this.saveDxfBtn.Location = new System.Drawing.Point(590, 508);
            this.saveDxfBtn.Name = "saveDxfBtn";
            this.saveDxfBtn.Size = new System.Drawing.Size(101, 23);
            this.saveDxfBtn.TabIndex = 17;
            this.saveDxfBtn.Text = "SaveAs Dxf";
            this.saveDxfBtn.UseVisualStyleBackColor = true;
            this.saveDxfBtn.Click += new System.EventHandler(this.saveDxfBtn_Click);
            // 
            // RemnantMatInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(778, 534);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.saveDxfBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RemnantMatInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remnant Material Info";
            this.Load += new System.EventHandler(this.RemnantMatInfoForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RemnantMatInfoForm_Paint);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemnantMatInfoForm_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel matViewWnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox posTextBox;
        private System.Windows.Forms.TreeView polyTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox mergeDisTextBox;
        private System.Windows.Forms.Button reGenBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveDxfBtn;
        private System.Windows.Forms.ImageList imageList2;
    }
}