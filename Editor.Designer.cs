namespace SWE1RSE
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_newTGFD = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_newProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_openTGFD = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_openProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_save = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_saveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.basepanel = new System.Windows.Forms.Panel();
            this.dlg_inGame = new System.Windows.Forms.OpenFileDialog();
            this.dlg_outGame = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ss_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.dlg_inProfile = new System.Windows.Forms.OpenFileDialog();
            this.dlg_outProfile = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.fileToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(464, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_newTGFD,
            this.menu_newProfile});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Visible = false;
            // 
            // menu_newTGFD
            // 
            this.menu_newTGFD.Name = "menu_newTGFD";
            this.menu_newTGFD.Size = new System.Drawing.Size(180, 22);
            this.menu_newTGFD.Text = "tgfd.dat";
            // 
            // menu_newProfile
            // 
            this.menu_newProfile.Name = "menu_newProfile";
            this.menu_newProfile.Size = new System.Drawing.Size(180, 22);
            this.menu_newProfile.Text = "Profile .sav";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_openTGFD,
            this.menu_openProfile});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Open";
            // 
            // menu_openTGFD
            // 
            this.menu_openTGFD.Name = "menu_openTGFD";
            this.menu_openTGFD.Size = new System.Drawing.Size(180, 22);
            this.menu_openTGFD.Text = "tgfd.dat";
            this.menu_openTGFD.Click += new System.EventHandler(this.TgfddatToolStripMenuItem_Click);
            // 
            // menu_openProfile
            // 
            this.menu_openProfile.Name = "menu_openProfile";
            this.menu_openProfile.Size = new System.Drawing.Size(180, 22);
            this.menu_openProfile.Text = "Profile .sav";
            this.menu_openProfile.Click += new System.EventHandler(this.Menu_openProfile_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_save,
            this.menu_saveAs});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // menu_save
            // 
            this.menu_save.Name = "menu_save";
            this.menu_save.Size = new System.Drawing.Size(180, 22);
            this.menu_save.Text = "Save";
            this.menu_save.Click += new System.EventHandler(this.TgfddatToolStripMenuItem1_Click);
            // 
            // menu_saveAs
            // 
            this.menu_saveAs.Name = "menu_saveAs";
            this.menu_saveAs.Size = new System.Drawing.Size(180, 22);
            this.menu_saveAs.Text = "Save As...";
            this.menu_saveAs.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // basepanel
            // 
            this.basepanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basepanel.AutoScroll = true;
            this.basepanel.AutoScrollMargin = new System.Drawing.Size(0, 8);
            this.basepanel.BackColor = System.Drawing.SystemColors.Control;
            this.basepanel.Location = new System.Drawing.Point(0, 24);
            this.basepanel.Name = "basepanel";
            this.basepanel.Size = new System.Drawing.Size(464, 556);
            this.basepanel.TabIndex = 1;
            // 
            // dlg_inGame
            // 
            this.dlg_inGame.Filter = "SWE1R Game Save (*.dat)|*.dat|All Files (*.*)|*.*";
            this.dlg_inGame.Title = "Open SWE1R Game Save";
            // 
            // dlg_outGame
            // 
            this.dlg_outGame.Filter = "SWE1R Game Save (*.dat)|*.dat";
            this.dlg_outGame.Title = "Save SWE1R Game Save";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(464, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ss_label
            // 
            this.ss_label.AutoSize = false;
            this.ss_label.Name = "ss_label";
            this.ss_label.Size = new System.Drawing.Size(448, 17);
            this.ss_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dlg_inProfile
            // 
            this.dlg_inProfile.Filter = "SWE1R Profile (*.sav)|*.sav|All Files (*.*)|*.*";
            this.dlg_inProfile.Title = "Open SWE1R Profile";
            // 
            // dlg_outProfile
            // 
            this.dlg_outProfile.Filter = "SWE1R Profile (*.sav)|*.sav";
            this.dlg_outProfile.Title = "Save SWE1R Profile";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(464, 602);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.basepanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(480, 1080);
            this.MinimumSize = new System.Drawing.Size(480, 480);
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SWE1R Save Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel basepanel;
        private System.Windows.Forms.OpenFileDialog dlg_inGame;
        private System.Windows.Forms.ToolStripMenuItem menu_openTGFD;
        private System.Windows.Forms.ToolStripMenuItem menu_openProfile;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_newTGFD;
        private System.Windows.Forms.ToolStripMenuItem menu_newProfile;
        private System.Windows.Forms.ToolStripMenuItem menu_save;
        private System.Windows.Forms.ToolStripMenuItem menu_saveAs;
        private System.Windows.Forms.SaveFileDialog dlg_outGame;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ss_label;
        private System.Windows.Forms.OpenFileDialog dlg_inProfile;
        private System.Windows.Forms.SaveFileDialog dlg_outProfile;
    }
}

