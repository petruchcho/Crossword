using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using CrosswordApplication.Crossword;
using CrosswordApplication.Dictionary;
using Orientation = CrosswordApplication.Crossword.Orientation;

namespace Crossword
{
    public class Crossword
    {
        public class CrosswordStateListener
        {
            private readonly Action<CrosswordWord> _wordAddAction;
            private readonly Action _sizeChangedAction;
            private readonly Action<CrosswordWord> _wordDeletedAction;

            public CrosswordStateListener(Action<CrosswordWord> wordAddAction, Action sizeChangedAction, Action<CrosswordWord> wordDeletedAction)
            {
                this._wordAddAction = wordAddAction;
                this._sizeChangedAction = sizeChangedAction;
                this._wordDeletedAction = wordDeletedAction;
            }

            public void OnWordAdded(CrosswordWord crosswordWord)
            {
                if (_wordAddAction != null)
                {
                    _wordAddAction.Invoke(crosswordWord);
                }
            }

            public void OnSizeChanged()
            {
                if (_sizeChangedAction != null)
                {
                    _sizeChangedAction.Invoke();   
                }
            }

            public void OnWordDeleted(CrosswordWord crosswordWord)
            {
                if (_wordDeletedAction != null)
                {
                    _wordDeletedAction.Invoke(crosswordWord);
                }                
            }
        }

        public static readonly string DefaultFileName = @"..\..\debug crossword.cwd";

        public static readonly int DefaultCrosswordWidth = 20;
        public static readonly int DefaultCrosswordHeight = 20;

        private int width;
        private int height;

        // TODO Replace with unmodifiable list
        private readonly List<CrosswordWord> crosswordWords;

        private List<CrosswordLetter> progress;
        private int letterHelpers = 0;
        private int wordHelpers = 0;

        private readonly CrosswordSerializer serializer = new CrosswordSerializer();

        private readonly CrosswordGenerator generator = new CrosswordGenerator();


        private CrosswordStateListener _crosswordStateListener;

        public Crossword()
        {
            width = DefaultCrosswordWidth;
            height = DefaultCrosswordHeight;
            crosswordWords = new List<CrosswordWord>();
            progress = new List<CrosswordLetter>();
        }

