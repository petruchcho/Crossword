using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrosswordApplication.Dictionary;

namespace CrosswordApplication.Forms
{
    public partial class CreateOrUpdateDictionaryWordForm : Form
    {
        private string selectedDascription;
        private string selectedWord;
        private Dictionary.Dictionary dictionary;

        private State state;

        private enum State
        {
            Normal,
            Error
        }

        public CreateOrUpdateDictionaryWordForm(Dictionary.Dictionary dict)
        {
            state = State.Normal;
            this.selectedWord = "";
            this.selectedDascription = "";
            InitializeComponent();
            this.Text = "Введите новое понятие";
            dictionary = dict;
        }

        public CreateOrUpdateDictionaryWordForm(string selectedWord, string selectedDascription, Dictionary.Dictionary dict)
        {
            state = State.Normal;

            this.selectedWord = selectedWord.ToLower();
            this.selectedDascription = selectedDascription;

            InitializeComponent();
            this.Text = "Отредактируйте текущее понятие";

            wordTextBox.Text = this.selectedWord;
            descriptionTextBox.Text = this.selectedDascription;
            dictionary = dict;
        }

        public Dictionary.Dictionary getDictionary()
        {
            return dictionary;
        }

        private void wordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((keyChar < 'а' || keyChar > 'я') && keyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void descriptionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((keyChar < 'А' || keyChar > 'я') && keyChar != '\b' && keyChar != ' ' && keyChar != '.' && keyChar != ',' && keyChar != '.' && keyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int wordSize = wordTextBox.Text.Length;
            int descriptionSize = descriptionTextBox.Text.Length;
            string newWord = wordTextBox.Text.ToUpper();
            string newDescription = descriptionTextBox.Text;
            if ((wordSize > 15) || (wordSize < 3) || (descriptionSize < 3))
            {
                MessageBox.Show(
                    "Вы неверно заполнили поля с понятием и определением",
                    "Ошибка", MessageBoxButtons.OK);
                DialogResult = DialogResult.None;
            }
            else
            {
                int position = -1;
                for (int i = 0; i < dictionary.DictionaryWords.Length; i++)
                {
                    string word = dictionary.DictionaryWords[i].Word;
                    if (word != selectedWord.ToUpper())
                    {
                        if (word == newWord)
                        {
                            state = State.Error;
                            MessageBox.Show(
                            "Понятие дублирует уже существующее",
                            "Ошибка", MessageBoxButtons.OK);
                            DialogResult = DialogResult.None;
                            return;
                        }
                    }
                    else
                    {
                        position = i;
                    }
                }

                if (state == State.Normal)
                {
                    if (position > -1)
                    {
                        dictionary.DictionaryWords[position].Word = newWord;
                        dictionary.DictionaryWords[position].Description = newDescription;
                    }
                    else
                    {
                        DictionaryWord[] newDictionaryWords = new DictionaryWord[dictionary.DictionaryWords.Length + 1];
                        for (int i = 0; i < newDictionaryWords.Length - 1; i++)
                        {
                            newDictionaryWords[i] = dictionary.DictionaryWords[i];
                        }
                        newDictionaryWords[newDictionaryWords.Length - 1] = new DictionaryWord(newWord + " " + newDescription);
                        dictionary.DictionaryWords = newDictionaryWords;
                    }
                }
            }

            state = State.Normal;
        }
    }

}
