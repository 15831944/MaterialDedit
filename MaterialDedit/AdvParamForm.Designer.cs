namespace MaterialDedit
{
    partial class AdvParamForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.partInPartCheckBox = new System.Windows.Forms.CheckBox();
            this.tolTextBox = new System.Windows.Forms.TextBox();
            this.rotTextBox = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.evalTextBox = new System.Windows.Forms.TextBox();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.optimizationCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "零件旋转数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "公差";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // partInPartCheckBox
            // 
            this.partInPartCheckBox.AutoSize = true;
            this.partInPartCheckBox.Location = new System.Drawing.Point(116, 151);
            this.partInPartCheckBox.Name = "partInPartCheckBox";
            this.partInPartCheckBox.Size = new System.Drawing.Size(72, 16);
            this.partInPartCheckBox.TabIndex = 1;
            this.partInPartCheckBox.Text = "零件紧靠";
            this.partInPartCheckBox.UseVisualStyleBackColor = true;
            // 
            // tolTextBox
            // 
            this.tolTextBox.Location = new System.Drawing.Point(116, 20);
            this.tolTextBox.Name = "tolTextBox";
            this.tolTextBox.Size = new System.Drawing.Size(147, 21);
            this.tolTextBox.TabIndex = 2;
            // 
            // rotTextBox
            // 
            this.rotTextBox.Location = new System.Drawing.Point(116, 50);
            this.rotTextBox.Name = "rotTextBox";
            this.rotTextBox.Size = new System.Drawing.Size(147, 21);
            this.rotTextBox.TabIndex = 2;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(44, 207);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(167, 207);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "排版时间(秒)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "评估因子";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // evalTextBox
            // 
            this.evalTextBox.Location = new System.Drawing.Point(116, 78);
            this.evalTextBox.Name = "evalTextBox";
            this.evalTextBox.Size = new System.Drawing.Size(147, 21);
            this.evalTextBox.TabIndex = 2;
            // 
            // timeTextBox
            // 
            this.timeTextBox.Location = new System.Drawing.Point(116, 105);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(147, 21);
            this.timeTextBox.TabIndex = 2;
            // 
            // optimizationCheckBox
            // 
            this.optimizationCheckBox.AutoSize = true;
            this.optimizationCheckBox.Location = new System.Drawing.Point(116, 173);
            this.optimizationCheckBox.Name = "optimizationCheckBox";
            this.optimizationCheckBox.Size = new System.Drawing.Size(72, 16);
            this.optimizationCheckBox.TabIndex = 1;
            this.optimizationCheckBox.Text = "允许优化";
            this.optimizationCheckBox.UseVisualStyleBackColor = true;
            // 
            // AdvParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(247)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(289, 236);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.timeTextBox);
            this.Controls.Add(this.rotTextBox);
            this.Controls.Add(this.evalTextBox);
            this.Controls.Add(this.tolTextBox);
            this.Controls.Add(this.optimizationCheckBox);
            this.Controls.Add(this.partInPartCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AdvParamForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "高级设置";
            this.Load += new System.EventHandler(this.AdvParamForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox partInPartCheckBox;
        private System.Windows.Forms.TextBox tolTextBox;
        private System.Windows.Forms.TextBox rotTextBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox evalTextBox;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.CheckBox optimizationCheckBox;
    }
}