        public void Load(UserRole userRole, Action<bool> callback)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = userRole == UserRole.Administrator ? 
                "Crossword files (*.cwd)|*.cwd" :
                "Crossword files (*.cwd, *.game)|*.cwd;*.game";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            new CommonUtils.AsyncTask<bool>(
                () =>
                {
                    try
                    {
                        var crosswordStrings = System.IO.File.ReadAllLines(openFileDialog.FileName, Encoding.GetEncoding(1251));
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

        public void Save(UserRole userRole, List<CrosswordLetter> progress, Action<bool> callback)
        {
            var saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = userRole == UserRole.Administrator ? 
                "Crossword files (*.cwd)|*.cwd" : 
                "Crossword game (*.game)|*.game";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = userRole == UserRole.Administrator ? "cwd" : "game";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            if (!saveFileDialog1.FileName.EndsWith(saveFileDialog1.DefaultExt))
            {
                MessageBox.Show("Некорректное расширение");
                return;
            }

            new CommonUtils.AsyncTask<bool>(
               () =>
               {
                   try
                   {
                       Stream myStream;
                       if ((myStream = saveFileDialog1.OpenFile()) != null)
                       {
                           var streamWriter = new StreamWriter(myStream, Encoding.GetEncoding(1251));
                           var serializedCrossword = userRole == UserRole.Administrator
                               ? serializer.SerializeCrossword(this)
                               : serializer.SerializeCrossword(this, progress);

                           streamWriter.WriteLine(serializedCrossword);
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
            UpdateHelpers();
        }

        public void DeleteWord(CrosswordWord crosswordWord)
        {
            CrosswordWord removedWord = null;
            for (int i = 0; i < crosswordWords.Count; i++)
            {
                if (crosswordWords[i].Word.Equals(crosswordWord.Word))
                {
                    removedWord = crosswordWords[i];
                    break;
                }
            }

            if (removedWord != null)
            {
                crosswordWords.Remove(removedWord);
            }
            if (_crosswordStateListener != null)
            {
                _crosswordStateListener.OnWordDeleted(removedWord);   
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

        public List<CrosswordLetter> Progress
        {
            get { return progress; }
        }

        public int LetterHelpers
        {
            get
            {
                return letterHelpers;
            }

            set { letterHelpers = value; }
        }

        public int WordHelpers
        {
            get
            {
                return wordHelpers;
            }

            set { wordHelpers = value; }
        }

        public void SetProgress(List<CrosswordLetter> progressList)
        {
            progress = new List<CrosswordLetter>();
            progress.AddRange(progressList);
        }

        public void AddLetterToProgress(int x, int y, string letter)
        {
            for (int i = 0; i < progress.Count; i++)
            {
                var curProgress = progress[i];
                if (curProgress.Position.X == x && curProgress.Position.Y == y)
                {
                    progress.RemoveAt(i);
                    break;
                }
            }
            progress.Add(new CrosswordLetter(letter, new CrosswordWordPosition(x, y, Orientation.Horizontal)));
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

        public CrosswordLetter GetLetterAtPosition(int x, int y)
        {
            foreach (var crosswordWord in CrosswordWords)
            {
                for (var i = 0; i < crosswordWord.Word.Length; i++)
                {
                    int xx;
                    int yy;
                    crosswordWord.PositionAtIndex(i, out xx, out yy);
                    if (xx == x && yy == y)
                    {
                        return new CrosswordLetter(crosswordWord.Word[i].ToString().ToUpper(), new CrosswordWordPosition(x, y, Orientation.Horizontal));
                    }
                }
            }
            return null;
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

        public CrosswordWord GetCrosswordWordForDescription(string description)
        {
            foreach (var crosswordWord in CrosswordWords)
            {
                if (description.Equals(crosswordWord.Description))
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

        public bool IsCorrectCrossword()
        {
            return !IsEmpty() && IsConnected();
        }

        private bool IsConnected()
        {
            if (IsEmpty())
            {
                return true;
            }
            bool[] visited = new bool[crosswordWords.Count];
            var q = new Queue<int>();
            q.Enqueue(0);
            visited[0] = true;

            while (q.Count > 0)
            {
                var curWord = crosswordWords[q.Dequeue()];
                for (var index = 0; index < crosswordWords.Count; index++)
                {
                    if (visited[index]) continue;
                    var intersectionType = curWord.GetIntersectionType(crosswordWords[index]);
                    switch (intersectionType)
                    {
                        case CrosswordWord.IntersectionType.WrongIntersection:
                            return false;
                        case CrosswordWord.IntersectionType.CorrectIntersection:
                            q.Enqueue(index);
                            visited[index] = true;
                            break;
                        case CrosswordWord.IntersectionType.NoIntersection:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return visited.Aggregate(true, (current, b) => current & b);
        }

        public bool IsEmpty()
        {
            return (crosswordWords == null || crosswordWords.Count == 0);
        }

        private bool IsWordInBorders(CrosswordWord word)
        {
            int x;
            int y;
            word.PositionAtIndex(word.Word.Length - 1, out x, out y);

            return x < Width && y < Height;
        }

        public double GetResult(List<CrosswordLetter> progress)
        {
            int countOfLetters = 0;
            int countOfCorrectLetters = 0;
            foreach (var crosswordWord in CrosswordWords)
            {
                int x, y;
                for (int i = 0; i < crosswordWord.Word.Length; i++)
                {
                    countOfLetters++;
                    crosswordWord.PositionAtIndex(i, out x, out y);
                    foreach (var crosswordLetter in progress)
                    {
                        if (crosswordLetter.Position.X == x && crosswordLetter.Position.Y == y)
                        {
                            if (crosswordLetter.Letter.ToUpper().Equals(crosswordWord.Word[i].ToString().ToUpper()))
                            {
                                countOfCorrectLetters++;
                            }
                            break;
                        }
                    }
                }
            }
            return 100.0*countOfCorrectLetters/countOfLetters;
        }

        public override string ToString()
        {
            return serializer.SerializeCrossword(this);
        }

        public void Generate(Dictionary dictionary)
        {
            int wordHaveBeenAdded = generator.Generate(this, dictionary);
            if (wordHaveBeenAdded == 0)
                MessageBox.Show("Новые слова не были добавлены!");
        }

        public bool CanChangeBorders(int newWidth, int newHeight)
        {
            var can = true;
            foreach (var crosswordWord in crosswordWords)
            {
                int x0, y0, x1, y1;
                crosswordWord.PositionAtIndex(0, out x0, out y0);
                crosswordWord.PositionAtIndex(crosswordWord.Word.Length - 1, out x1, out y1);
                can &= x0 < newWidth && x1 < newWidth && y0 < newHeight && y1 < newHeight;
            }
            return can;
        }

        public void UpdateHelpers()
        {
            int sumOfLetters = 0;
            foreach (CrosswordWord word in CrosswordWords)
            {
                sumOfLetters += word.Word.Length;
            }
            LetterHelpers = (int)(0.1 * sumOfLetters);
            WordHelpers = (int)(0.1 * crosswordWords.Count);
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
                crossword.CrosswordWords.Add(new CrosswordWord(crossword, new DictionaryWord(word, description), new CrosswordWordPosition(x, y, orientation), isResolved));
            }

            crossword.UpdateHelpers();

            try
            {
                var wordHelpers = int.Parse(lines[3 + wordsCount*6]);
                var letterHelpers = int.Parse(lines[4 + wordsCount * 6]);
                crossword.LetterHelpers = letterHelpers;
                crossword.WordHelpers = wordHelpers;
                var progressCount = int.Parse(lines[5 + wordsCount * 6]);
                for (var i = 0; i < progressCount; i++)
                {
                    var curIndex = 5 + wordsCount * 6 + 1 + i * 3;
                    var letter = lines[curIndex];
                    var x = int.Parse(lines[curIndex + 1]);
                    var y = int.Parse(lines[curIndex + 2]);
                    crossword.Progress.Add(new CrosswordLetter(letter, new CrosswordWordPosition(x, y, Orientation.Horizontal)));
                }
            }
            catch
            {
            }

            crossword.SetSize(width, height);
        }

        internal string SerializeCrossword(Crossword crossword, List<CrosswordLetter> progress)
        {
            var sb = new StringBuilder(SerializeCrossword(crossword));
            sb.AppendLine(crossword.WordHelpers.ToString());
            sb.AppendLine(crossword.LetterHelpers.ToString());
            sb.AppendLine(progress.Count.ToString());
            foreach (CrosswordLetter letter in progress)
            {
                sb.AppendLine(letter.Letter.ToString());
                sb.AppendLine(letter.Position.X.ToString());
                sb.AppendLine(letter.Position.Y.ToString());
            }
            return sb.ToString();
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

    class CrosswordGenerator
    {
        private readonly static Random random = new Random();

        internal int Generate(Crossword crossword, Dictionary dictionary)
        {
            if (dictionary.DictionaryWords.Length == 0)
            {
                return -1;
            }
            
            int blankIterations = 0;
            int wordsHaveBeenAdded = 0;

            while (blankIterations < 100)
            {
                var dictionaryWord = dictionary.GetRandomDictionaryWord();
                var positions = crossword.GetPreviewsPositions(dictionaryWord);

                if (positions != null)
                {
                    if (positions.Count == 0)
                    {
                        blankIterations++;
                        continue;
                    }

                    var position = positions[random.Next(0, positions.Count)]; //  range: [0; positions.Count - 1]  
                    var crosswordWord = new CrosswordWord(crossword, dictionaryWord, position.Position, false);
                    crossword.AddWord(crosswordWord);
                    wordsHaveBeenAdded = 1;
                }
                else
                {
                    int x = random.Next(0, crossword.Width);
                    int y = random.Next(0, crossword.Height);
                    int lengthOfWord = dictionaryWord.Word.Length;

                    while (x + lengthOfWord - 1 > crossword.Width - 1)
                    {
                        x = random.Next(0, crossword.Width);
                    }

                    crossword.AddWord(new CrosswordWord(crossword, dictionaryWord, new CrosswordWordPosition(x, y, Orientation.Horizontal), false));
                    wordsHaveBeenAdded = 1;
                }                
                
                blankIterations = 0;
            }

            return wordsHaveBeenAdded;
        }
    }
}
