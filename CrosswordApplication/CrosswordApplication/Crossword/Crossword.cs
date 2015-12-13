﻿using System;
using System.Collections.Generic;
using System.Text;
using CrosswordApplication.Crossword;
using CrosswordApplication.Dictionary;

namespace Crossword
{
    public class Crossword
    {
        public static readonly string DefaultFileName = @"..\..\debug crossword.cwd";

        public static readonly int DefaultCrosswordWidth = 20;
        public static readonly int DefaultCrosswordHeight = 20;

        private int width;
        private int height;

        // TODO Replace with unmodifiable list
        private readonly List<CrosswordWord> crosswordWords;

        private readonly CrosswordSerializer serializer = new CrosswordSerializer();

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
            UpdateState();
        }

        public void UpdateState()
        {
            
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
                string word = crosswordWord.Word;
                
                int curX = crosswordWord.Position.X;
                int curY = crosswordWord.Position.Y;

                for (int i = 0; i < word.Length; i++)
                {
                    if (crosswordWord.Position.Orientation == Orientation.Horizontal)
                    {
                        if (curX == x && curY + i == y)
                        {
                            foundedWords.Add(crosswordWord);
                        }
                    }
                    if (crosswordWord.Position.Orientation == Orientation.Horizontal)
                    {
                        if (curX + i == x && curY == y)
                        {
                            foundedWords.Add(crosswordWord);
                        }
                    }
                }
            }
            
            return foundedWords;
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
