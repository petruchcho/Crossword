using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CrosswordApplication.CommonUtils;
using CrosswordApplication.Crossword;
using CrosswordApplication.Dictionary;
using Orientation = CrosswordApplication.Crossword.Orientation;

namespace CrosswordApplication.Forms
{
    public partial class CrosswordEditForm : Form
    {
        private global::Crossword.Crossword crossword;
        private Dictionary.Dictionary dictionary;

        private ICrosswordDrawer crosswordDrawer;

        public UserRole userRole;

        public CrosswordEditForm(UserRole userRole)
        {
            this.userRole = userRole;
            InitializeComponent();
        }

        private void CrosswordEditForm_Load(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void newCrosswordToolStripMenu_Click(object sender, EventArgs e)
        {
            // TODO Save Existed

            crossword = new global::Crossword.Crossword();
            SetCrossword();
            UpdateUi();
        }

        private void loadDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO Save Existed
            //            if (dictionary != null)
            //            {
            //                dictionary.Save(res => { });
            //            }

            dictionary = new Dictionary.Dictionary();
            dictionary.Load(res =>
            {
                SetDictionary();
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
                SetCrossword();
            });
        }

        private void SaveCrosswordWithDialog()
        {
            if (crossword == null || crossword.IsEmpty())
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
            // TODO Remove 100
            for (var i = 0; i < Math.Min(100, dictionary.DictionaryWords.Length); i++)
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
            if (crosswordDrawer == null)
            {
                var wordSelectionListener = new WordSelectionListener(crosswordWord =>
                {
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
                    crosswordDrawer.Draw(crossword);
                },
                () =>
                {
                    // Size changed
                }, 
                crosswordWord =>
                {
                    // Word deleted
                    ConfigureQuestionsAdminList();
                    questionsListBox.ClearSelection();
                    crosswordDrawer.Draw(crossword);
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

            questionsListBox.SelectionChanged -= QuestionBoxSelectionChanges;
            questionsListBox.SelectionChanged += QuestionBoxSelectionChanges;
        }

        private void QuestionBoxSelectionChanges(object sender, EventArgs args)
        {
            if (questionsListBox.SelectedCells.Count == 0)
            {
                crosswordDrawer.SelectWord(null);
                return;
            }
            var word = questionsListBox.SelectedCells[0].Value.ToString();
            var selectedWord = crossword.GetCrosswordWordForWord(word);
            if (selectedWord != null)
            {
                crosswordDrawer.SelectWord(selectedWord);
            }
        }

        private void QuestionUserListSelectionChanges(object sender, EventArgs args)
        {
            if (questionsUserList.SelectedCells.Count == 0)
            {
                crosswordDrawer.SelectWord(null);
                return;
            }
            var description = questionsUserList.SelectedCells[0].Value.ToString();
            var selectedWord = crossword.GetCrosswordWordForDescription(description);
            if (selectedWord != null)
            {
                crosswordDrawer.SelectWord(selectedWord);
            }
        }

        private void UpdateUi()
        {
            словарьToolStripMenuItem.Visible = userRole == UserRole.Administrator;
            newCrosswordToolStripMenu.Visible = userRole == UserRole.Administrator;
            параметрыToolStripMenuItem.Visible = userRole == UserRole.Administrator;

            questionsAdminSplitContainer.Visible = userRole == UserRole.Administrator;
            questionsUserPanel.Visible = userRole == UserRole.User;

            bool dictionaryLoaded = dictionary != null && dictionary.IsLoaded();
            bool crosswordLoaded = crossword != null;

            dictionaryToolStrip.Enabled = dictionaryLoaded;
            dictionaryListBox.Visible = dictionaryLoaded;

            newCrosswordToolStripMenu.Enabled = true;
            loadCrosswordToolStripMenu.Enabled = true;
            saveCrosswordToolStripMenuItem.Enabled = crosswordLoaded;

            сгенерироватьToolStripMenuItem.Enabled = false;

            board.Visible = crosswordLoaded;
        }

        private void dictionaryListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (dictionaryListBox.SelectedRows.Count == 0) return;
            dictionaryListBox.DoDragDrop(dictionaryListBox.SelectedCells[0].Value + " " +
                dictionaryListBox.SelectedCells[1].Value, DragDropEffects.Move);
        }

        private void toolStripButton_Click(object sender, EventArgs e)
        {

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

            public void Draw(global::Crossword.Crossword crossword)
            {
                this.crossword = crossword;
                InitBoard();
                ShowWords();

                if (userRole == UserRole.User)
                {
                    ShowProgress();
                }
            }

            private void InitBoard()
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

                board.KeyDown += (sender, e) =>
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
                };

                if (userRole == UserRole.Administrator)
                {
                    board.KeyDown -= DeletePress;
                    board.KeyDown += DeletePress;
                }

                board.CellValueChanged += (sender, args) =>
                {
                    try
                    {
                        string curValue = board[args.ColumnIndex, args.RowIndex].Value.ToString();
                        if (curValue.Length > 1)
                        {
                            board[args.ColumnIndex, args.RowIndex].Value =
                                board[args.ColumnIndex, args.RowIndex].Value.ToString()[0];
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        board[args.ColumnIndex, args.RowIndex].Value =
                            board[args.ColumnIndex, args.RowIndex].Value.ToString().ToUpper();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                };

                board.CellClick += (sender, args) =>
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
                };

                if (userRole == UserRole.Administrator)
                {
                    InitDragAndDrop();
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

            public void SelectWord(CrosswordWord crosswordWord)
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
    }

    interface ICrosswordDrawer
    {
        void Draw(global::Crossword.Crossword crossword);

        void SelectWord(CrosswordWord crosswordWord);
    }

}

/* Cells documentation
   Available = white cell with letter
   Select = was selected by user(for example by tapping on question in the list)
   Preview = there can be a word
   Highlight = there will be a word after you drop it
*/