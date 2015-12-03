using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public DictionaryManagerForm(string fileName) : base()
        {
            this.fileName = fileName;
        }

        private void loadDictionary(string fileName)
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
            var items = dictionaryListView.Items;
            // TODO Remove 100
            for (int i = 0; i < Math.Min(100, dictionary.dictionaryFields.Length); i++)
            {
                items.Add(dictionary.dictionaryFields[i].field);
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
                loadDictionary(fileName);
            }
        }

        private void открытьСловарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadDictionary(null);
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
