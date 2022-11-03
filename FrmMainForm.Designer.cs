namespace PairsAssignmentFM
{
    partial class FrmMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TBBtnGame = new System.Windows.Forms.ToolStripDropDownButton();
            this.TsmiNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmiSaveGame = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiLoadGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TBBtnSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSize6x6 = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSize10x10 = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSize16x16 = new System.Windows.Forms.ToolStripMenuItem();
            this.TBBtnHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PnlPlayer1 = new System.Windows.Forms.Panel();
            this.PnlPlayer2 = new System.Windows.Forms.Panel();
            this.PnlGameBoard = new System.Windows.Forms.Panel();
            this.PicBxLargeLogo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.PnlGameBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBxLargeLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TBBtnGame,
            this.TBBtnSettings,
            this.TBBtnHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1500, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TBBtnGame
            // 
            this.TBBtnGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TBBtnGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiNewGame,
            this.toolStripSeparator2,
            this.TsmiSaveGame,
            this.TsmiLoadGame,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.TBBtnGame.Image = ((System.Drawing.Image)(resources.GetObject("TBBtnGame.Image")));
            this.TBBtnGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBBtnGame.Name = "TBBtnGame";
            this.TBBtnGame.Size = new System.Drawing.Size(62, 24);
            this.TBBtnGame.Text = "Game";
            // 
            // TsmiNewGame
            // 
            this.TsmiNewGame.Name = "TsmiNewGame";
            this.TsmiNewGame.Size = new System.Drawing.Size(189, 26);
            this.TsmiNewGame.Text = "New Game";
            this.TsmiNewGame.Click += new System.EventHandler(this.TsmiNewGame_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // TsmiSaveGame
            // 
            this.TsmiSaveGame.Name = "TsmiSaveGame";
            this.TsmiSaveGame.Size = new System.Drawing.Size(189, 26);
            this.TsmiSaveGame.Text = "Save Game";
            this.TsmiSaveGame.Click += new System.EventHandler(this.TsmiSaveGame_Click);
            // 
            // TsmiLoadGame
            // 
            this.TsmiLoadGame.Name = "TsmiLoadGame";
            this.TsmiLoadGame.Size = new System.Drawing.Size(189, 26);
            this.TsmiLoadGame.Text = "Retreive Game";
            this.TsmiLoadGame.Click += new System.EventHandler(this.TsmiLoadGame_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // TBBtnSettings
            // 
            this.TBBtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TBBtnSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripMenuItem});
            this.TBBtnSettings.Image = ((System.Drawing.Image)(resources.GetObject("TBBtnSettings.Image")));
            this.TBBtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBBtnSettings.Name = "TBBtnSettings";
            this.TBBtnSettings.Size = new System.Drawing.Size(76, 24);
            this.TBBtnSettings.Text = "Settings";
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiSize6x6,
            this.TsmiSize10x10,
            this.TsmiSize16x16});
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(149, 26);
            this.sizeToolStripMenuItem.Text = "Grid size";
            // 
            // TsmiSize6x6
            // 
            this.TsmiSize6x6.CheckOnClick = true;
            this.TsmiSize6x6.Name = "TsmiSize6x6";
            this.TsmiSize6x6.Size = new System.Drawing.Size(131, 26);
            this.TsmiSize6x6.Text = "6x6";
            this.TsmiSize6x6.Click += new System.EventHandler(this.TsmiSize6x6_Click);
            // 
            // TsmiSize10x10
            // 
            this.TsmiSize10x10.CheckOnClick = true;
            this.TsmiSize10x10.Name = "TsmiSize10x10";
            this.TsmiSize10x10.Size = new System.Drawing.Size(131, 26);
            this.TsmiSize10x10.Text = "10x10";
            this.TsmiSize10x10.Click += new System.EventHandler(this.TsmiSize10x10_Click);
            // 
            // TsmiSize16x16
            // 
            this.TsmiSize16x16.CheckOnClick = true;
            this.TsmiSize16x16.Name = "TsmiSize16x16";
            this.TsmiSize16x16.Size = new System.Drawing.Size(131, 26);
            this.TsmiSize16x16.Text = "16x16";
            this.TsmiSize16x16.Click += new System.EventHandler(this.TsmiSize16x16_Click);
            // 
            // TBBtnHelp
            // 
            this.TBBtnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TBBtnHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.TBBtnHelp.Image = ((System.Drawing.Image)(resources.GetObject("TBBtnHelp.Image")));
            this.TBBtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBBtnHelp.Name = "TBBtnHelp";
            this.TBBtnHelp.Size = new System.Drawing.Size(55, 24);
            this.TBBtnHelp.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // PnlPlayer1
            // 
            this.PnlPlayer1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PnlPlayer1.Location = new System.Drawing.Point(12, 40);
            this.PnlPlayer1.MaximumSize = new System.Drawing.Size(300, 850);
            this.PnlPlayer1.MinimumSize = new System.Drawing.Size(300, 850);
            this.PnlPlayer1.Name = "PnlPlayer1";
            this.PnlPlayer1.Size = new System.Drawing.Size(300, 850);
            this.PnlPlayer1.TabIndex = 1;
            // 
            // PnlPlayer2
            // 
            this.PnlPlayer2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PnlPlayer2.Location = new System.Drawing.Point(1174, 40);
            this.PnlPlayer2.MaximumSize = new System.Drawing.Size(300, 850);
            this.PnlPlayer2.MinimumSize = new System.Drawing.Size(300, 850);
            this.PnlPlayer2.Name = "PnlPlayer2";
            this.PnlPlayer2.Size = new System.Drawing.Size(300, 850);
            this.PnlPlayer2.TabIndex = 2;
            // 
            // PnlGameBoard
            // 
            this.PnlGameBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlGameBoard.Controls.Add(this.PicBxLargeLogo);
            this.PnlGameBoard.Location = new System.Drawing.Point(318, 40);
            this.PnlGameBoard.MaximumSize = new System.Drawing.Size(850, 850);
            this.PnlGameBoard.MinimumSize = new System.Drawing.Size(850, 850);
            this.PnlGameBoard.Name = "PnlGameBoard";
            this.PnlGameBoard.Size = new System.Drawing.Size(850, 850);
            this.PnlGameBoard.TabIndex = 3;
            // 
            // PicBxLargeLogo
            // 
            this.PicBxLargeLogo.Location = new System.Drawing.Point(3, 168);
            this.PicBxLargeLogo.Name = "PicBxLargeLogo";
            this.PicBxLargeLogo.Size = new System.Drawing.Size(844, 490);
            this.PicBxLargeLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBxLargeLogo.TabIndex = 0;
            this.PicBxLargeLogo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1122, 905);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Copyright Freddie Mason Sheffield Hallam University 2022";
            // 
            // FrmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1500, 959);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PnlGameBoard);
            this.Controls.Add(this.PnlPlayer2);
            this.Controls.Add(this.PnlPlayer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1518, 1006);
            this.MinimumSize = new System.Drawing.Size(1518, 1006);
            this.Name = "FrmMainForm";
            this.Text = "Pairs";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PnlGameBoard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBxLargeLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton TBBtnGame;
        private System.Windows.Forms.ToolStripMenuItem TsmiNewGame;
        private System.Windows.Forms.ToolStripMenuItem TsmiSaveGame;
        private System.Windows.Forms.ToolStripMenuItem TsmiLoadGame;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton TBBtnSettings;
        private System.Windows.Forms.ToolStripDropDownButton TBBtnHelp;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TsmiSize6x6;
        private System.Windows.Forms.ToolStripMenuItem TsmiSize10x10;
        private System.Windows.Forms.ToolStripMenuItem TsmiSize16x16;
        private System.Windows.Forms.Panel PnlPlayer1;
        private System.Windows.Forms.Panel PnlPlayer2;
        private System.Windows.Forms.Panel PnlGameBoard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PicBxLargeLogo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

