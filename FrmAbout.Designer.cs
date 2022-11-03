namespace PairsAssignmentFM.Forms
{
    partial class FrmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.LblPairsV1 = new System.Windows.Forms.Label();
            this.TxtAboutText = new System.Windows.Forms.TextBox();
            this.PicBxPairsLogo = new System.Windows.Forms.PictureBox();
            this.BtnCloseAbout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PicBxPairsLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // LblPairsV1
            // 
            this.LblPairsV1.AutoSize = true;
            this.LblPairsV1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPairsV1.Location = new System.Drawing.Point(231, 24);
            this.LblPairsV1.Name = "LblPairsV1";
            this.LblPairsV1.Size = new System.Drawing.Size(135, 31);
            this.LblPairsV1.TabIndex = 0;
            this.LblPairsV1.Text = "Pairs v1.0";
            // 
            // TxtAboutText
            // 
            this.TxtAboutText.Location = new System.Drawing.Point(277, 86);
            this.TxtAboutText.Multiline = true;
            this.TxtAboutText.Name = "TxtAboutText";
            this.TxtAboutText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtAboutText.Size = new System.Drawing.Size(360, 200);
            this.TxtAboutText.TabIndex = 1;
            // 
            // PicBxPairsLogo
            // 
            this.PicBxPairsLogo.Location = new System.Drawing.Point(27, 86);
            this.PicBxPairsLogo.Name = "PicBxPairsLogo";
            this.PicBxPairsLogo.Size = new System.Drawing.Size(200, 200);
            this.PicBxPairsLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBxPairsLogo.TabIndex = 2;
            this.PicBxPairsLogo.TabStop = false;
            // 
            // BtnCloseAbout
            // 
            this.BtnCloseAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCloseAbout.Location = new System.Drawing.Point(182, 315);
            this.BtnCloseAbout.Name = "BtnCloseAbout";
            this.BtnCloseAbout.Size = new System.Drawing.Size(277, 48);
            this.BtnCloseAbout.TabIndex = 3;
            this.BtnCloseAbout.Text = "Ok";
            this.BtnCloseAbout.UseVisualStyleBackColor = true;
            this.BtnCloseAbout.Click += new System.EventHandler(this.BtnCloseAbout_Click);
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 375);
            this.Controls.Add(this.BtnCloseAbout);
            this.Controls.Add(this.PicBxPairsLogo);
            this.Controls.Add(this.TxtAboutText);
            this.Controls.Add(this.LblPairsV1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.PicBxPairsLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblPairsV1;
        private System.Windows.Forms.TextBox TxtAboutText;
        private System.Windows.Forms.PictureBox PicBxPairsLogo;
        private System.Windows.Forms.Button BtnCloseAbout;
    }
}