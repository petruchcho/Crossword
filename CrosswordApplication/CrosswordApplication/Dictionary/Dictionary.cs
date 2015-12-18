using System;
using System.Collections.Generic;
using System.Text;

namespace CrosswordApplication.Dictionary
{
    class Dictionary
    {
        public static readonly string DefaultFileName = @"..\..\Главный.dict";

        // TODO Make private
        public DictionaryWord[] DictionaryWords;

        // Default constructor - creates empty dictionary
        public Dictionary()
        {
            DictionaryWords = new DictionaryWord[0];
        }

        /* 
         * Returns an iterator that contains only fields that 
         * match mask 
        */
        public DictionaryIterator GetIterator(DictionaryMask mask)
        {
            // TODO
            throw new NotImplementedException();
        }

        // TODO Maybe we should create special classes for comparers
        // e.g. LengthComparer, LexicalComparer
        public void Sort(IComparer<DictionaryWord> comparer)
        {
            Array.Sort<DictionaryWord>(DictionaryWords, comparer);
        }

        /*
         * This method opens a dialog to select a file and then 
         * starts async loading.
         */
        public void Load(Action<bool> callback)
        {
            //new OpenFileDialog().ShowDialog();

            // TODO Get fileName with dialog
            string fileName = DefaultFileName;

            new global::CommonUtils.AsyncTask<bool>(
                () =>
                {
                    try
                    {
                        var dictionaryLines = System.IO.File.ReadAllLines(fileName, Encoding.GetEncoding(1251));
                        var dictionarySize = dictionaryLines.Length;
                        DictionaryWords = new DictionaryWord[dictionarySize];
                        for (var i = 0; i < dictionarySize; i++)
                        {
                            DictionaryWords[i] = new DictionaryWord(dictionaryLines[i]);
                        }
                        // TODO Sort it with some comparer
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
         * This method opens a dialog to select a file and then 
         * starts async loading.
         */
        public void Save(Action<bool> callback)
        {
            // TODO
            throw new NotImplementedException();
        }

        /*
         * Shows if disctionary is loaded
         */
        public bool IsLoaded()
        {
            return DictionaryWords != null && DictionaryWords.Length > 0;
        }
    }
}
