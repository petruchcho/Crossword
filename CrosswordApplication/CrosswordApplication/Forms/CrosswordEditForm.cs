using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CrosswordApplication.CommonUtils;
using CrosswordApplication.Crossword;
using CrosswordApplication.Dictionary;
using System.Diagnostics;
using System.ComponentModel;
using Orientation = CrosswordApplication.Crossword.Orientation;

namespace CrosswordApplication.Forms
{
    public partial class CrosswordEditForm : Form
    {
        private global::Crossword.Crossword crossword;
        private Dictionary.Dictionary dictionary;

        private ICrosswordDrawer crosswordDrawer;

        private bool IsFinished = false;

        public UserRole userRole;

        public CrosswordEditForm(UserRole userRole)
        {
            this.userRole = userRole;
            InitializeComponent();

            FormClosing += (sender, args) =>
            {
                SaveCrosswordWithDialog();
            };
        }

        private void CrosswordEditForm_Load(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void newCrosswordToolStripMenu_Click(object sender, EventArgs e)
        {
            SaveCrosswordWithDialog();

            crossword = new global::Crossword.Crossword();
            SetCrossword();
            UpdateUi();
        }

        private void loadDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dictionary = new Dictionary.Dictionary();
            dictionary.Load(res =>
            {
                if (res)
                {
                    SetDictionary();
                }
            });
        }

        private void saveCrosswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCrossword();
        }

        private void loadCrosswordToolStripMenu_Click(object sender, EventArgs e)
        {
            SaveCrosswordWithDialog();

            crossword = new global::Crossword.Crossword();
            crossword.Load(userRole, 
                res =>
            {
                if (res)
                {
                    SetCrossword();
                }
            });
        }

