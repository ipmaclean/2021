using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_22
{
    public class Instruction
    {
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }
        public int z1 { get; set; }
        public int z2 { get; set; }
        public string Name { get; set; }

        public Instruction(string name, int x1, int x21, int y1, int y2, int z1, int z2)
        {
            Name = name;
            this.x1 = x1;
            this.x2 = x21;
            this.y1 = y1;
            this.y2 = y2;
            this.z1 = z1;
            this.z2 = z2;
        }
    }
}
