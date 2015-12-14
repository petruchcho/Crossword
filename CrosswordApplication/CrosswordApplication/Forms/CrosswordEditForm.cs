using System;
using System.Drawing;
using System.Security.Cryptography;
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

        public DataGridViewCrosswordDrawer(DataGridView board)
        {
            this.board = board;
        }

        public void Draw(global::Crossword.Crossword crossword)
        {
            InitBoard(crossword);
            ShowWords(crossword);
        }

        private void InitBoard(global::Crossword.Crossword crossword)
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

            InitDragAndDrop(crossword);
        }

        private void InitDragAndDrop(global::Crossword.Crossword crossword)
        {
            board.AllowDrop = true;
            board.DragOver += (sender, args) =>
            {
                //string word = args.Data.GetData(typeof (string)).ToString();
                args.Effect = DragDropEffects.Move;

                //var recalculatedPoint = board.PointToClient(new Point(args.X, args.Y));

                //var curPositionInfo = board.HitTest(recalculatedPoint.X, recalculatedPoint.Y);
                //if (curPositionInfo.Type == DataGridViewHitTestType.Cell)
                //{
                //    if (board.CurrentCell != board[curPositionInfo.ColumnIndex, curPositionInfo.RowIndex])
                //    {
                //        board.CurrentCell = board[curPositionInfo.ColumnIndex, curPositionInfo.RowIndex];
                //    }
                //    SetAvailableCell(curPositionInfo.ColumnIndex, curPositionInfo.RowIndex, word[0].ToString());
                //}
            };

            board.DragLeave += (sender, args) => CleanAllPreviews();
            board.DragDrop += (sender, args) => CleanAllPreviews();

            board.DragEnter += (sender, args) =>
            {
                args.Effect = DragDropEffects.Move;
                ShowPreviews(crossword, ExtractDataFromDragAndDrop(args));
            };
        }

        private static DictionaryWord ExtractDataFromDragAndDrop(DragEventArgs args)
        {
            return new DictionaryWord(args.Data.GetData(typeof(string)).ToString());
        }

        private void ShowWords(global::Crossword.Crossword crossword)
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

        private void PreviewWord(PreviewCrosswordWord crosswordWord)
        {
            var word = crosswordWord.Word;
            for (var i = 0; i < word.Length; i++)
            {
                int curX;
                int curY;
                crosswordWord.PositionAtIndex(i, out curX, out curY);
                SetPreviewCell(curX, curY, "");
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

        private void SetPreviewCell(int x, int y, string value)
        {
            x += CROSSWORD_MARGIN;
            y += CROSSWORD_MARGIN;

            if ("Available".Equals(board[x, y].Tag))
            {
                return;
            }

            board[x, y].Style.BackColor = Color.LightGreen;
            board[x, y].Value = value;

            board[x, y].Tag = "Preview";
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
            board[x, y].Style.SelectionBackColor = Color.Black;
            board[x, y].Value = "";

            board[x, y].Tag = "";
        }
    }
}
