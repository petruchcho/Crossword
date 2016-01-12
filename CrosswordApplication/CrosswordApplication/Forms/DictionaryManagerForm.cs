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
        private string selectedDascription;

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
                if (dictionary.DictionaryWords.Length > 0)
                {
                    dictionary.Save((res) => { });
                }
            }

            ShowProgress();
            dictionary = new Dictionary.Dictionary();

            dictionary.Load((res) =>
            {
                HideProgress();
                try
                {
                    UpdateUi();
                }
                catch (Exception e)
                {
                    return;
                }
            });
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
            var items = dictionaryListBox.Items;
            items.Clear();
            for (var i = 0; i < dictionary.DictionaryWords.Length; i++)
            {
                items.Add(dictionary.DictionaryWords[i].ToString());
                if (i % 200 == 0)
                {
                    dictionaryListBox.Update();
                }
            }
        }

        private void ShowEmptyState()
        {
            var items = dictionaryListBox.Items;
            items.Clear();
            dictionaryListBox.Update();
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
            ShowButtonsForWord(false);
        }

        private void ShowDirectionButtons(bool ascending, bool descending)
        {
            anscendingButton.Enabled = ascending;
            descendingButton.Enabled = descending;
        }

        private void ShowButtonsForWord(bool flag)
        {
            newWordButton.Enabled = flag;
            updateWordButton.Enabled = flag;
            deleteWordButton.Enabled = flag;
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

            if (dictionary.DictionaryWords.Length > 0)
            {
                ShowDirectionButtons(false, true);
                ShowSortTypeButtons(false, true);
                ShowMask(true);
                ShowButtonsForWord(true);
            }
        }

        private void ShowProgress()
        {
            //progressBar.Visible = true;
        }

        private void HideProgress()
        {
            //progressBar.Visible = false;
        }

        private void anscendingButton_Click(object sender, EventArgs e)
        {
            selectedWord = null;
            sortDirection = DictionaryWordComparator.SortDirection.Ascending;

            dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
            UpdateUi();

            ShowDirectionButtons(false, true);
        }

        private void descendingButton_Click(object sender, EventArgs e)
        {
            selectedWord = null;
            sortDirection = DictionaryWordComparator.SortDirection.Descending;

            dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
            UpdateUi();

            ShowDirectionButtons(true, false);
        }

        private void поАлфавиту_Click(object sender, EventArgs e)
        {
            selectedWord = null;

            sortBy = DictionaryWordComparator.SortBy.Alphabet;

            dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
            UpdateUi();

            ShowSortTypeButtons(false, true);
        }

        private void поКоличествуБукв_Click(object sender, EventArgs e)
        {
            selectedWord = null;
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
            selectedWord = null;
            int letterCount = searchMask.Text.Length;
            string mask = searchMask.Text;

            if (letterCount > 0)
            {
                int wordCount = 0;
                dictionary.Sort(new DictionaryWordComparator(DictionaryWordComparator.SortDirection.Ascending, DictionaryWordComparator.SortBy.LetterCount));

                var items = dictionaryListBox.Items;
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
                            dictionaryListBox.Update();
                            wordCount++;
                        }
                    }
                }

                if (wordCount == 0)
                {
                    items.Add("По данной маске ничего не найдено");
                    dictionaryListBox.Update();
                }

                ShowDirectionButtons(false, false);
                ShowSortTypeButtons(false, false);
            }
        }

        private void deleteMaskButton_Click(object sender, EventArgs e)
        {
            selectedWord = null;
            if (searchMask.Text.Length > 0)
            {
                searchMask.Text = "";
                sortDirection = DictionaryWordComparator.SortDirection.Ascending;
                sortBy = DictionaryWordComparator.SortBy.Alphabet;

                ShowDirectionButtons(false, true);
                ShowSortTypeButtons(false, true);

                dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));

                UpdateUi();
            }
        }

        private void dictionaryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = dictionaryListBox.SelectedIndex;
            if (dictionaryListBox.SelectedItem.ToString() != "По данной маске ничего не найдено")
            {
                selectedWord = dictionary.DictionaryWords[indexSelected].Word;
                selectedDascription = dictionary.DictionaryWords[indexSelected].Description;
            }
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
            if (selectedWord != null)
            {
                CreateOrUpdateDictionaryWordForm createOrUpdateDictionaryWordForm = new CreateOrUpdateDictionaryWordForm(selectedWord, selectedDascription, dictionary);
                if (createOrUpdateDictionaryWordForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateUi();
                }
            }
            else
            {
                MessageBox.Show(
                    "Вы не выбрали понятие",
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void newWordButton_Click(object sender, EventArgs e)
        {
            CreateOrUpdateDictionaryWordForm createOrUpdateDictionaryWordForm = new CreateOrUpdateDictionaryWordForm(dictionary);
            if (createOrUpdateDictionaryWordForm.ShowDialog() == DialogResult.OK)
            {
                dictionary.Sort(new DictionaryWordComparator(sortDirection, sortBy));
                UpdateUi();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dictionary != null)
            {
                if (dictionary.DictionaryWords.Length < 1)
                {
                    MessageBox.Show(
                    "Словарь пустой, его нельзя сохранить",
                    "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    dictionary.Save((res) => { });
                }
            }
            else
            {
                MessageBox.Show(
                    "Словарь пустой, его нельзя сохранить",
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dictionary = new Dictionary.Dictionary();
            dictionary.DictionaryWords = new DictionaryWord[0];
            ShowDirectionButtons(false, true);
            ShowSortTypeButtons(false, true);
            ShowMask(true);
            ShowButtonsForWord(true);
        }
    }
}
