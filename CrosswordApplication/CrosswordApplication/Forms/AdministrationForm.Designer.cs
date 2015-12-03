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
            this.SuspendLayout();
            // 
            // dictionaryManagerButton
            // 
            this.dictionaryManagerButton.Location = new System.Drawing.Point(12, 90);
            this.dictionaryManagerButton.Name = "dictionaryManagerButton";
            this.dictionaryManagerButton.Size = new System.Drawing.Size(317, 60);
            this.dictionaryManagerButton.TabIndex = 3;
            this.dictionaryManagerButton.Text = "Менеджер словарей";
            this.dictionaryManagerButton.UseVisualStyleBackColor = true;
            this.dictionaryManagerButton.Click += new System.EventHandler(this.dictionaryManagerButton_Click);
            // 
            // crosswordManagerButton
            // 
            this.crosswordManagerButton.Location = new System.Drawing.Point(12, 13);
            this.crosswordManagerButton.Name = "crosswordManagerButton";
            this.crosswordManagerButton.Size = new System.Drawing.Size(317, 60);
            this.crosswordManagerButton.TabIndex = 2;
            this.crosswordManagerButton.Text = "Менеджер кроссвордов";
            this.crosswordManagerButton.UseVisualStyleBackColor = true;
            this.crosswordManagerButton.Click += new System.EventHandler(this.crosswordManagerButton_Click);
            // 
            // AdministrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.dictionaryManagerButton);
            this.Controls.Add(this.crosswordManagerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AdministrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Администрирование";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button dictionaryManagerButton;
        private System.Windows.Forms.Button crosswordManagerButton;
    }
}