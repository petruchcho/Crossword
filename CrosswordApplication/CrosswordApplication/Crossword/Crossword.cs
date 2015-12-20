using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CrosswordApplication.Crossword;
using CrosswordApplication.Dictionary;

namespace Crossword
{
    public class Crossword
    {
        public class CrosswordStateListener
        {
            private readonly Action<CrosswordWord> _wordAddAction;
            private readonly Action _sizeChangedAction;

            public CrosswordStateListener(Action<CrosswordWord> wordAddAction, Action sizeChangedAction)
            {
                this._wordAddAction = wordAddAction;
                this._sizeChangedAction = sizeChangedAction;
            }

            public void OnWordAdded(CrosswordWord crosswordWord)
            {
                _wordAddAction.Invoke(crosswordWord);
            }

            public void OnSizeChanged()
            {
                _sizeChangedAction.Invoke();
            }
        }

        public static readonly string DefaultFileName = @"..\..\debug crossword.cwd";

        public static readonly int DefaultCrosswordWidth = 20;
        public static readonly int DefaultCrosswordHeight = 20;

        private int width;
        private int height;

        // TODO Replace with unmodifiable list
        private readonly List<CrosswordWord> crosswordWords;

        private readonly CrosswordSerializer serializer = new CrosswordSerializer();

        private CrosswordStateListener _crosswordStateListener;

        public Crossword()
        {
            width = DefaultCrosswordWidth;
            height = DefaultCrosswordHeight;
            crosswordWords = new List<CrosswordWord>();
        }

        public void Load(Action<bool> callback)
        {
            // TODO Get fileName with dialog
            string fileName = DefaultFileName;

            new CommonUtils.AsyncTask<bool>(
                () =>
                {
                    try
                    {
                        var crosswordStrings = System.IO.File.ReadAllLines(fileName, Encoding.GetEncoding(1251));
                        serializer.DeserializeCrossword(this, crosswordStrings);
                        return true;
                    }
                    catch (Exception e)
                    {
                        // TODO Proceed exception
                    }
                    return false;
                }, callback).Start();

        }

        public bool IsLoaded()
        {
            return crosswordWords != null && crosswordWords.Count > 0;
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            if (_crosswordStateListener != null)
            {
                _crosswordStateListener.OnSizeChanged();
            }
        }

        public void AddWord(CrosswordWord crosswordWord)
        {
            if (typeof(PreviewCrosswordWord) == crosswordWord.GetType())
            {
                throw new InvalidOperationException("You should not put PreviewWord here");
            }

            crosswordWords.Add(crosswordWord);
            if (_crosswordStateListener != null)
            {
                _crosswordStateListener.OnWordAdded(crosswordWord);
            }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public List<CrosswordWord> CrosswordWords
        {
            get { return crosswordWords; }
        }

        public List<CrosswordWord> FindWordsAtPosition(int x, int y)
        {
            List<CrosswordWord> foundedWords = new List<CrosswordWord>();
            foreach (var crosswordWord in CrosswordWords)
            {

                for (int i = 0; i < crosswordWord.Word.Length; i++)
                {
                    int curX, curY;
                    crosswordWord.PositionAtIndex(i, out curX, out curY);

                    if (curX == x && curY == y)
                    {
                        foundedWords.Add(crosswordWord);
                        break;
                    }
                }
            }

            return foundedWords;
        }

        public void SetCrosswordStateListener(CrosswordStateListener crosswordStateListener)
        {
            _crosswordStateListener = crosswordStateListener;
        }

        public List<PreviewCrosswordWord> GetPreviewsPositions(DictionaryWord dictionaryWord)
        {
            if (crosswordWords == null || crosswordWords.Count == 0)
            {
                return null;
            }

            var result = new List<PreviewCrosswordWord>();

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    var previewWord = new PreviewCrosswordWord(dictionaryWord,
                        new CrosswordWordPosition(i, j, Orientation.Vertical));

                    if (CheckIfPreviewWordMatches(previewWord))
                    {
                        result.Add(previewWord);
                    }

                    previewWord = new PreviewCrosswordWord(dictionaryWord,
                        new CrosswordWordPosition(i, j, Orientation.Horizontal));
                    if (CheckIfPreviewWordMatches(previewWord))
                    {
                        result.Add(previewWord);
                    }
                }
            }

            return result;
        }

