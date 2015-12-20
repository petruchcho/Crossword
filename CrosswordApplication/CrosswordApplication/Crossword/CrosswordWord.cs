using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CrosswordApplication.Crossword;
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

        public CrosswordWord(global::Crossword.Crossword crossword, PreviewCrosswordWord previewCrosswordWord) : this(crossword, previewCrosswordWord.dictionaryWord, previewCrosswordWord.position, previewCrosswordWord.isResolved)
        {
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
                
            }
        }

        public void PositionAtIndex(int index, out int x, out int y)
        {
            x = position.X;
            y = position.Y;
            CrosswordWordPosition.NextIndexPosition(position.Orientation, index, ref x, ref y);
        }

        public enum IntersectionType
        {
            WrongIntersection,
            CorrectIntersection,
            NoIntersection
        }

        public IntersectionType GetIntersectionType(CrosswordWord other)
        {
            if (other.Word.Equals(Word))
            {
                return IntersectionType.WrongIntersection;
            }

            for (int i = 0; i < Word.Length; i++)
            {
                for (int j = 0; j < other.Word.Length; j++)
                {
                    int x1, y1, x2, y2;
                    PositionAtIndex(i, out x1, out y1);
                    other.PositionAtIndex(j, out x2, out y2);

                    if (x1 == x2 && y1 == y2)
                    {
                        if (other.Position.Orientation == Position.Orientation)
                        {
                            return IntersectionType.WrongIntersection;
                        }
                        if (!Word[i].Equals(other.Word[j]))
                        {
                            return IntersectionType.WrongIntersection;
                        }
                        return IntersectionType.CorrectIntersection;
                    }
                }
            }

            return IntersectionType.NoIntersection;
        }

        public override string ToString()
        {
            return dictionaryWord.ToString();
        }
    }
}

public class PreviewCrosswordWord : CrosswordWord
{
    public PreviewCrosswordWord(DictionaryWord dictionaryWord, CrosswordWordPosition position) : base(null, dictionaryWord, position)
    {
    }

    public PreviewCrosswordWord(string word, CrosswordWordPosition position) : base(null, new DictionaryWord(word), position)
    {
    }
    
}