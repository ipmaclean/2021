using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_23
{
    public class Tile
    {
        public TileType Type { get; set; }
        public (int, int) Coords { get; set; }

        public Tile()
        {
        }

        public Tile((int, int) coords)
        {
            Coords = coords;
        }

        public Tile((int, int) coords, TileType type)
        {
            Type = type;
            Coords = coords;
        }
    }
}