        public List<PreviewCrosswordWord> GetHighlights(DictionaryWord dictionaryWord, int x, int y)
        {
            var result = new List<PreviewCrosswordWord>();

            var previewWords = GetPreviewsPositions(dictionaryWord);
            if (previewWords == null)
            {
                var horizontalHighlight = new PreviewCrosswordWord(dictionaryWord,
                    new CrosswordWordPosition(x, y, Orientation.Horizontal));
                var verticalHighlight = new PreviewCrosswordWord(dictionaryWord,
                    new CrosswordWordPosition(x, y, Orientation.Vertical));

                if (IsWordInBorders(horizontalHighlight))
                {
                    result.Add(horizontalHighlight);
                }

                if (IsWordInBorders(verticalHighlight))
                {
                    result.Add(verticalHighlight);
                }

                return result;
            }


            foreach (var previewCrosswordWord in previewWords)
            {
                if (x == previewCrosswordWord.Position.X && y == previewCrosswordWord.Position.Y)
                {
                    result.Add(new PreviewCrosswordWord(dictionaryWord,
                        new CrosswordWordPosition(x, y, previewCrosswordWord.Position.Orientation)));
                }
            }

            return result;
        }

        public PreviewCrosswordWord GetBestHighlight(DictionaryWord dictionaryWord, int x, int y,
            Orientation preferedOrientation)
        {
            var highlights = GetHighlights(dictionaryWord, x, y);
            if (highlights.Count == 0)
            {
                return null;
            }

            foreach (var previewCrosswordWord in highlights)
            {
                if (previewCrosswordWord.Position.Orientation == preferedOrientation)
                {
                    return previewCrosswordWord;
                }
            }

            return crosswordWords.Count > 0 ? highlights[0] : null;
        }

        public CrosswordWord GetCrosswordWordForWord(string word)
        {
            foreach (var crosswordWord in CrosswordWords)
            {
                if (word.Equals(crosswordWord.Word))
                {
                    return crosswordWord;
                }
            }
            return null;
        }

        private bool CheckIfPreviewWordMatches(PreviewCrosswordWord previewWord)
        {
            var countOfIntersections = 0;
            var goodWord = true;

            if (!IsWordInBorders(previewWord))
            {
                return false;
            }


            foreach (var crosswordWord in CrosswordWords)
            {
                var intersectionType = crosswordWord.GetIntersectionType(previewWord);
                if (intersectionType == CrosswordWord.IntersectionType.WrongIntersection)
                {
                    goodWord = false;
                    break;
                }
                if (intersectionType == CrosswordWord.IntersectionType.CorrectIntersection)
                {
                    countOfIntersections++;
                }
            }
            if (countOfIntersections == 0)
            {
                goodWord = false;
            }
            return goodWord;
        }

        private bool IsWordInBorders(CrosswordWord word)
        {
            int x;
            int y;
            word.PositionAtIndex(word.Word.Length - 1, out x, out y);

            return x < Width && y < Height;
        }

        public override string ToString()
        {
            return serializer.SerializeCrossword(this);
        }
    }

    class CrosswordSerializer
    {
        internal void DeserializeCrossword(Crossword crossword, string[] lines)
        {
            var width = int.Parse(lines[0]);
            var height = int.Parse(lines[1]);

            var wordsCount = int.Parse(lines[2]);

            for (var i = 0; i < wordsCount; i++)
            {
                var curIndex = 3 + i*6;
                var word = lines[curIndex];
                var description = lines[curIndex + 1];
                var x = int.Parse(lines[curIndex + 2]);
                var y = int.Parse(lines[curIndex + 3]);
                Orientation orientation;
                if (!Enum.TryParse(lines[curIndex + 4], true, out orientation))
                {
                    throw new Exception("Orientation deserialization fail");
                }
                var isResolved = bool.Parse(lines[curIndex + 5]);
                
                crossword.CrosswordWords.Add(new CrosswordWord(crossword, new DictionaryWord(word, description), 
                    new CrosswordWordPosition(x, y, orientation), isResolved));
            }
            crossword.SetSize(width, height);
        }

        internal string SerializeCrossword(Crossword crossword)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(crossword.Width.ToString());
            stringBuilder.AppendLine(crossword.Height.ToString());
            stringBuilder.AppendLine(crossword.CrosswordWords.Count.ToString());
            foreach (var crosswordWord in crossword.CrosswordWords)
            {
                stringBuilder.AppendLine(crosswordWord.Word.ToString());
                stringBuilder.AppendLine(crosswordWord.Description.ToString());
                stringBuilder.AppendLine(crosswordWord.Position.X.ToString());
                stringBuilder.AppendLine(crosswordWord.Position.Y.ToString());
                stringBuilder.AppendLine(crosswordWord.Position.Orientation.ToString());
                stringBuilder.AppendLine(crosswordWord.IsResolved.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
