using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrosswordApplication.Crossword;

namespace Crossword
{
    public class Crossword
    {
        public static readonly int DefaultCrosswordWidth = 20;
        public static readonly int DefaultCrosswordHeight = 20;

        private readonly int width;
        private readonly int height;
        private List<CrosswordWord> crosswordWords;

        public Crossword()
        {
            width = DefaultCrosswordWidth;
            height = DefaultCrosswordHeight;
            crosswordWords = new List<CrosswordWord>();
        }



        public bool IsLoaded()
        {
            return crosswordWords != null && crosswordWords.Count > 0;
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }
    }
}
