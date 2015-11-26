namespace CrosswordApplication
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.solvingButton = new System.Windows.Forms.Button();
            this.administrationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // solvingButton
            // 
            this.solvingButton.Location = new System.Drawing.Point(12, 12);
            this.solvingButton.Name = "solvingButton";
            this.solvingButton.Size = new System.Drawing.Size(317, 60);
            this.solvingButton.TabIndex = 0;
            this.solvingButton.Text = "Разгадывание";
            this.solvingButton.UseVisualStyleBackColor = true;
            // 
            // administrationButton
            // 
            this.administrationButton.Location = new System.Drawing.Point(12, 89);
            this.administrationButton.Name = "administrationButton";
            this.administrationButton.Size = new System.Drawing.Size(317, 60);
            this.administrationButton.TabIndex = 1;
            this.administrationButton.Text = "Администрирование";
            this.administrationButton.UseVisualStyleBackColor = true;
            this.administrationButton.Click += new System.EventHandler(this.administrationButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.administrationButton);
            this.Controls.Add(this.solvingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button solvingButton;
        private System.Windows.Forms.Button administrationButton;

    }
}

