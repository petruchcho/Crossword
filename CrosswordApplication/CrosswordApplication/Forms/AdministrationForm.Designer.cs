namespace CrosswordApplication
{
    partial class AdministrationForm
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
            this.dictionaryManagerButton = new System.Windows.Forms.Button();
            this.crosswordManagerButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dictionaryManagerButton
            // 
            this.dictionaryManagerButton.Location = new System.Drawing.Point(129, 139);
            this.dictionaryManagerButton.Name = "dictionaryManagerButton";
            this.dictionaryManagerButton.Size = new System.Drawing.Size(317, 60);
            this.dictionaryManagerButton.TabIndex = 3;
            this.dictionaryManagerButton.Text = "Менеджер словарей";
            this.dictionaryManagerButton.UseVisualStyleBackColor = true;
            this.dictionaryManagerButton.Click += new System.EventHandler(this.dictionaryManagerButton_Click);
            // 
            // crosswordManagerButton
            // 
            this.crosswordManagerButton.Location = new System.Drawing.Point(129, 55);
            this.crosswordManagerButton.Name = "crosswordManagerButton";
            this.crosswordManagerButton.Size = new System.Drawing.Size(317, 60);
            this.crosswordManagerButton.TabIndex = 2;
            this.crosswordManagerButton.Text = "Менеджер кроссвордов";
            this.crosswordManagerButton.UseVisualStyleBackColor = true;
            this.crosswordManagerButton.Click += new System.EventHandler(this.crosswordManagerButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // AdministrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dictionaryManagerButton);
            this.Controls.Add(this.crosswordManagerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AdministrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Администрирование";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dictionaryManagerButton;
        private System.Windows.Forms.Button crosswordManagerButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
    }
}