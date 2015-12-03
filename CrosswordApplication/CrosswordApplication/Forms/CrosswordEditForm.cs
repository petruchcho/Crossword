using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            loadDictionary();
        }

        private void loadDictionary()
        {
            // TODO Save Existed
            if (dictionary != null)
            {
                dictionary.Save((res) => { });
            }
            else
            {
                dictionary = new Dictionary.Dictionary();
                dictionary.Load((res) =>
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
            for (int i = 0; i < Math.Min(100, dictionary.dictionaryFields.Length); i++)
            {
                items.Add(dictionary.dictionaryFields[i].field);
            }

            dictionaryListBox.Visible = true;
        }
    }
}
