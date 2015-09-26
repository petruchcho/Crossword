using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    class Dictionary
    {
        // TODO Make private
        public DictionaryField[] dictionaryFields;

        // Default constructor - creates empty dictionary
        public Dictionary()
        {
            dictionaryFields = new DictionaryField[0];
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
        public void Sort(IComparer<DictionaryField> comparer)
        {
            Array.Sort<DictionaryField>(dictionaryFields, comparer);
        }

        // Opens a new form with this dictionary
        public void Show()
        {
            // TODO
            throw new NotImplementedException();
        }

        /*
         * This method opens a dialog to select a file and then 
         * starts async loading.
         */
        public void Load(Action<bool> callback)
        {
            // TODO Get fileName with dialog
            string fileName = DictionaryManager.DEFAULT_FILE_NAME;

            new CommonUtils.AsyncTask<bool>(
                () =>
                {
                    try
                    {
                        string[] dictionaryLines = System.IO.File.ReadAllLines(fileName, Encoding.GetEncoding(1251));
                        int dictionarySize = dictionaryLines.Length;
                        dictionaryFields = new DictionaryField[dictionarySize];
                        for (int i = 0; i < dictionarySize; i++)
                        {
                            dictionaryFields[i] = new DictionaryField(dictionaryLines[i]);
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
    }
}
