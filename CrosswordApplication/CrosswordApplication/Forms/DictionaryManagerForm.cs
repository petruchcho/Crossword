using System;
using System.Windows.Forms;
using CrosswordApplication.Dictionary;

namespace CrosswordApplication.Forms
{
    public partial class DictionaryManagerForm : Form
    {
        private string fileName;
        private Dictionary.Dictionary dictionary;

        private DictionaryWordComparator.SortDirection sortDirection;
        private DictionaryWordComparator.SortBy sortBy;

        private string selectedWord;

        public DictionaryManagerForm()
        {
            InitializeComponent();
        }

        public DictionaryManagerForm(string fileName)
        {
            this.fileName = fileName;
        }

        private void LoadDictionary()
        {   
            // TODO Save Existed
            if (dictionary != null)
            {
                dictionary.Save((res) => {});
            }
            else
            {
                ShowProgress();
                dictionary = new Dictionary.Dictionary();
                dictionary.Load((res) =>
                {
                    HideProgress();
                    UpdateUi();
                });
            }
        }
        
        private void UpdateUi()
        {
            if (dictionary == null || !dictionary.IsLoaded())
            {
                ShowEmptyState();
            }
            else
            {
                ShowDictionary();
            }
        }

        private void ShowDictionary()
        {
            var items = listView1.Items;
            items.Clear();
            for (var i = 0; i < dictionary.DictionaryWords.Length; i++)
            {
                items.Add(dictionary.DictionaryWords[i].ToString());
                if (i%200 == 0)
                {
                    listView1.Update();
                }
            }
        }

        private void ShowEmptyState()
        {
            throw new NotImplementedException();
        }

        private void DictionaryManagerForm_Load(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                LoadDictionary();
            }

            ShowDirectionButtons(false, false);
            ShowSortTypeButtons(false, false);
            ShowMask(false);
        }

        private void ShowDirectionButtons(bool ascending, bool descending)
        {
            anscendingButton.Enabled = ascending;
            descendingButton.Enabled = descending;
        }

        private void ShowSortTypeButtons(bool alphabet, bool lettercount)
        {
            поАлфавиту.Enabled = alphabet;
            поКоличествуБукв.Enabled = lettercount;
        }

        private void ShowMask(bool flag)
        {
            searchMask.ReadOnly = !flag;
            searchButton.Enabled = flag;
            deleteMaskButton.Enabled = flag;
        }

        private void открытьСловарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDictionary();

            sortDirection = DictionaryWordComparator.SortDirection.Ascending;
            sortBy = DictionaryWordComparator.SortBy.Alphabet;

            ShowDirectionButtons(false, true);
            ShowSortTypeButtons(false, true);
            ShowMask(true);
        }

        private void ShowProgress()
        {
            progressBar.Visible = true;
        }

        private void HideProgress()
        {
            progressBar.Visible = false;
        }

        private void anscendingButton_Click(object sender, EventArgs e)
        {
            sortDirection = DictionaryWordComparator.SortDirection.Ascending;

            dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
            UpdateUi();

            ShowDirectionButtons(false, true);
        }

        private void descendingButton_Click(object sender, EventArgs e)
        {
            sortDirection = DictionaryWordComparator.SortDirection.Descending;

            dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
            UpdateUi();

            ShowDirectionButtons(true, false);
        }

        private void поАлфавиту_Click(object sender, EventArgs e)
        {
            sortBy = DictionaryWordComparator.SortBy.Alphabet;

            dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
            UpdateUi();

            ShowSortTypeButtons(false, true);
        }

        private void поКоличествуБукв_Click(object sender, EventArgs e)
        {
            sortBy = DictionaryWordComparator.SortBy.LetterCount;

            dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
            UpdateUi();

            ShowSortTypeButtons(true, false);
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((keyChar < 'а' || keyChar > 'я') && keyChar != '\b' && keyChar != '*')
            {
                e.Handled = true;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            int letterCount = searchMask.Text.Length;
            string mask = searchMask.Text;

            if (letterCount > 0)
            {
                int wordCount = 0;
                dictionary.Sort(new DictionaryWordComparator(DictionaryWordComparator.SortDirection.Ascending, DictionaryWordComparator.SortBy.LetterCount));

                var items = listView1.Items;
                items.Clear();
                foreach (DictionaryWord t in dictionary.DictionaryWords)
                {
                    if (t.Word.Length > letterCount)
                    {
                        break;
                    }
                    else if (t.Word.Length == letterCount)
                    {
                        bool flag = true;
                        for (int i = 0; i < letterCount; i++)
                        {
                            if (!((mask[i] == '*') || (mask[i] == t.Word.ToLower()[i])))
                            {
                                flag = false;
                                break;
                            }
                        }

                        if (flag == true)
                        {
                            items.Add(t.ToString());
                            listView1.Update();
                            wordCount++;
                        }
                    }
                }

                if (wordCount == 0)
                {
                    items.Add("По данной маске ничего не найдено");
                    listView1.Update();
                }

                ShowDirectionButtons(false, false);
                ShowSortTypeButtons(false, false);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (searchMask.Text.Length > 0)
            {
                sortDirection = DictionaryWordComparator.SortDirection.Ascending;
                sortBy = DictionaryWordComparator.SortBy.Alphabet;

                ShowDirectionButtons(false, true);
                ShowSortTypeButtons(false, true);

                searchMask.Text = "";

                dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));

                UpdateUi();
            }
        }

        private void dictionaryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = listView1.SelectedIndex;
            selectedWord = dictionary.DictionaryWords[indexSelected].Word;
        }

        private void deleteWordButton_Click(object sender, EventArgs e)
        {
            if (selectedWord != null)
            {
                if (MessageBox.Show(
                    "Вы уверены, что хотите удалить понятие " + selectedWord + " из словаря?",
                    "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DictionaryWord[] NewDictionaryWords = new DictionaryWord[dictionary.DictionaryWords.Length - 1];

                    int j = 0;

                    for (int i = 0; i < dictionary.DictionaryWords.Length; i++)
                    {
                        if (dictionary.DictionaryWords[i].Word != selectedWord)
                        {
                            NewDictionaryWords[j] = dictionary.DictionaryWords[i];
                            j++;
                        }
                    }

                    dictionary.DictionaryWords = NewDictionaryWords;
                    UpdateUi();

                    selectedWord = null;
                }
            }
            else
            {
                MessageBox.Show(
                    "Вы не выбрали понятие",
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void updateWordButton_Click(object sender, EventArgs e)
        {

        }

        private void newWordButton_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
