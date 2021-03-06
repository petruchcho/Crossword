﻿namespace CrosswordApplication.Forms
{
    partial class DictionaryManagerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictionaryManagerForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newWordButton = new System.Windows.Forms.ToolStripButton();
            this.updateWordButton = new System.Windows.Forms.ToolStripButton();
            this.deleteWordButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.searchButton = new System.Windows.Forms.ToolStripButton();
            this.searchMask = new System.Windows.Forms.ToolStripTextBox();
            this.deleteMaskButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stripSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.поАлфавиту = new System.Windows.Forms.ToolStripMenuItem();
            this.поКоличествуБукв = new System.Windows.Forms.ToolStripMenuItem();
            this.anscendingButton = new System.Windows.Forms.ToolStripButton();
            this.descendingButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьСловарьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userguideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyState = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.dictionaryListBox = new CrosswordApplication.Forms.CustomDataGridView();
            this.wordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryListBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWordButton,
            this.updateWordButton,
            this.deleteWordButton,
            this.toolStripSeparator1,
            this.searchButton,
            this.searchMask,
            this.deleteMaskButton,
            this.toolStripSeparator2,
            this.stripSplitButton,
            this.anscendingButton,
            this.descendingButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(736, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newWordButton
            // 
            this.newWordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newWordButton.Image = global::CrosswordApplication.Properties.Resources.plus_circle;
            this.newWordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newWordButton.Name = "newWordButton";
            this.newWordButton.Size = new System.Drawing.Size(23, 22);
            this.newWordButton.Text = "toolStripButton1";
            this.newWordButton.ToolTipText = "Добавить новое понятие";
            this.newWordButton.Click += new System.EventHandler(this.newWordButton_Click);
            // 
            // updateWordButton
            // 
            this.updateWordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updateWordButton.Image = global::CrosswordApplication.Properties.Resources.pencil;
            this.updateWordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateWordButton.Name = "updateWordButton";
            this.updateWordButton.Size = new System.Drawing.Size(23, 22);
            this.updateWordButton.Text = "toolStripButton2";
            this.updateWordButton.ToolTipText = "Редактировать понятие";
            this.updateWordButton.Click += new System.EventHandler(this.updateWordButton_Click);
            // 
            // deleteWordButton
            // 
            this.deleteWordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteWordButton.Image = global::CrosswordApplication.Properties.Resources.cross_script;
            this.deleteWordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteWordButton.Name = "deleteWordButton";
            this.deleteWordButton.Size = new System.Drawing.Size(23, 22);
            this.deleteWordButton.Text = "toolStripButton3";
            this.deleteWordButton.ToolTipText = "Удалить понятие";
            this.deleteWordButton.Click += new System.EventHandler(this.deleteWordButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // searchButton
            // 
            this.searchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchButton.Image = global::CrosswordApplication.Properties.Resources.magnifier;
            this.searchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(23, 22);
            this.searchButton.Text = "toolStripButton4";
            this.searchButton.ToolTipText = "Найти";
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchMask
            // 
            this.searchMask.Name = "searchMask";
            this.searchMask.Size = new System.Drawing.Size(100, 25);
            this.searchMask.ToolTipText = "Поиск по маске\r\n? - любая одна буква\r\n* - любые несколько букв\r\n";
            this.searchMask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchMask_KeyDown);
            this.searchMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBox1_KeyPress);
            // 
            // deleteMaskButton
            // 
            this.deleteMaskButton.AutoSize = false;
            this.deleteMaskButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteMaskButton.Image = global::CrosswordApplication.Properties.Resources.cross_circle;
            this.deleteMaskButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteMaskButton.Name = "deleteMaskButton";
            this.deleteMaskButton.Size = new System.Drawing.Size(23, 22);
            this.deleteMaskButton.Text = "toolStripButton5";
            this.deleteMaskButton.ToolTipText = "Очистить маску";
            this.deleteMaskButton.Click += new System.EventHandler(this.deleteMaskButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // stripSplitButton
            // 
            this.stripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поАлфавиту,
            this.поКоличествуБукв});
            this.stripSplitButton.Image = global::CrosswordApplication.Properties.Resources.sort;
            this.stripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripSplitButton.Name = "stripSplitButton";
            this.stripSplitButton.Size = new System.Drawing.Size(125, 22);
            this.stripSplitButton.Text = "Тип сортировки";
            // 
            // поАлфавиту
            // 
            this.поАлфавиту.Name = "поАлфавиту";
            this.поАлфавиту.Size = new System.Drawing.Size(184, 22);
            this.поАлфавиту.Text = "По алфавиту";
            this.поАлфавиту.Click += new System.EventHandler(this.поАлфавиту_Click);
            // 
            // поКоличествуБукв
            // 
            this.поКоличествуБукв.Name = "поКоличествуБукв";
            this.поКоличествуБукв.Size = new System.Drawing.Size(184, 22);
            this.поКоличествуБукв.Text = "По количеству букв";
            this.поКоличествуБукв.Click += new System.EventHandler(this.поКоличествуБукв_Click);
            // 
            // anscendingButton
            // 
            this.anscendingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.anscendingButton.Image = global::CrosswordApplication.Properties.Resources.arrow_090;
            this.anscendingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.anscendingButton.Name = "anscendingButton";
            this.anscendingButton.Size = new System.Drawing.Size(23, 22);
            this.anscendingButton.Text = "toolStripButton6";
            this.anscendingButton.ToolTipText = "Сортировка по возрастанию\r\n";
            this.anscendingButton.Click += new System.EventHandler(this.anscendingButton_Click);
            // 
            // descendingButton
            // 
            this.descendingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.descendingButton.Image = global::CrosswordApplication.Properties.Resources.arrow_270;
            this.descendingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.descendingButton.Name = "descendingButton";
            this.descendingButton.Size = new System.Drawing.Size(23, 22);
            this.descendingButton.Text = "toolStripButton6";
            this.descendingButton.ToolTipText = "Сортировка по убыванию";
            this.descendingButton.Click += new System.EventHandler(this.descendingButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(736, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.открытьСловарьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // открытьСловарьToolStripMenuItem
            // 
            this.открытьСловарьToolStripMenuItem.Name = "открытьСловарьToolStripMenuItem";
            this.открытьСловарьToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.открытьСловарьToolStripMenuItem.Text = "Открыть словарь";
            this.открытьСловарьToolStripMenuItem.Click += new System.EventHandler(this.открытьСловарьToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.userguideToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // userguideToolStripMenuItem
            // 
            this.userguideToolStripMenuItem.Name = "userguideToolStripMenuItem";
            this.userguideToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.userguideToolStripMenuItem.Text = "Руководство пользователя";
            this.userguideToolStripMenuItem.Click += new System.EventHandler(this.userguideToolStripMenuItem_Click);
            // 
            // emptyState
            // 
            this.emptyState.AutoSize = true;
            this.emptyState.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.emptyState.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.emptyState.ForeColor = System.Drawing.Color.Red;
            this.emptyState.Location = new System.Drawing.Point(179, 202);
            this.emptyState.Name = "emptyState";
            this.emptyState.Size = new System.Drawing.Size(372, 24);
            this.emptyState.TabIndex = 5;
            this.emptyState.Text = "Загрузите или создайте новый словарь";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(514, 28);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(209, 15);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // dictionaryListBox
            // 
            this.dictionaryListBox.AllowUserToAddRows = false;
            this.dictionaryListBox.AllowUserToDeleteRows = false;
            this.dictionaryListBox.AllowUserToResizeColumns = false;
            this.dictionaryListBox.AllowUserToResizeRows = false;
            this.dictionaryListBox.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dictionaryListBox.BackgroundColor = System.Drawing.Color.White;
            this.dictionaryListBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dictionaryListBox.ColumnHeadersVisible = false;
            this.dictionaryListBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wordColumn,
            this.descriptionColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dictionaryListBox.DefaultCellStyle = dataGridViewCellStyle1;
            this.dictionaryListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dictionaryListBox.Location = new System.Drawing.Point(0, 49);
            this.dictionaryListBox.MultiSelect = false;
            this.dictionaryListBox.Name = "dictionaryListBox";
            this.dictionaryListBox.RowHeadersVisible = false;
            this.dictionaryListBox.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dictionaryListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dictionaryListBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dictionaryListBox.Size = new System.Drawing.Size(736, 385);
            this.dictionaryListBox.TabIndex = 6;
            this.dictionaryListBox.SelectionChanged += new System.EventHandler(this.dictionaryListBox_SelectedIndexChanged);
            this.dictionaryListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dictionaryListBox_MouseDoubleClick);
            // 
            // wordColumn
            // 
            this.wordColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.wordColumn.FillWeight = 25F;
            this.wordColumn.HeaderText = "Слово";
            this.wordColumn.Name = "wordColumn";
            this.wordColumn.ReadOnly = true;
            this.wordColumn.Width = 183;
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.descriptionColumn.FillWeight = 75F;
            this.descriptionColumn.HeaderText = "Описание";
            this.descriptionColumn.Name = "descriptionColumn";
            this.descriptionColumn.ReadOnly = true;
            this.descriptionColumn.Width = 550;
            // 
            // DictionaryManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(736, 434);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.emptyState);
            this.Controls.Add(this.dictionaryListBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DictionaryManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Менеджер словарей";
            this.Load += new System.EventHandler(this.DictionaryManagerForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryListBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьСловарьToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newWordButton;
        private System.Windows.Forms.ToolStripButton updateWordButton;
        private System.Windows.Forms.ToolStripButton deleteWordButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton searchButton;
        private System.Windows.Forms.ToolStripTextBox searchMask;
        private System.Windows.Forms.ToolStripButton deleteMaskButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton descendingButton;
        private System.Windows.Forms.ToolStripButton anscendingButton;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton stripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem поАлфавиту;
        private System.Windows.Forms.ToolStripMenuItem поКоличествуБукв;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userguideToolStripMenuItem;
        private System.Windows.Forms.Label emptyState;
        private CustomDataGridView dictionaryListBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
    }
}