namespace CrosswordApplication.Crossword
{
    internal class CrosswordWordPosition
    {
        private readonly int x;
        private readonly int y;
        private readonly Orientation orientation;

        public CrosswordWordPosition(int x, int y, Orientation orientation)
        {
            this.x = x;
            this.y = y;
            this.orientation = orientation;
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Orientation Orientation
        {
            get { return orientation; }
        }
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }
}
