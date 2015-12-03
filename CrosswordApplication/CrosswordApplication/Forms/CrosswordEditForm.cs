using System;
using System.Windows.Forms;

namespace CrosswordApplication.Forms
{
    public partial class CrosswordEditForm : Form
    {
        private Dictionary.Dictionary dictionary;

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
                dictionary.Save((res) => { });
            }
            else
            {
                dictionary = new Dictionary.Dictionary();
                dictionary.Load(res =>
                {
                    SetDictionary();
                });
            }

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
    }
}
