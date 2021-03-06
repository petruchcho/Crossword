﻿namespace CrosswordApplication.Forms
{
    partial class CrosswordEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrosswordEditForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.кроссвордToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCrosswordToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCrosswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCrosswordToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.словарьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDictionaryToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userguideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootSplitContainer = new System.Windows.Forms.SplitContainer();
            this.questionsUserPanel = new System.Windows.Forms.Panel();
            this.finishButton = new System.Windows.Forms.Button();
            this.openLetterButton = new System.Windows.Forms.Button();
            this.openWordButton = new System.Windows.Forms.Button();
            this.questionsAdminSplitContainer = new System.Windows.Forms.SplitContainer();
            this.questionsToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.dictionaryToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.loadDictionaryHintLabel = new System.Windows.Forms.Label();
            this.dictionarySortDesc = new System.Windows.Forms.ToolStripButton();
            this.dictionarySortAsc = new System.Windows.Forms.ToolStripButton();
            this.board = new CrosswordApplication.Forms.CustomDataGridView();
            this.questionsListBox = new CrosswordApplication.Forms.CustomDataGridView();
            this.wordColumnQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumnQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dictionaryListBox = new CrosswordApplication.Forms.CustomDataGridView();
            this.wordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.questionsUserList = new CrosswordApplication.Forms.CustomDataGridView();
            this.DescriptionUserColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rootSplitContainer)).BeginInit();
            this.rootSplitContainer.Panel1.SuspendLayout();
            this.rootSplitContainer.Panel2.SuspendLayout();
            this.rootSplitContainer.SuspendLayout();
            this.questionsUserPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionsAdminSplitContainer)).BeginInit();
            this.questionsAdminSplitContainer.Panel1.SuspendLayout();
            this.questionsAdminSplitContainer.Panel2.SuspendLayout();
            this.questionsAdminSplitContainer.SuspendLayout();
            this.questionsToolStrip.SuspendLayout();
            this.dictionaryToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsListBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryListBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsUserList)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.кроссвордToolStripMenuItem,
            this.словарьToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // кроссвордToolStripMenuItem
            // 
            this.кроссвордToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCrosswordToolStripMenu,
            this.saveCrosswordToolStripMenuItem,
            this.loadCrosswordToolStripMenu,
            this.generateToolStripMenuItem,
            this.parametersToolStripMenuItem});
            this.кроссвордToolStripMenuItem.Name = "кроссвордToolStripMenuItem";
            this.кроссвордToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.кроссвордToolStripMenuItem.Text = "Кроссворд";
            // 
            // newCrosswordToolStripMenu
            // 
            this.newCrosswordToolStripMenu.Name = "newCrosswordToolStripMenu";
            this.newCrosswordToolStripMenu.Size = new System.Drawing.Size(157, 22);
            this.newCrosswordToolStripMenu.Text = "Новый";
            this.newCrosswordToolStripMenu.Click += new System.EventHandler(this.newCrosswordToolStripMenu_Click);
            // 
            // saveCrosswordToolStripMenuItem
            // 
            this.saveCrosswordToolStripMenuItem.Enabled = false;
            this.saveCrosswordToolStripMenuItem.Name = "saveCrosswordToolStripMenuItem";
            this.saveCrosswordToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saveCrosswordToolStripMenuItem.Text = "Сохранить";
            this.saveCrosswordToolStripMenuItem.Click += new System.EventHandler(this.saveCrosswordToolStripMenuItem_Click);
            // 
            // loadCrosswordToolStripMenu
            // 
            this.loadCrosswordToolStripMenu.Enabled = false;
            this.loadCrosswordToolStripMenu.Name = "loadCrosswordToolStripMenu";
            this.loadCrosswordToolStripMenu.Size = new System.Drawing.Size(157, 22);
            this.loadCrosswordToolStripMenu.Text = "Загрузить";
            this.loadCrosswordToolStripMenu.Click += new System.EventHandler(this.loadCrosswordToolStripMenu_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.generateToolStripMenuItem.Text = "Сгенерировать";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.parametersToolStripMenuItem.Text = "Параметры";
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            // 
            // словарьToolStripMenuItem
            // 
            this.словарьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDictionaryToolStripMenu});
            this.словарьToolStripMenuItem.Name = "словарьToolStripMenuItem";
            this.словарьToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.словарьToolStripMenuItem.Text = "Словарь";
            // 
            // loadDictionaryToolStripMenu
            // 
            this.loadDictionaryToolStripMenu.Name = "loadDictionaryToolStripMenu";
            this.loadDictionaryToolStripMenu.Size = new System.Drawing.Size(128, 22);
            this.loadDictionaryToolStripMenu.Text = "Загрузить";
            this.loadDictionaryToolStripMenu.Click += new System.EventHandler(this.loadDictionaryToolStripMenuItem_Click);
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
            // rootSplitContainer
            // 
            this.rootSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rootSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.rootSplitContainer.IsSplitterFixed = true;
            this.rootSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.rootSplitContainer.Name = "rootSplitContainer";
            // 
            // rootSplitContainer.Panel1
            // 
            this.rootSplitContainer.Panel1.Controls.Add(this.board);
            // 
            // rootSplitContainer.Panel2
            // 
            this.rootSplitContainer.Panel2.Controls.Add(this.questionsAdminSplitContainer);
            this.rootSplitContainer.Panel2.Controls.Add(this.questionsUserPanel);
            this.rootSplitContainer.Size = new System.Drawing.Size(784, 537);
            this.rootSplitContainer.SplitterDistance = 500;
            this.rootSplitContainer.TabIndex = 1;
            // 
            // questionsUserPanel
            // 
            this.questionsUserPanel.Controls.Add(this.finishButton);
            this.questionsUserPanel.Controls.Add(this.openLetterButton);
            this.questionsUserPanel.Controls.Add(this.openWordButton);
            this.questionsUserPanel.Controls.Add(this.questionsUserList);
            this.questionsUserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questionsUserPanel.Location = new System.Drawing.Point(0, 0);
            this.questionsUserPanel.Name = "questionsUserPanel";
            this.questionsUserPanel.Size = new System.Drawing.Size(280, 537);
            this.questionsUserPanel.TabIndex = 1;
            // 
            // finishButton
            // 
            this.finishButton.Location = new System.Drawing.Point(138, 406);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(137, 127);
            this.finishButton.TabIndex = 3;
            this.finishButton.Text = "Завершить игру";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // openLetterButton
            // 
            this.openLetterButton.Location = new System.Drawing.Point(4, 474);
            this.openLetterButton.Name = "openLetterButton";
            this.openLetterButton.Size = new System.Drawing.Size(128, 59);
            this.openLetterButton.TabIndex = 2;
            this.openLetterButton.Text = "Открыть слово";
            this.openLetterButton.UseVisualStyleBackColor = true;
            this.openLetterButton.Click += new System.EventHandler(this.openLetterButton_Click);
            // 
            // openWordButton
            // 
            this.openWordButton.Location = new System.Drawing.Point(4, 407);
            this.openWordButton.Name = "openWordButton";
            this.openWordButton.Size = new System.Drawing.Size(128, 61);
            this.openWordButton.TabIndex = 1;
            this.openWordButton.Text = "Открыть букву";
            this.openWordButton.UseVisualStyleBackColor = true;
            this.openWordButton.Click += new System.EventHandler(this.openWordButton_Click);
            // 
            // questionsAdminSplitContainer
            // 
            this.questionsAdminSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.questionsAdminSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questionsAdminSplitContainer.IsSplitterFixed = true;
            this.questionsAdminSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.questionsAdminSplitContainer.Name = "questionsAdminSplitContainer";
            this.questionsAdminSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // questionsAdminSplitContainer.Panel1
            // 
            this.questionsAdminSplitContainer.Panel1.Controls.Add(this.questionsListBox);
            this.questionsAdminSplitContainer.Panel1.Controls.Add(this.questionsToolStrip);
            // 
            // questionsAdminSplitContainer.Panel2
            // 
            this.questionsAdminSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.questionsAdminSplitContainer.Panel2.Controls.Add(this.dictionaryListBox);
            this.questionsAdminSplitContainer.Panel2.Controls.Add(this.dictionaryToolStrip);
            this.questionsAdminSplitContainer.Panel2.Controls.Add(this.loadDictionaryHintLabel);
            this.questionsAdminSplitContainer.Size = new System.Drawing.Size(280, 537);
            this.questionsAdminSplitContainer.SplitterDistance = 299;
            this.questionsAdminSplitContainer.TabIndex = 0;
            // 
            // questionsToolStrip
            // 
            this.questionsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.questionsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
            this.questionsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.questionsToolStrip.Name = "questionsToolStrip";
            this.questionsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.questionsToolStrip.Size = new System.Drawing.Size(278, 25);
            this.questionsToolStrip.TabIndex = 1;
            this.questionsToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(104, 22);
            this.toolStripLabel2.Text = "Список вопросов";
            // 
            // dictionaryToolStrip
            // 
            this.dictionaryToolStrip.Enabled = false;
            this.dictionaryToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.dictionaryToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButton,
            this.dictionarySortDesc,
            this.dictionarySortAsc});
            this.dictionaryToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.dictionaryToolStrip.Location = new System.Drawing.Point(0, 0);
            this.dictionaryToolStrip.Name = "dictionaryToolStrip";
            this.dictionaryToolStrip.Size = new System.Drawing.Size(278, 25);
            this.dictionaryToolStrip.TabIndex = 0;
            this.dictionaryToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabel1.Text = "Словарь";
            // 
            // toolStripButton
            // 
            this.toolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton.Image")));
            this.toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton.Name = "toolStripButton";
            this.toolStripButton.Size = new System.Drawing.Size(69, 22);
            this.toolStripButton.Text = "Менеджер";
            this.toolStripButton.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // loadDictionaryHintLabel
            // 
            this.loadDictionaryHintLabel.AutoSize = true;
            this.loadDictionaryHintLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.loadDictionaryHintLabel.Location = new System.Drawing.Point(89, 115);
            this.loadDictionaryHintLabel.Name = "loadDictionaryHintLabel";
            this.loadDictionaryHintLabel.Size = new System.Drawing.Size(104, 13);
            this.loadDictionaryHintLabel.TabIndex = 2;
            this.loadDictionaryHintLabel.Text = "Загрузите словарь";
            // 
            // dictionarySortDesc
            // 
            this.dictionarySortDesc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dictionarySortDesc.Image = global::CrosswordApplication.Properties.Resources.arrow_090;
            this.dictionarySortDesc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dictionarySortDesc.Name = "dictionarySortDesc";
            this.dictionarySortDesc.Size = new System.Drawing.Size(23, 22);
            this.dictionarySortDesc.Text = "toolStripButton1";
            this.dictionarySortDesc.Click += new System.EventHandler(this.dictionarySortDesc_Click);
            // 
            // dictionarySortAsc
            // 
            this.dictionarySortAsc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dictionarySortAsc.Image = global::CrosswordApplication.Properties.Resources.arrow_270;
            this.dictionarySortAsc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dictionarySortAsc.Name = "dictionarySortAsc";
            this.dictionarySortAsc.Size = new System.Drawing.Size(23, 22);
            this.dictionarySortAsc.Text = "toolStripButton2";
            this.dictionarySortAsc.Click += new System.EventHandler(this.dictionarySortAsc_Click);
            // 
            // board
            // 
            this.board.AllowUserToAddRows = false;
            this.board.AllowUserToDeleteRows = false;
            this.board.AllowUserToResizeColumns = false;
            this.board.AllowUserToResizeRows = false;
            this.board.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.board.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.board.ColumnHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.board.DefaultCellStyle = dataGridViewCellStyle4;
            this.board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board.Location = new System.Drawing.Point(0, 0);
            this.board.Margin = new System.Windows.Forms.Padding(10);
            this.board.MultiSelect = false;
            this.board.Name = "board";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.board.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.board.RowHeadersVisible = false;
            this.board.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.board.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.board.Size = new System.Drawing.Size(498, 535);
            this.board.TabIndex = 0;
            this.board.Visible = false;
            // 
            // questionsListBox
            // 
            this.questionsListBox.AllowUserToAddRows = false;
            this.questionsListBox.AllowUserToDeleteRows = false;
            this.questionsListBox.AllowUserToResizeColumns = false;
            this.questionsListBox.AllowUserToResizeRows = false;
            this.questionsListBox.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.questionsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.questionsListBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.questionsListBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wordColumnQuestion,
            this.descriptionColumnQuestion});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.questionsListBox.DefaultCellStyle = dataGridViewCellStyle6;
            this.questionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questionsListBox.Location = new System.Drawing.Point(0, 25);
            this.questionsListBox.MultiSelect = false;
            this.questionsListBox.Name = "questionsListBox";
            this.questionsListBox.RowHeadersVisible = false;
            this.questionsListBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.questionsListBox.Size = new System.Drawing.Size(278, 272);
            this.questionsListBox.TabIndex = 3;
            // 
            // wordColumnQuestion
            // 
            this.wordColumnQuestion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.wordColumnQuestion.FillWeight = 40F;
            this.wordColumnQuestion.HeaderText = "Слово";
            this.wordColumnQuestion.Name = "wordColumnQuestion";
            this.wordColumnQuestion.ReadOnly = true;
            // 
            // descriptionColumnQuestion
            // 
            this.descriptionColumnQuestion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionColumnQuestion.FillWeight = 40F;
            this.descriptionColumnQuestion.HeaderText = "Описание";
            this.descriptionColumnQuestion.Name = "descriptionColumnQuestion";
            this.descriptionColumnQuestion.ReadOnly = true;
            // 
            // dictionaryListBox
            // 
            this.dictionaryListBox.AllowUserToAddRows = false;
            this.dictionaryListBox.AllowUserToDeleteRows = false;
            this.dictionaryListBox.AllowUserToResizeColumns = false;
            this.dictionaryListBox.AllowUserToResizeRows = false;
            this.dictionaryListBox.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dictionaryListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dictionaryListBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dictionaryListBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wordColumn,
            this.descriptionColumn});
            this.dictionaryListBox.DefaultCellStyle = dataGridViewCellStyle6;
            this.dictionaryListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dictionaryListBox.Location = new System.Drawing.Point(0, 25);
            this.dictionaryListBox.MultiSelect = false;
            this.dictionaryListBox.Name = "dictionaryListBox";
            this.dictionaryListBox.RowHeadersVisible = false;
            this.dictionaryListBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dictionaryListBox.Size = new System.Drawing.Size(278, 207);
            this.dictionaryListBox.TabIndex = 3;
            this.dictionaryListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dictionaryListBox_MouseDown);
            // 
            // wordColumn
            // 
            this.wordColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.wordColumn.FillWeight = 40F;
            this.wordColumn.HeaderText = "Слово";
            this.wordColumn.Name = "wordColumn";
            this.wordColumn.ReadOnly = true;
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionColumn.FillWeight = 60F;
            this.descriptionColumn.HeaderText = "Описание";
            this.descriptionColumn.Name = "descriptionColumn";
            this.descriptionColumn.ReadOnly = true;
            // 
            // questionsUserList
            // 
            this.questionsUserList.AllowUserToAddRows = false;
            this.questionsUserList.AllowUserToDeleteRows = false;
            this.questionsUserList.AllowUserToResizeColumns = false;
            this.questionsUserList.AllowUserToResizeRows = false;
            this.questionsUserList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.questionsUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.questionsUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DescriptionUserColumn});
            this.questionsUserList.DefaultCellStyle = dataGridViewCellStyle6;
            this.questionsUserList.Dock = System.Windows.Forms.DockStyle.Top;
            this.questionsUserList.Location = new System.Drawing.Point(0, 0);
            this.questionsUserList.MultiSelect = false;
            this.questionsUserList.Name = "questionsUserList";
            this.questionsUserList.ReadOnly = true;
            this.questionsUserList.RowHeadersVisible = false;
            this.questionsUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.questionsUserList.Size = new System.Drawing.Size(280, 400);
            this.questionsUserList.TabIndex = 0;
            // 
            // DescriptionUserColumn
            // 
            this.DescriptionUserColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DescriptionUserColumn.HeaderText = "Описание";
            this.DescriptionUserColumn.Name = "DescriptionUserColumn";
            this.DescriptionUserColumn.ReadOnly = true;
            // 
            // CrosswordEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.rootSplitContainer);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CrosswordEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CrosswordEditForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.rootSplitContainer.Panel1.ResumeLayout(false);
            this.rootSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rootSplitContainer)).EndInit();
            this.rootSplitContainer.ResumeLayout(false);
            this.questionsUserPanel.ResumeLayout(false);
            this.questionsAdminSplitContainer.Panel1.ResumeLayout(false);
            this.questionsAdminSplitContainer.Panel1.PerformLayout();
            this.questionsAdminSplitContainer.Panel2.ResumeLayout(false);
            this.questionsAdminSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.questionsAdminSplitContainer)).EndInit();
            this.questionsAdminSplitContainer.ResumeLayout(false);
            this.questionsToolStrip.ResumeLayout(false);
            this.questionsToolStrip.PerformLayout();
            this.dictionaryToolStrip.ResumeLayout(false);
            this.dictionaryToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsListBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryListBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsUserList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem кроссвордToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem словарьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDictionaryToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.SplitContainer rootSplitContainer;
        private System.Windows.Forms.SplitContainer questionsAdminSplitContainer;
        private System.Windows.Forms.ToolStrip dictionaryToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton;
        private System.Windows.Forms.ToolStrip questionsToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Label loadDictionaryHintLabel;
        private System.Windows.Forms.ToolStripMenuItem loadCrosswordToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem newCrosswordToolStripMenu;
        private CustomDataGridView dictionaryListBox;
        private CustomDataGridView board;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
        private CustomDataGridView questionsListBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordColumnQuestion;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumnQuestion;
        private System.Windows.Forms.ToolStripMenuItem saveCrosswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.Panel questionsUserPanel;
        private CustomDataGridView questionsUserList;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionUserColumn;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userguideToolStripMenuItem;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Button openLetterButton;
        private System.Windows.Forms.Button openWordButton;
        private System.Windows.Forms.ToolStripButton dictionarySortDesc;
        private System.Windows.Forms.ToolStripButton dictionarySortAsc;
    }
}