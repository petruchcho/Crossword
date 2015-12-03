using CrosswordApplication.Dictionary;

namespace CrosswordApplication.Crossword
{
    class CrosswordWord
    {
        private readonly DictionaryWord dictionaryWord;
        private CrosswordWordPosition position;

        public CrosswordWord(DictionaryWord dictionaryWord, CrosswordWordPosition position) : this(dictionaryWord, position, false)
        {
        }

        public CrosswordWord(DictionaryWord dictionaryWord, CrosswordWordPosition position, bool isResolved)
        {
            this.dictionaryWord = dictionaryWord;
            this.position = position;
            IsResolved = isResolved;
        }

        public CrosswordWordPosition Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsResolved { get; set; }

        public string Word()
        {
            return dictionaryWord.Word;
        }

        public string Description()
        {
            return dictionaryWord.Description;
        }
    }
}
