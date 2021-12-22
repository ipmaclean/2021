using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_22
{
    public class Instruction
    {
        public Cuboid Cuboid { get; set; }
        public string Name { get; set; }

        public Instruction(string name, int x1, int x2, int y1, int y2, int z1, int z2)
        {
            Name = name;
            Cuboid = new Cuboid((x1, y1, z1), (x2, y2, z2));
        }
    }
}
