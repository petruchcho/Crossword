using System;

namespace CrosswordApplication.Dictionary
{
    public class DictionaryWord
    {
        private string _word;
        private string _description;

        // Probably we should check it with other mask
        public DictionaryWord(string dictionaryString)
        {
            Word = dictionaryString.Substring(0, dictionaryString.IndexOf(" ", StringComparison.Ordinal));
            Description = dictionaryString.Substring(dictionaryString.IndexOf(" ", StringComparison.Ordinal) + 1);
        }

        public DictionaryWord(string word, string description)
        {
            _word = word; 
            _description = description;
        }

        public string Word
        {
            get { return _word; }
            set { _word = value; Update(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; Update(); }
        }

        public void Update()
        {
            
        }

        public override string ToString()
        {
            return Word + " " + Description;
        }
    }
}
