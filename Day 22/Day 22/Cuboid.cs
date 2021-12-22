using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_22
{
    public class Cuboid
    {
        public (int, int, int) Vertex1 { get; set; }
        public (int, int, int) Vertex2 { get; set; }

        public Cuboid((int, int, int) vertex1, (int, int, int) vertex2)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
        }

        public long CalculateVolume()
        {
            return (long)(Vertex2.Item1 - Vertex1.Item1 + 1) * (long)(Vertex2.Item2 - Vertex1.Item2 + 1) * (long)(Vertex2.Item3 - Vertex1.Item3 + 1);
        }
    }
}
