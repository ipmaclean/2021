using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_25
{
    public class Cucumber
    {
        public char Facing { get; set; }
        public (int, int) Coords { get; set; }
        public bool WillMove { get; set; } = false;

        public Cucumber(char facing, (int, int) coords)
        {
            Facing = facing;
            Coords = coords;
        }

        public void Move(int columnCount, int rowCount)
        {
            if (Facing == '>')
            {
                if (Coords.Item1 < columnCount - 1)
                {
                    Coords = (Coords.Item1 + 1, Coords.Item2);
                }
                else
                {
                    Coords = (0, Coords.Item2);
                }
            }
            else
            {
                if (Coords.Item2 < rowCount - 1)
                {
                    Coords = (Coords.Item1, Coords.Item2 + 1);
                }
                else
                {
                    Coords = (Coords.Item1, 0);
                }
            }
            WillMove = false;
        }
    }
}
