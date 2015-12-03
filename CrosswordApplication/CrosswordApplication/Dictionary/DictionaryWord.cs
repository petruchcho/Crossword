using System;

namespace CrosswordApplication.Dictionary
{
    class DictionaryWord
    {
        // Probably we should check it with other mask
        public DictionaryWord(string dictionaryString)
        {
            Word = dictionaryString.Substring(0, dictionaryString.IndexOf(" ", StringComparison.Ordinal));
            Description = dictionaryString.Substring(dictionaryString.IndexOf(" ", StringComparison.Ordinal) + 1);
        }

        public string Word { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return Word + " " + Description;
        }
    }
}
