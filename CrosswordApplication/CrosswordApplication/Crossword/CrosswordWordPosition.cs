using System.Text;

namespace CrosswordApplication.Crossword
{
    public class CrosswordWordPosition
    {
        public CrosswordWordPosition(int x, int y, Orientation orientation)
        {
            this.X = x;
            this.Y = y;
            this.Orientation = orientation;
        }

        public int X { get; }

        public int Y { get; }

        public Orientation Orientation { get; }

        public static void NextPosition(Orientation orientation, ref int x, ref int y)
        {
            if (orientation == Orientation.Horizontal)
            {
                x = x + 1;
            }
            else
            {
                y = y + 1;
            }
        }

        public static void NextIndexPosition(Orientation orientation, int index, ref int x, ref int y)
        {
            if (orientation == Orientation.Horizontal)
            {
                x = x + index;
            }
            else
            {
                y = y + index;
            }
        }
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }
}
