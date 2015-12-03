using System;
using System.Windows.Forms;
using Dictionary;

namespace CrosswordApplication
{
    public partial class DictionaryManagerForm : Form
    {
        private string fileName;
        private Dictionary.Dictionary dictionary;

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
            var items = dictionaryListBox.Items;
            for (var i = 0; i < dictionary.dictionaryFields.Length; i++)
            {
                items.Add(dictionary.dictionaryFields[i].field);
                if (i%200 == 0)
                {
                    dictionaryListBox.Update();
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
        }

        private void открытьСловарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDictionary();
        }

        private void ShowProgress()
        {
            progressBar.Visible = true;
        }

        private void HideProgress()
        {
            progressBar.Visible = false;
        }
    }
}