        private void SaveCrosswordWithDialog()
        {
            if (crossword == null || crossword.IsEmpty() || IsFinished)
            {
                return;
            }
            if (MessageBox.Show("Вы хотите сохранить текущий кроссворд?", "Сохранить", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                SaveCrossword();
            }
        }

        private void SaveCrossword()
        {
            if (crossword != null)
            {
                if (!crossword.IsCorrectCrossword())
                {
                    if (MessageBox.Show(
                        "Кроссворд не удовлетворяет некоторым правилам построения. Вы уверены, что хотите сохранить его?",
                        "Предупреждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                crossword.Save(userRole, BuildProgress(), res => { });
            }
        }

        private void SetDictionary()
        {
            if (dictionary == null || !dictionary.IsLoaded())
            {
                UpdateUi();
                return;
            }

            loadCrosswordToolStripMenu.Enabled = true;
            dictionaryListBox.Visible = true;

            var items = dictionaryListBox.Rows;
            for (var i = 0; i < dictionary.DictionaryWords.Length; i++)
            {
                var dictionaryWord = dictionary.DictionaryWords[i];
                items.Add(dictionaryWord.Word, dictionaryWord.Description);
            }

            UpdateUi();
        }

        private void SetCrossword()
        {
            if (crossword == null)
            {
                UpdateUi();
                return;
            }
            IsFinished = false;
            if (crosswordDrawer == null)
            {
                var wordSelectionListener = new WordSelectionListener(crosswordWord =>
                {
                    questionsListBox.ClearSelection();
                    questionsUserList.ClearSelection();
                    if (crosswordWord == null)
                    {
                        return;
                    }
                    for (int i = 0; i < questionsListBox.RowCount; i++)
                    {
                        if (questionsListBox.Rows[i].Cells[0].Value.Equals(crosswordWord.Word))
                        {
                            questionsListBox.Rows[i].Selected = true;
                            break;
                        }
                    }

                    for (int i = 0; i < questionsUserList.RowCount; i++)
                    {
                        if (questionsUserList.Rows[i].Cells[0].Value.Equals(crosswordWord.Description))
                        {
                            questionsUserList.Rows[i].Selected = true;
                            break;
                        }
                    }
                });
                crosswordDrawer = new DataGridViewCrosswordDrawer(userRole, board, wordSelectionListener);
            }
           
            crossword.SetCrosswordStateListener(new global::Crossword.Crossword.CrosswordStateListener(
                crosswordWord =>
                {
                    // Word added
                    questionsListBox.Rows.Add(crosswordWord.Word, crosswordWord.Description);
                    questionsListBox.ClearSelection();
                    crosswordDrawer.ReDraw(BuildProgress(), IsFinished);
                },
                () =>
                {
                    // Size changed
                    crosswordDrawer.ReDraw(BuildProgress(), IsFinished);
                }, 
                crosswordWord =>
                {
                    // Word deleted
                    ConfigureQuestionsAdminList();
                    questionsListBox.ClearSelection();
                    crosswordDrawer.ReDraw(BuildProgress(), IsFinished);
                }));
            crosswordDrawer.Draw(crossword);

            ConfigureQuestionsUserList();
            ConfigureQuestionsAdminList();

            UpdateUi();
            board.Focus();
        }

        private void ConfigureQuestionsUserList()
        {
            questionsUserList.Rows.Clear();
            questionsUserList.ClearSelection();
            if (crossword != null)
            {
                foreach (var crosswordWord in crossword.CrosswordWords)
                {
                    questionsUserList.Rows.Add(crosswordWord.Description);
                }
            }
            questionsUserList.ClearSelection();

            questionsUserList.SelectionChanged -= QuestionUserListSelectionChanges;
            questionsUserList.SelectionChanged += QuestionUserListSelectionChanges;
        }

        private void ConfigureQuestionsAdminList()
        {
            questionsListBox.Rows.Clear();
            questionsListBox.ClearSelection();
            if (crossword != null)
            {
                foreach (var crosswordWord in crossword.CrosswordWords)
                {
                    questionsListBox.Rows.Add(crosswordWord.Word, crosswordWord.Description);
                }
            }
            questionsUserList.ClearSelection();

            questionsListBox.SelectionChanged -= QuestionBoxSelectionChanges;
            questionsListBox.SelectionChanged += QuestionBoxSelectionChanges;
        }

        private void QuestionBoxSelectionChanges(object sender, EventArgs args)
        {
            if (questionsListBox.SelectedCells.Count == 0)
            {
                crosswordDrawer.SelectWord(null, IsFinished);
                return;
            }
            var word = questionsListBox.SelectedCells[0].Value.ToString();
            var selectedWord = crossword.GetCrosswordWordForWord(word);
            if (selectedWord != null)
            {
                crosswordDrawer.SelectWord(selectedWord, IsFinished);
            }
        }

        private void QuestionUserListSelectionChanges(object sender, EventArgs args)
        {
            if (questionsUserList.SelectedCells.Count == 0)
            {
                crosswordDrawer.SelectWord(null, IsFinished);
                return;
            }
            var description = questionsUserList.SelectedCells[0].Value.ToString();
            var selectedWord = crossword.GetCrosswordWordForDescription(description);
            if (selectedWord != null)
            {
                crosswordDrawer.SelectWord(selectedWord, IsFinished);
            }
        }

        private void UpdateUi()
        {
            словарьToolStripMenuItem.Visible = userRole == UserRole.Administrator;
            newCrosswordToolStripMenu.Visible = userRole == UserRole.Administrator;
            parametersToolStripMenuItem.Visible = userRole == UserRole.Administrator;

            questionsAdminSplitContainer.Visible = userRole == UserRole.Administrator;
            questionsUserPanel.Visible = userRole == UserRole.User;

            if (userRole == UserRole.Administrator)
            {
                this.Text = "Менеджер кроссвордов";
                if (crossword != null && crossword.GetFilename() != null)
                {
                    this.Text += ": " + crossword.GetFilename();
                }
            }
            else
            {
                this.Text = "Разгадывание";
            }

            bool dictionaryLoaded = dictionary != null && dictionary.IsLoaded();
            bool crosswordLoaded = crossword != null;

            parametersToolStripMenuItem.Enabled = crosswordLoaded;

            dictionaryToolStrip.Enabled = dictionaryLoaded;
            dictionaryListBox.Visible = dictionaryLoaded;

            newCrosswordToolStripMenu.Enabled = true;
            loadCrosswordToolStripMenu.Enabled = true;
            saveCrosswordToolStripMenuItem.Enabled = crosswordLoaded && !IsFinished;

            generateToolStripMenuItem.Enabled = (userRole == UserRole.Administrator && dictionaryLoaded && crosswordLoaded)
                || (userRole == UserRole.User);

            board.Visible = crosswordLoaded;

            if (crossword != null && userRole == UserRole.User)
            {
                openLetterButton.Text = "Открыть букву(" + crossword.LetterHelpers + ")";
                openWordButton.Text = "Открыть слово(" + crossword.WordHelpers + ")";
                openWordButton.Enabled = crossword.WordHelpers > 0 && !IsFinished;
                openLetterButton.Enabled = crossword.LetterHelpers > 0 && !IsFinished;
                finishButton.Enabled = !IsFinished;
            }
            else
            {
                openLetterButton.Enabled = false;
                openWordButton.Enabled = false;
                finishButton.Enabled = false;
            }
        }

        private void dictionaryListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (dictionaryListBox.SelectedRows.Count == 0) return;
            dictionaryListBox.DoDragDrop(dictionaryListBox.SelectedCells[0].Value + " " +
                dictionaryListBox.SelectedCells[1].Value, DragDropEffects.Move);
        }

        private void toolStripButton_Click(object sender, EventArgs e)
        {
            FormUtils.OpenFormAndCloseCurrent(this, new DictionaryManagerForm());
        }

        private List<CrosswordLetter> BuildProgress()
        {
            var result = new List<CrosswordLetter>();
            for (var x = 0; x < board.ColumnCount; x++)
                for (var y = 0; y < board.RowCount; y++)
                {
                    if (board[x, y].Value != null)
                    {
                        string value = (string) board[x, y].Value;
                        if (value.Length > 0)
                        {
                            result.Add(new CrosswordLetter(value, new CrosswordWordPosition(x, y, Orientation.Horizontal)));
                        }
                    }
                }
            return result;
        } 

        class DataGridViewCrosswordDrawer : ICrosswordDrawer
        {
            private readonly DataGridView board;
            private readonly WordSelectionListener _wordSelectionListener;
            private readonly UserRole userRole;

            Orientation preferedOrientation;

            private CrosswordWord cachedSelectedWord;

            private global::Crossword.Crossword crossword;

            public DataGridViewCrosswordDrawer(UserRole userRole, DataGridView board, WordSelectionListener wordSelectionListener)
            {
                this.board = board;
                _wordSelectionListener = wordSelectionListener;
                this.userRole = userRole;
            }

            public void ReDraw(List<CrosswordLetter> progress, bool IsFinished)
            {
                var selectedCellX = -1;
                var selectedCellY = -1;
                if (board.CurrentCell != null)
                {
                    selectedCellX = board.CurrentCell.ColumnIndex;
                    selectedCellY = board.CurrentCell.RowIndex;
                }

                CleanBoard();
                ShowWords();

                if (userRole == UserRole.User)
                {
                    crossword.SetProgress(progress);
                    ShowProgress();
                }

                if (IsFinished)
                {
                    ShowResult(userRole);
                }

                if (selectedCellX > -1)
                {
                    board.CurrentCell = board[selectedCellX, selectedCellY];
                }
            }

            public void Draw(global::Crossword.Crossword crossword)
            {
                this.crossword = crossword;
                
                InitBoard();
                ShowWords();

                if (userRole == UserRole.User)
                {
                    ShowProgress();
                }


                board.CurrentCell = null;
                _wordSelectionListener.OnWordSelected(null);
            }

            private void InitBoard()
            {
                CleanBoard();

                board.KeyDown -= OnBoardOnKeyDown;
                board.KeyDown += OnBoardOnKeyDown;

                if (userRole == UserRole.Administrator)
                {
                    board.KeyDown -= DeletePress;
                    board.KeyDown += DeletePress;
                }

                board.CellValueChanged -= OnBoardOnCellValueChanged;
                board.CellValueChanged += OnBoardOnCellValueChanged;

                board.CellClick -= OnBoardOnCellClick;
                board.CellClick += OnBoardOnCellClick;

                if (userRole == UserRole.Administrator)
                {
                    InitDragAndDrop();
                }
            }

            private void CleanBoard()
            {
                board.BackgroundColor = Color.Black;
                int crosswordSize = Math.Max(crossword.Width, crossword.Height);

                board.Columns.Clear();
                board.Rows.Clear();

                for (int i = 0; i < crossword.Width; i++)
                {
                    board.Columns.Add(new DataGridViewTextBoxColumn());
                }

                for (int i = 0; i < crossword.Height; i++)
                {
                    board.Rows.Add();
                }

                // Set size of each column
                foreach (DataGridViewColumn column in board.Columns)
                {
                    column.Width = (int)Math.Round(1.0 * board.Width / crossword.Width);
                }

                // Set size of each Row
                foreach (DataGridViewRow row in board.Rows)
                {
                    row.Height = (int)Math.Round(1.0 * board.Height / crossword.Height);
                }

                for (int i = 0; i < crossword.Width; i++)
                    for (int j = 0; j < crossword.Height; j++)
                    {
                        board[i, j].ReadOnly = true;
                    }
            }

            public void ShowResult(UserRole userRole)
            {
                if (userRole != UserRole.User)
                {
                    return;
                }
                for (int i = 0; i < board.ColumnCount; i++)
                    for (int j = 0; j < board.RowCount; j++)
                    {
                        board[i, j].ReadOnly = true;
                    }
                foreach (var crosswordWord in crossword.CrosswordWords)
                {
                    var word = crosswordWord.Word;
                    for (var i = 0; i < word.Length; i++)
                    {
                        int curX;
                        int curY;
                        crosswordWord.PositionAtIndex(i, out curX, out curY);
                        SetCellCorrectness(curX, curY, word[i].ToString());
                    }
                }
            }

            private void OnBoardOnKeyDown(object sender, KeyEventArgs e)
            {
                try
                {
                    if (e.KeyCode == Keys.Enter && !board.CurrentCell.IsInEditMode)
                    {
                        board.BeginEdit(true);
                        e.Handled = true;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            private void OnBoardOnCellClick(object sender, DataGridViewCellEventArgs args)
            {
                var preferedOrientation = Orientation.Horizontal;
                if ((Control.ModifierKeys & Keys.Control) != 0)
                {
                    preferedOrientation = Orientation.Vertical;
                }

                var foundedWords = crossword.FindWordsAtPosition(args.ColumnIndex, args.RowIndex);
                if (foundedWords.Count == 0)
                {
                    return;
                }

                foreach (var crosswordWord in foundedWords)
                {
                    if (crosswordWord.Position.Orientation != preferedOrientation) continue;
                    _wordSelectionListener.OnWordSelected(crosswordWord);
                    return;
                }

                _wordSelectionListener.OnWordSelected(foundedWords[0]);
            }

            private void OnBoardOnCellValueChanged(object sender, DataGridViewCellEventArgs args)
            {
                try
                {
                    string curValue = board[args.ColumnIndex, args.RowIndex].Value.ToString();
                    if (curValue.Length > 1)
                    {
                        board[args.ColumnIndex, args.RowIndex].Value = board[args.ColumnIndex, args.RowIndex].Value.ToString()[0];
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                try
                {
                    if (!board[args.ColumnIndex, args.RowIndex].Value.ToString().ToUpper().Equals(board[args.ColumnIndex, args.RowIndex].Value))
                    {
                        board[args.ColumnIndex, args.RowIndex].Value = board[args.ColumnIndex, args.RowIndex].Value.ToString().ToUpper();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                try
                {
                    if (board[args.ColumnIndex, args.RowIndex].Value.ToString().Length > 0)
                    {
                        char c = board[args.ColumnIndex, args.RowIndex].Value.ToString()[0];
                        if (!(c >= 'А' && c <= 'Я'))
                        {
                            board[args.ColumnIndex, args.RowIndex].Value = "";
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            private void InitDragAndDrop()
            {
                board.AllowDrop = true;

                board.DragOver -= DragOverBoard;
                board.DragOver += DragOverBoard;

                board.DragLeave -= DragCleanBoard;
                board.DragLeave += DragCleanBoard;

                board.DragDrop -= DragDropBoard;
                board.DragDrop += DragDropBoard;

                board.DragEnter -= DragEnterBoard;
                board.DragEnter += DragEnterBoard;
            }

            private static DictionaryWord ExtractDataFromDragAndDrop(DragEventArgs args)
            {
                return new DictionaryWord(args.Data.GetData(typeof(string)).ToString());
            }

            private void CleanBoardFromTemporaryCells()
            {
                CleanAllPreviews();
                CleanHighlight();
            }

            private void ShowWords()
            {
                foreach (var crosswordWord in crossword.CrosswordWords)
                {
                    ShowWord(crosswordWord);
                }
            }

            private void ShowProgress()
            {
                foreach (var letter in crossword.Progress)
                {
                    SetAvailableCell(letter.Position.X, letter.Position.Y, letter.Letter);
                }
            }

            private void ShowPreviews(global::Crossword.Crossword crossword, DictionaryWord word)
            {
                var previews = crossword.GetPreviewsPositions(word);
                if (previews == null)
                {
                    return;
                }
                foreach (var crosswordWord in previews)
                {
                    PreviewWord(crosswordWord);
                }
            }

            private void ShowWord(CrosswordWord crosswordWord)
            {
                var word = crosswordWord.Word;
                for (var i = 0; i < word.Length; i++)
                {
                    int curX;
                    int curY;
                    crosswordWord.PositionAtIndex(i, out curX, out curY);
                    if (crosswordWord.IsResolved || userRole == UserRole.Administrator)
                    {
                        SetAvailableCell(curX, curY, word[i].ToString());
                    }
                    else
                    {
                        SetAvailableCell(curX, curY, "");
                    }
                }
            }

            public void SelectWord(CrosswordWord crosswordWord, bool isFinished)
            {
                CleanSelect();
                if (crosswordWord == null)
                {
                    return;
                }

                cachedSelectedWord = crosswordWord;
                var word = crosswordWord.Word;
                for (var i = 0; i < word.Length; i++)
                {
                    int curX;
                    int curY;
                    crosswordWord.PositionAtIndex(i, out curX, out curY);
                    SetSelectedCell(curX, curY);
                    if (isFinished)
                    {
                        board[curX, curY].ReadOnly = true;
                    }
                }
                board.Focus();
            }

            private void HighlightWord(CrosswordWord crosswordWord)
            {
                if (crosswordWord == null)
                {
                    return;
                }
                var word = crosswordWord.Word;
                for (var i = 0; i < word.Length; i++)
                {
                    int curX;
                    int curY;
                    crosswordWord.PositionAtIndex(i, out curX, out curY);
                    SetHighlightedCell(curX, curY, word[i].ToString());
                }
            }

            private void PreviewWord(PreviewCrosswordWord crosswordWord)
            {
                var word = crosswordWord.Word;
                for (var i = 0; i < word.Length; i++)
                {
                    int curX;
                    int curY;
                    crosswordWord.PositionAtIndex(i, out curX, out curY);
                    SetPreviewCell(curX, curY, crosswordWord.Word[0].ToString());
                }
            }

            private void CleanAllPreviews()
            {
                for (var x = 0; x < board.ColumnCount; x++)
                    for (var y = 0; y < board.RowCount; y++)
                    {
                        if ("Preview".Equals(board[x, y].Tag))
                        {
                            CleanCell(x, y);
                        }
                    }
            }

            private void CleanHighlight()
            {
                for (var x = 0; x < board.ColumnCount; x++)
                    for (var y = 0; y < board.RowCount; y++)
                    {
                        if ("Highlight".Equals(board[x, y].Tag))
                        {
                            CleanCell(x, y);
                        }
                    }
            }

            private void CleanSelect()
            {
                cachedSelectedWord = null;
                for (var x = 0; x < board.ColumnCount; x++)
                    for (var y = 0; y < board.RowCount; y++)
                    {
                        if ("Select".Equals(board[x, y].Tag))
                        {
                            if (board[x, y].Value == null)
                            {
                                board[x, y].Value = "";
                            }
                            SetAvailableCell(x, y, board[x, y].Value.ToString());
                        }
                    }
            }

            private void SetPreviewCell(int x, int y, string value)
            {
                if ("Available".Equals(board[x, y].Tag) || "Select".Equals(board[x, y].Tag))
                {
                    return;
                }

                board[x, y].Style.BackColor = Color.LightGreen;

                board[x, y].Tag = "Preview";
            }

            private void SetHighlightedCell(int x, int y, string value)
            {
                if ("Available".Equals(board[x, y].Tag) || "Select".Equals(board[x, y].Tag))
                {
                    return;
                }

                board[x, y].Style.BackColor = Color.ForestGreen;
                board[x, y].Value = value;

                board[x, y].Tag = "Highlight";
            }

            private void SetAvailableCell(int x, int y, string value)
            {
                board[x, y].ReadOnly = userRole != UserRole.User;
                board[x, y].Style.BackColor = Color.White;
                
                board[x, y].Value = value;

                board[x, y].Tag = "Available";
            }

            private void SetCellCorrectness(int x, int y, string value)
            {
                if (board[x, y].Value == null || board[x, y].Value.ToString().Length == 0)
                {
                    SetAvailableCell(x, y, value);
                    board[x, y].ReadOnly = true;
                }
                else
                {
                    Color color = board[x, y].Value.ToString().ToUpper().Equals(value.ToUpper()) ? Color.ForestGreen : Color.IndianRed;
                    SetAvailableCell(x, y, value);
                    board[x, y].ReadOnly = true;
                    //board[x, y].Style.BackColor = color;
                }
            }

            private void SetSelectedCell(int x, int y)
            {
                board[x, y].Style.BackColor = Color.LightSkyBlue;

                board[x, y].Tag = "Select";
            }

            private void CleanCell(int x, int y)
            {
                board[x, y].ReadOnly = true;
                board[x, y].Style.BackColor = Color.Black;
                //board[x, y].Style.SelectionBackColor = Color.Black;
                board[x, y].Value = "";

                board[x, y].Tag = "";
            }

            private void DragOverBoard(object sender, DragEventArgs args)
            {
                if ((args.KeyState & 2) == 2 || (args.KeyState & 8) == 8)
                {
                    preferedOrientation = Orientation.Vertical;
                }
                else
                {
                    preferedOrientation = Orientation.Horizontal;
                }

                args.Effect = DragDropEffects.Move;
                var word = ExtractDataFromDragAndDrop(args);

                var recalculatedPoint = board.PointToClient(new Point(args.X, args.Y));

                var curPositionInfo = board.HitTest(recalculatedPoint.X, recalculatedPoint.Y);
                if (curPositionInfo.Type == DataGridViewHitTestType.Cell)
                {
                    // if (board.CurrentCell != board[curPositionInfo.ColumnIndex, curPositionInfo.RowIndex])
                    // {
                    CleanHighlight();
                    ShowPreviews(crossword, ExtractDataFromDragAndDrop(args));
                    var x = curPositionInfo.ColumnIndex;
                    var y = curPositionInfo.RowIndex;
                    board.CurrentCell = board[x, y];
                    if (!"".Equals(board[x, y].Tag) || crossword.CrosswordWords.Count == 0)
                    {
                        HighlightWord(crossword.GetBestHighlight(word, x, y, preferedOrientation));
                    }
                    // }
                }
            }

            private void DragEnterBoard(object sender, DragEventArgs args)
            {
                args.Effect = DragDropEffects.Move;
                ShowPreviews(crossword, ExtractDataFromDragAndDrop(args));
            }

            private void DragDropBoard(object sender, DragEventArgs args)
            {
                if ((args.KeyState & 2) == 2 || (args.KeyState & 8) == 8)
                {
                    preferedOrientation = Orientation.Vertical;
                }
                else
                {
                    preferedOrientation = Orientation.Horizontal;
                }

                args.Effect = DragDropEffects.Move;
                var word = ExtractDataFromDragAndDrop(args);

                var recalculatedPoint = board.PointToClient(new Point(args.X, args.Y));

                var curPositionInfo = board.HitTest(recalculatedPoint.X, recalculatedPoint.Y);
                if (curPositionInfo.Type == DataGridViewHitTestType.Cell)
                {
                    // if (board.CurrentCell != board[curPositionInfo.ColumnIndex, curPositionInfo.RowIndex])
                    // {
                    CleanHighlight();
                    ShowPreviews(crossword, ExtractDataFromDragAndDrop(args));
                    var x = curPositionInfo.ColumnIndex;
                    var y = curPositionInfo.RowIndex;
                    board.CurrentCell = board[x, y];
                    var currentPreviewWord = crossword.GetBestHighlight(word, x, y, preferedOrientation);
                    if (currentPreviewWord != null)
                    {
                        crossword.AddWord(new CrosswordWord(crossword, currentPreviewWord));
                    }
                    // }
                }

                CleanBoardFromTemporaryCells();
            }

            private void DragCleanBoard(object sender, EventArgs eventArgs)
            {
                CleanBoardFromTemporaryCells();
            }

            private void DeletePress(object sender, KeyEventArgs e)
            {
                try
                {
                    if (e.KeyCode == Keys.Delete && userRole == UserRole.Administrator &&
                        !board.CurrentCell.IsInEditMode && cachedSelectedWord != null)
                    {
                        crossword.DeleteWord(cachedSelectedWord);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram aboutForm = new AboutProgram();
            aboutForm.ShowDialog();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userRole == UserRole.Administrator)
            {
                if (
                    new CrosswordGenerationParametersForm(CrosswordGenerationParametersForm.Type.GenerateWithoutDictionary, crossword, dictionary)
                        .ShowDialog() == DialogResult.OK)
                {
                    crosswordDrawer.ReDraw(BuildProgress(), IsFinished);
                    UpdateUi();
                }
            }
            else
            {
                SaveCrosswordWithDialog();
                crossword = new global::Crossword.Crossword();
                if (
                    new CrosswordGenerationParametersForm(CrosswordGenerationParametersForm.Type.GenerateWithDictionary, crossword)
                        .ShowDialog() == DialogResult.OK)
                {
                    SetCrossword();
                }
            }
        }

        private void openWordButton_Click(object sender, EventArgs e)
        {
            if (questionsUserList.SelectedCells.Count == 0)
            {
                ShowErrorDialog("Выберите слово");
                return;
            }
            var description = questionsUserList.SelectedCells[0].Value.ToString();
            var selectedWord = crossword.GetCrosswordWordForDescription(description);
            if (selectedWord != null)
            {
                bool isSelected = false;
                for (int i = 0; i < selectedWord.Word.Length; i++)
                {
                    int x, y;
                    selectedWord.PositionAtIndex(i, out x, out y);
                    isSelected |= "Select".Equals(board[x, y].Tag);
                }

                if (!isSelected)
                {
                    ShowErrorDialog("Выберите слово");
                    return;
                }

                for (int i = 0; i < selectedWord.Word.Length; i++)
                {
                    int x, y;
                    selectedWord.PositionAtIndex(i, out x, out y);
                    board[x, y].Value = selectedWord.Word[i];
                }
                crossword.WordHelpers--;
                crosswordDrawer.ReDraw(BuildProgress(), IsFinished);
                UpdateUi();
            }
            else
            {
                ShowErrorDialog("Выберите слово");
            }
        }

        private void openLetterButton_Click(object sender, EventArgs e)
        {
            var cell = board.CurrentCell;
            if (cell == null)
            {
                ShowErrorDialog("Выберите букву");
                return;
            }
            var crosswordLetter = crossword.GetLetterAtPosition(cell.ColumnIndex, cell.RowIndex);
            if (crosswordLetter == null)
            {
                ShowErrorDialog("Выберите букву");
                return;
            }
            board[cell.ColumnIndex, cell.RowIndex].Value = crosswordLetter.Letter;
            crossword.LetterHelpers--;
            crosswordDrawer.ReDraw(BuildProgress(), IsFinished);
            UpdateUi();
        }

        private void ShowErrorDialog(string message)
        {
            MessageBox.Show(message);
        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CrosswordGenerationParametersForm(CrosswordGenerationParametersForm.Type.Params, crossword).ShowDialog();
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            IsFinished = true;
            var result = Math.Round(crossword.GetResult(BuildProgress()), 2) + "%";
            MessageBox.Show("Кроссворд разгадан на " + result, "Результат игры");
            crosswordDrawer.ShowResult(userRole);
            UpdateUi();
        }

        private void userguideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String text = System.IO.File.ReadAllText(System.IO.Path.GetFullPath(@"..\..\pages") + @"\help.html");
                if (!(text.Contains("<head>") || text.Contains("</head>") || text.Contains("<body>") || text.Contains("</body>")))
                {
                    MessageBox.Show("Файл справки некорректен!");
                    return;
                }
                Process.Start(System.IO.Path.GetFullPath(@"..\..\pages") + @"\help.html");
            }
            catch (Exception e1)
            {
                MessageBox.Show("Файл справки отсутствует!");
            }
        }
    }

    interface ICrosswordDrawer
    {
        void Draw(global::Crossword.Crossword crossword);

        void SelectWord(CrosswordWord crosswordWord, bool isFinished);

        void ReDraw(List<CrosswordLetter> progress, bool IsFinished);

        void ShowResult(UserRole userRole);
    }

}

/* Cells documentation
   Available = white cell with letter
   Select = was selected by user(for example by tapping on question in the list)
   Preview = there can be a word
   Highlight = there will be a word after you drop it
*/