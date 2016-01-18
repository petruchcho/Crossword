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

                int margin = 80;

                generateButton.Location = new Point(generateButton.Location.X, generateButton.Location.Y - margin);
                heightNumericUpDown.Location = new Point(heightNumericUpDown.Location.X, heightNumericUpDown.Location.Y - margin);
                widthNumericUpDown.Location = new Point(widthNumericUpDown.Location.X, widthNumericUpDown.Location.Y - margin);
                label2.Location = new Point(label2.Location.X, label2.Location.Y - margin);
                heightLabel.Location = new Point(heightLabel.Location.X, heightLabel.Location.Y - margin);
                widthLabel.Location = new Point(widthLabel.Location.X, widthLabel.Location.Y - margin);
                this.Height -= margin;
            }
            if (type == Type.Params)
            {
                generateButton.Text = "Сохранить";
                this.Text = "Параметры";
            }

            if (crossword != null)
            {
                heightNumericUpDown.Value = crossword.Height;
                widthNumericUpDown.Value = crossword.Width;
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

   