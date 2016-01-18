using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CrosswordApplication.Dictionary
{
    public class Dictionary
    {
        public static readonly string DefaultFileName = @"..\..\Главный.dict";

        private readonly static Random random = new Random();

        // TODO Make private
        public DictionaryWord[] DictionaryWords;

        // Default constructor - creates empty dictionary
        public Dictionary()
        {
            DictionaryWords = new DictionaryWord[0];
        }

        private string filename;

        /* 
         * Returns an iterator that contains only fields that 
         * match mask 
        */
        public DictionaryIterator GetIterator(DictionaryMask mask)
        {
            // TODO
            return new DictionaryIterator(mask, DictionaryWords);
        }

        // TODO Maybe we should create special classes for comparers
        // e.g. LengthComparer, LexicalComparer
        public void Sort(DictionaryWordComparator comparer)
        {
            Array.Sort(DictionaryWords, comparer);
        }

        /*
         * This method opens a dialog to select a file and then 
         * starts async loading.
         */
        public void Load(Action<bool> callback)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Dictionary files (*.dict)|*.dict";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            
            new global::CommonUtils.AsyncTask<bool>(
                () =>
                {
                    try
                    {
                        var dictionaryLines = System.IO.File.ReadAllLines(openFileDialog.FileName, Encoding.GetEncoding(1251));
                        var dictionarySize = dictionaryLines.Length;
                        DictionaryWords = new DictionaryWord[dictionarySize];
                        for (var i = 0; i < dictionarySize; i++)
                        {
                            DictionaryWords[i] = new DictionaryWord(dictionaryLines[i]);
                        }
                        // TODO Sort it with some comparer
                        Sort(new DictionaryWordComparator(DictionaryWordComparator.SortDirection.Ascending, DictionaryWordComparator.SortBy.Alphabet));
                        filename = openFileDialog.FileName;
                        return true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(
                    "Неверный формат файла",
                    "Ошибка", MessageBoxButtons.OK);
                    }
                    return false;
                }, callback).Start();
        }

        /*
         * This method opens a dialog to select a file and then 
         * starts async loading.
         */
        public void Save(Action<bool> callback)
        {
            var saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Dictionary files (*.dict)|*.dict";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            if (!saveFileDialog1.FileName.EndsWith(saveFileDialog1.DefaultExt))
            {
                MessageBox.Show("Некорректное расширение");
                return;
            }

            new global::CommonUtils.AsyncTask<bool>(
               () =>
               {
                   try
                   {
                       Stream myStream;
                       if ((myStream = saveFileDialog1.OpenFile()) != null)
                       {
                           var streamWriter = new StreamWriter(myStream, Encoding.GetEncoding(1251));

                           var sb = new StringBuilder();
                           for (int i = 0; i < this.DictionaryWords.Length; i++)
                           {
                               sb.AppendLine(this.DictionaryWords[i].Word + ' ' + this.DictionaryWords[i].Description);
                           }

                           streamWriter.Write(sb.ToString());
                           streamWriter.Close();
                       }
                       return true;
                   }
                   catch (Exception e)
                   {
                       // TODO Proceed exception
                   }
                   return false;
               }, callback).Start();
        }

        /*
         * Shows if disctionary is loaded
         */
        public bool IsLoaded()
        {
            return DictionaryWords != null && DictionaryWords.Length > 0;
        }

        public DictionaryWord GetRandomDictionaryWord()
        {
            if (DictionaryWords == null || DictionaryWords.Length == 0)
            {
                return null;
            }

            DictionaryWord dictionaryWord = DictionaryWords[random.Next(0, DictionaryWords.Length)];

            return dictionaryWord;
        }

        public string GetFilename()
        {
            return filename;
        }
    }
}
