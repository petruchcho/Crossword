using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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

        public CrosswordEditForm()
        {
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


        private void loadCrosswordToolStripMenu_Click(object sender, EventArgs e)
        {
            // TODO Save Existed

            crossword = new global::Crossword.Crossword();
            crossword.Load(res =>
            {
                SetCrossword();
            });
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

            var items = dictionaryListBox.Items;
            // TODO Remove 100
            for (var i = 0; i < Math.Min(100, dictionary.DictionaryWords.Length); i++)
            {
                items.Add(dictionary.DictionaryWords[i].ToString());
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
                crosswordDrawer = new DataGridViewCrosswordDrawer(board);
            }
           
            crossword.SetCrosswordStateListener(new CrosswordStateListener(
                crosswordWord =>
                {
                    // Word added
                    questionsListBox.Items.Add(crosswordWord.ToString());
                    crosswordDrawer.Draw(crossword);
                },
                () =>
                {
                    // Size changed
                }));
            crosswordDrawer.Draw(crossword);

            UpdateUi();
            board.Focus();
        }

        private void UpdateUi()
        {
            bool dictionaryLoaded = dictionary != null && dictionary.IsLoaded();
            bool crosswordLoaded = dictionaryLoaded && crossword != null;

            dictionaryToolStrip.Enabled = dictionaryLoaded;
            dictionaryListBox.Visible = dictionaryLoaded;

            newCrosswordToolStripMenu.Enabled = dictionaryLoaded;
            loadCrosswordToolStripMenu.Enabled = dictionaryLoaded;
            board.Visible = crosswordLoaded;
        }

        private void dictionaryListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (dictionaryListBox.SelectedItem == null) return;
            dictionaryListBox.DoDragDrop(dictionaryListBox.SelectedItem, DragDropEffects.Move);
        }
    }

    interface ICrosswordDrawer
    {
        void Draw(global::Crossword.Crossword crossword);
    }

    class DataGridViewCrosswordDrawer : ICrosswordDrawer
    {
        private static readonly int CROSSWORD_MARGIN = 0;
        private readonly DataGridView board;

        Orientation preferedOrientation;

        private global::Crossword.Crossword crossword;

        public DataGridViewCrosswordDrawer(DataGridView board)
        {
            this.board = board;
        }

        public void Draw(global::Crossword.Crossword crossword)
        {
            this.crossword = crossword;
            InitBoard();
            ShowWords();
        }

        private void InitBoard()
        {
            board.BackgroundColor = Color.Black;

            int crosswordSize = Math.Max(crossword.Width, crossword.Height) + CROSSWORD_MARGIN * 2;

            board.Columns.Clear();
            board.Rows.Clear();

            for (int i = 0; i < crosswordSize; i++)
            {
                board.Columns.Add(new DataGridViewTextBoxColumn());
            }

            for (int i = 0; i < crosswordSize; i++)
            {
                board.Rows.Add();
            }

            // Set size of each column
            foreach (DataGridViewColumn column in board.Columns)
            {
                column.Width = (int)Math.Round(1.0 * board.Width / crosswordSize);
            }

            // Set size of each Row
            foreach (DataGridViewRow row in board.Rows)
            {
                row.Height = (int)Math.Round(1.0 * board.Height / crosswordSize);
            }

            for (int i = 0; i < crosswordSize; i++)
                for (int j = 0; j < crosswordSize; j++)
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

            InitDragAndDrop();
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

        private void ShowPreviews(global::Crossword.Crossword crossword, DictionaryWord word)
        {
            var previews = crossword.GetPreviewsPositions(word);
            if (previews == null)
            {
                // TODO ????
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
                SetAvailableCell(curX, curY, word[i].ToString());
            }
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

        private void SetPreviewCell(int x, int y, string value)
        {
            x += CROSSWORD_MARGIN;
            y += CROSSWORD_MARGIN;

            if ("Available".Equals(board[x, y].Tag))
            {
                return;
            }

            board[x, y].Style.BackColor = Color.LightGreen;
            //board[x, y].Value = value;

            board[x, y].Tag = "Preview";
        }

        private void SetHighlightedCell(int x, int y, string value)
        {
            x += CROSSWORD_MARGIN;
            y += CROSSWORD_MARGIN;

            if ("Available".Equals(board[x, y].Tag))
            {
                return;
            }

            board[x, y].Style.BackColor = Color.ForestGreen;
            board[x, y].Value = value;

            board[x, y].Tag = "Highlight";
        }

        private void SetAvailableCell(int x, int y, string value)
        {
            x += CROSSWORD_MARGIN;
            y += CROSSWORD_MARGIN;

            board[x, y].ReadOnly = false;
            board[x, y].Style.BackColor = Color.White;
            board[x, y].Value = value;

            board[x, y].Tag = "Available";
        }

        private void CleanCell(int x, int y)
        {
            x += CROSSWORD_MARGIN;
            y += CROSSWORD_MARGIN;

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
                    HighlightWord(crossword.GetBestHighlight(word, x, y, preferedOrientation));
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

            CleanBoardFromTemporaryCells();

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
                crossword.AddWord(new CrosswordWord(crossword, crossword.GetBestHighlight(word, x, y, preferedOrientation)));
                // }
            }
        }

        private void DragCleanBoard(object sender, EventArgs eventArgs)
        {
            CleanBoardFromTemporaryCells();
        }
    }

    class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            DoubleBuffered = true;
        }     
    }

    class CrosswordStateListener : global::Crossword.Crossword.ICrosswordStateListener
    {
        private readonly Action<CrosswordWord> _wordAddAction;
        private readonly Action _sizeChangedAction;

        public CrosswordStateListener(Action<CrosswordWord> wordAddAction, Action sizeChangedAction)
        {
            this._wordAddAction = wordAddAction;
            this._sizeChangedAction = sizeChangedAction;
        }

        public void OnWordAdded(CrosswordWord crosswordWord)
        {
            _wordAddAction.Invoke(crosswordWord);
        }

        public void OnSizeChanged()
        {
            _sizeChangedAction.Invoke();
        }
    }
}
