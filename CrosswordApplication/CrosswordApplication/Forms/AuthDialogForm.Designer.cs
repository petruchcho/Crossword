﻿namespace CrosswordApplication
{
    partial class AuthDialogForm
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
            this.ok_button = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.typePasswordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(12, 68);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(96, 28);
            this.ok_button.TabIndex = 0;
            this.ok_button.Text = "Ok";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(12, 31);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(204, 20);
            this.passwordTextBox.TabIndex = 1;
            // 
            // typePasswordLabel
            // 
            this.typePasswordLabel.AutoSize = true;
            this.typePasswordLabel.Location = new System.Drawing.Point(12, 12);
            this.typePasswordLabel.Name = "typePasswordLabel";
            this.typePasswordLabel.Size = new System.Drawing.Size(88, 13);
            this.typePasswordLabel.TabIndex = 2;
            this.typePasswordLabel.Text = "Введите пароль";
            // 
            // AuthDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 118);
            this.Controls.Add(this.typePasswordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.ok_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AuthDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label typePasswordLabel;
    }
}