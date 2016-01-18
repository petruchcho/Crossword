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
            GenerateWithDictionary,
            GenerateWithoutDictionary,
            Params
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
            if (type != Type.GenerateWithDictionary)
            {
                dictionaryFilePathTextBox.Visible = false;
                openDictionaryFileButton.Visible = false;
                label1.Visible = false;
            }
            if (type == Type.Params)
            {
                generateButton.Text = "Сохранить";
                this.Text = "Параметры";
            }

            _dictionary = new Dictionary.Dictionary();
        }

        public CrosswordGenerationParametersForm(Type type, global::Crossword.Crossword crossword,
            Dictionary.Dictionary dictionary) : this(type, crossword)
        {
            _dictionary = dictionary;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (_type == Type.GenerateWithDictionary && !_dictionary.IsLoaded())
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
            if (_type == Type.Params)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
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

   