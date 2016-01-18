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
    public partial class CrosswordGenerationParametersForm : Form
    {
        public enum Type
        {
            WithDictionary,
            NoDictionary
        }

        private Type _type;
        private global::Crossword.Crossword _crossword;
        private readonly Dictionary.Dictionary _dictionary = new Dictionary.Dictionary();

        public CrosswordGenerationParametersForm(Type type, global::Crossword.Crossword crossword)
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;

            _crossword = crossword;
            _type = type;
            if (type == Type.NoDictionary)
            {
                dictionaryFilePathTextBox.Visible = false;
                label1.Visible = false;
                generateButton.Text = "Сохранить";
                this.Text = "Параметры";
            }
        }



        private void generateButton_Click(object sender, EventArgs e)
        {
            if (_type == Type.WithDictionary && !_dictionary.IsLoaded())
            {
                MessageBox.Show("Выберите словарь", "Ошибка");
                return;
            }
            int newWidth = (int) widthNumericUpDown.Value;
            int newHeight = (int) heightNumericUpDown.Value;
            if (!_crossword.CanChangeBorders(newWidth, newHeight))
            {
                MessageBox.Show("Некоторые слова не поместятся в новые границы. Увеличьте размер или удалите несколько слов", "Ошибка");
                return;
            }
            _crossword.SetSize(newWidth, newHeight);
            if (_type == Type.WithDictionary)
            {
                _crossword.Generate(_dictionary);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void openDictionaryFileButton_Click(object sender, EventArgs e)
        {
            _dictionary.Load(res =>
            {
                if (res)
                {
                    dictionaryFilePathTextBox.Text = _dictionary.GetFilename();
                }
                else
                {
                    MessageBox.Show("Ошибка при загрузке словаря", "Ошибка");
                }
            });
        }
    }
}

   