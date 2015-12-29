namespace CrosswordApplication.Crossword
{
    public class CrosswordLetter
    {
        private string letter;
        private CrosswordWordPosition position;

        public CrosswordLetter(string letter, CrosswordWordPosition position)
        {
            this.letter = letter;
            this.position = position;
        }

        public string Letter
        {
            get { return letter; }
        }

        public CrosswordWordPosition Position
        {
            get { return position; }
        }
    }
}
