using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDictionary();
        }

        private void LoadDictionary()
        {
            // TODO Save Existed
            if (dictionary != null)
            {
                dictionary.Save(res => { });
            }

            dictionary = new Dictionary.Dictionary();
            dictionary.Load(res =>
            {
                SetDictionary();
            });
        }

        private void SetDictionary()
        {
            if (dictionary == null || !dictionary.IsLoaded())
            {
                dictionaryListBox.Visible = false;
                return;
            }

            var items = dictionaryListBox.Items;
            // TODO Remove 100
            for (var i = 0; i < Math.Min(100, dictionary.DictionaryWords.Length); i++)
            {
                items.Add(dictionary.DictionaryWords[i].ToString());
            }

            dictionaryListBox.Visible = true;
        }

        private void загрузитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            crossword = new global::Crossword.Crossword();
            crossword.Load(res =>
            {
                ShowCrossword();
            });
        }

        private void ShowCrossword()
        {
            if (crosswordDrawer == null)
            {
                crosswordDrawer = new DataGridViewCrosswordDrawer(board);
            }
            crosswordDrawer.Draw(crossword);
        }

        private void board_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                board.BeginEdit(true);
            }
        }
    }

    interface ICrosswordDrawer
    {
        void Draw(global::Crossword.Crossword crossword);
    }

    class DataGridViewCrosswordDrawer : ICrosswordDrawer
    {
        private static readonly int CROSSWORD_MARGIN = 2;
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
            
            board.Visible = true;
            board.Focus();
        }

        private void ShowWords(global::Crossword.Crossword crossword)
        {
            foreach (var crosswordWord in crossword.CrosswordWords)
            {
                string word = crosswordWord.Word;

                for (int i = 0; i < word.Length; i++)
                {
                    bool hasNext = i < word.Length - 1;
                    int curX = crosswordWord.Position.X;
                    int curY = crosswordWord.Position.Y;
                    int nextX = crosswordWord.Position.X;
                    int nextY = crosswordWord.Position.Y;

                    if (crosswordWord.Position.Orientation == Orientation.Vertical)
                    {
                        curX += i;
                        nextX += i + 1;
                    }
                    else
                    {
                        curY += i;
                        nextY += i + 1;
                    }

                    SetCell(curX, curY, word[i].ToString(), hasNext, nextX, nextY);
                }
            }
        }

        private void SetCell(int x, int y, String value, bool hasNext, int nextX, int nextY)
        {
            x += CROSSWORD_MARGIN;
            y += CROSSWORD_MARGIN;

            board[y, x].ReadOnly = false;
            board[y, x].Style.BackColor = Color.White;
            board[y, x].Value = value;

            
        }
    }
}
