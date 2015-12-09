using System.Text;
using CrosswordApplication.Dictionary;

namespace CrosswordApplication.Crossword
{
    public class CrosswordWord
    {
        private readonly global::Crossword.Crossword crossword;
        private readonly DictionaryWord dictionaryWord;
        private CrosswordWordPosition position;
        private bool isResolved;

        public CrosswordWord(global::Crossword.Crossword crossword, DictionaryWord dictionaryWord, CrosswordWordPosition position) : this(crossword, dictionaryWord, position, false)
        {
        }

        public CrosswordWord(global::Crossword.Crossword crossword, DictionaryWord dictionaryWord, CrosswordWordPosition position, bool isResolved)
        {
            this.dictionaryWord = dictionaryWord;
            this.position = position;
            IsResolved = isResolved;
        }

        public CrosswordWordPosition Position
        {
            get { return position; }
            set { position = value; Update(); }
        }

        public bool IsResolved { 
            get { return isResolved; } 
            set { isResolved = value; Update(); } }

        public string Word
        {
            get { return dictionaryWord.Word; }
        }

        public string Description
        {
            get { return dictionaryWord.Description; }
        }

        public void Update()
        {
            if (crossword != null)
            {
                crossword.UpdateState();
            }
        }
    }
}
