namespace MaterialDedit
{
    partial class QrCodeForm
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
            this.panelQrCode = new System.Windows.Forms.Panel();
            this.cbShadowBmp = new System.Windows.Forms.ComboBox();
            this.panelQr = new System.Windows.Forms.Panel();
            this.Paneltext = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelQrCode
            // 
            this.panelQrCode.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelQrCode.Location = new System.Drawing.Point(151, 24);
            this.panelQrCode.Name = "panelQrCode";
            this.panelQrCode.Size = new System.Drawing.Size(400, 271);
            this.panelQrCode.TabIndex = 0;
            // 
            // cbShadowBmp
            // 
            this.cbShadowBmp.FormattingEnabled = true;
            this.cbShadowBmp.Location = new System.Drawing.Point(12, 3);
            this.cbShadowBmp.Name = "cbShadowBmp";
            this.cbShadowBmp.Size = new System.Drawing.Size(121, 20);
            this.cbShadowBmp.TabIndex = 1;
            this.cbShadowBmp.SelectedValueChanged += new System.EventHandler(this.cbShadowBmp_SelectedValueChanged);
            // 
            // panelQr
            // 
            this.panelQr.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelQr.Location = new System.Drawing.Point(12, 170);
            this.panelQr.Name = "panelQr";
            this.panelQr.Size = new System.Drawing.Size(133, 125);
            this.panelQr.TabIndex = 2;
            // 
            // Paneltext
            // 
            this.Paneltext.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Paneltext.Location = new System.Drawing.Point(12, 27);
            this.Paneltext.Name = "Paneltext";
            this.Paneltext.Size = new System.Drawing.Size(133, 122);
            this.Paneltext.TabIndex = 3;
            // 
            // QrCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 365);
            this.Controls.Add(this.Paneltext);
            this.Controls.Add(this.panelQr);
            this.Controls.Add(this.cbShadowBmp);
            this.Controls.Add(this.panelQrCode);
            this.Name = "QrCodeForm";
            this.Text = "QrCodeForm";
            this.Load += new System.EventHandler(this.QrCodeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelQrCode;
        private System.Windows.Forms.ComboBox cbShadowBmp;
        private System.Windows.Forms.Panel panelQr;
        private System.Windows.Forms.Panel Paneltext;
    }
}