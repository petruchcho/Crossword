namespace CrosswordApplication.Forms
{
    partial class CrosswordGenerationParametersForm
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
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openDictionaryFileButton = new System.Windows.Forms.Button();
            this.dictionaryFilePathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.widthNumericUpDown.Location = new System.Drawing.Point(350, 123);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.widthNumericUpDown.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.ReadOnly = true;
            this.widthNumericUpDown.Size = new System.Drawing.Size(50, 24);
            this.widthNumericUpDown.TabIndex = 0;
            this.widthNumericUpDown.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.CausesValidation = false;
            this.heightNumericUpDown.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.heightNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.heightNumericUpDown.Location = new System.Drawing.Point(147, 126);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.heightNumericUpDown.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.ReadOnly = true;
            this.heightNumericUpDown.Size = new System.Drawing.Size(50, 24);
            this.heightNumericUpDown.TabIndex = 1;
            this.heightNumericUpDown.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.widthLabel.Location = new System.Drawing.Point(279, 126);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(62, 16);
            this.widthLabel.TabIndex = 2;
            this.widthLabel.Text = "Ширина:";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.heightLabel.Location = new System.Drawing.Point(79, 126);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(52, 16);
            this.heightLabel.TabIndex = 3;
            this.heightLabel.Text = "Длина:";
            // 
            // generateButton
            // 
            this.generateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.generateButton.Location = new System.Drawing.Point(183, 173);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(145, 30);
            this.generateButton.TabIndex = 4;
            this.generateButton.Text = "Сгенерировать";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(27, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Словарь";
            // 
            // openDictionaryFileButton
            // 
            this.openDictionaryFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openDictionaryFileButton.Location = new System.Drawing.Point(406, 49);
            this.openDictionaryFileButton.Name = "openDictionaryFileButton";
            this.openDictionaryFileButton.Size = new System.Drawing.Size(80, 29);
            this.openDictionaryFileButton.TabIndex = 6;
            this.openDictionaryFileButton.Text = "Обзор...";
            this.openDictionaryFileButton.UseVisualStyleBackColor = true;
            this.openDictionaryFileButton.Click += new System.EventHandler(this.openDictionaryFileButton_Click);
            // 
            // dictionaryFilePathTextBox
            // 
            this.dictionaryFilePathTextBox.Location = new System.Drawing.Point(30, 54);
            this.dictionaryFilePathTextBox.Multiline = true;
            this.dictionaryFilePathTextBox.Name = "dictionaryFilePathTextBox";
            this.dictionaryFilePathTextBox.ReadOnly = true;
            this.dictionaryFilePathTextBox.Size = new System.Drawing.Size(370, 23);
            this.dictionaryFilePathTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(27, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Размер сетки";
            this.label2.UseMnemonic = false;
            // 
            // CrosswordGenerationParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 223);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dictionaryFilePathTextBox);
            this.Controls.Add(this.openDictionaryFileButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.heightNumericUpDown);
            this.Controls.Add(this.widthNumericUpDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CrosswordGenerationParametersForm";
            this.Text = "Сгенерировать кроссворд";
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown widthNumericUpDown;
        private System.Windows.Forms.NumericUpDown heightNumericUpDown;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openDictionaryFileButton;
        private System.Windows.Forms.TextBox dictionaryFilePathTextBox;
        private System.Windows.Forms.Label label2;
    }
}